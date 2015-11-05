using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMPS.Client;
using Excel= Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Runtime.InteropServices;

namespace AMPSExcel
{
    public class ActiveSub
    {
        Client _client;
        CommandId _cmdId;
        int _row, _col;
        Excel.Worksheet _worksheet;
        Excel.Workbook _workbook;
        AMPSAddin.SubscriptionDefinition _def;
        MessageType _messageType;

        Dictionary<string, int> _columns = new Dictionary<string, int>();
        Dictionary<string, int> _rows = new Dictionary<string, int>();
        Dictionary<string, object> _shredded = new Dictionary<string, object>();
        Queue<int> _empty = new Queue<int>();
        volatile int _lastrow = 0;
        bool _running = true;

        static Dictionary<AMPSAddin.SubscriptionDefinition, ActiveSub> _actives = new Dictionary<AMPSAddin.SubscriptionDefinition, ActiveSub>();

        public static ActiveSub activate(AMPSAddin.SubscriptionDefinition d, Client client, string messageType, Excel.Workbook workbook)
        {
            if (!_actives.ContainsKey(d))
            {
                _actives[d] = new ActiveSub(client, messageType, d, workbook);
            }
            return _actives[d];

        }

        public static bool isActive(AMPSAddin.SubscriptionDefinition d)
        {
            return _actives.ContainsKey(d);
        }

        public static ActiveSub find(AMPSAddin.SubscriptionDefinition d)
        {
            if (_actives.ContainsKey(d))
            {
                return _actives[d];
            }
            else
            {
                return null;
            }
        }

        private void vbaInvoke(System.Action thing)
        {
            while (_running)
            {
                try
                {
                    thing.Invoke();
                    break;
                }
                catch (COMException)
                {
                    Thread.Yield();
                }
            }
        }
        private void ReceivedMessage(AMPS.Client.Message msg)
        {
            if (!_running) return;
            /*try
            {*/
            string sowKey = msg.getSowKey();
            // if an OOF, remove the row if we know of it
            if (msg.Command == AMPS.Client.Message.Commands.OOF && _rows.ContainsKey(sowKey))
            {
                int theRow = _rows[sowKey];

                vbaInvoke(() =>
                {
                    Excel.Range range = _worksheet.Rows[theRow];
                    range.ClearContents();
                    _worksheet.Cells[theRow, _col] = "(deleted)";
                }
                );
                _rows.Remove(sowKey);
                _empty.Enqueue(theRow);
            }
            else if (msg.Command == AMPS.Client.Message.Commands.Publish || msg.Command == AMPS.Client.Message.Commands.DeltaPublish ||
                    msg.Command == AMPS.Client.Message.Commands.SOW)
            {
                int thisRow = 0;
                // try and find the row
                if (!_rows.TryGetValue(sowKey,out thisRow))
                {
                    string findKey = "|" + sowKey + "|";
                    vbaInvoke(() =>
                    {
                        if (_empty.Count > 0)
                        {
                            thisRow = _empty.Dequeue();

                        }
                        else
                        {
                            thisRow = _lastrow++;
                        }
                        _rows[sowKey] = thisRow;
                        _worksheet.Cells[thisRow, _col].Value = findKey;
                    });
                    if (thisRow == -1) return;
                }
                // row == the row we want
                _shredded.Clear();
                var dataField = msg.getDataRaw();
                _messageType.PopulateDictionary(dataField.buffer, dataField.position, dataField.length, _shredded);
                foreach (var x in _shredded.Keys)
                {
                    int thisCol = 0;
                    if (!_columns.ContainsKey(x))
                    {
                        // find a spot for a new column.  hopefully this happens rarely.
                        for (int i = _col + 1; i < 1024768; i++)
                        {
                            vbaInvoke(() =>
                            {
                                dynamic c = _worksheet.Cells[_row, i];
                                if (c.Text == x)
                                {
                                    thisCol = i;
                                    _columns.Add(x, thisCol);
                                }
                                else if (c.Text == null || c.Text == "")
                                {
                                    c.Value = x;
                                    c.Font.Bold = true;
                                    thisCol = i;
                                    _columns.Add(x, thisCol);
                                }
                            });
                            if (thisCol != 0) break;

                        }
                        if (thisCol == 0) return; // no room
                    }
                    else { thisCol = _columns[x]; }
                    vbaInvoke(() => _worksheet.Cells[thisRow, thisCol].Value = _shredded[x].ToString());
                }
            }
            /*}
            catch (Exception e)
            {
                this.close();
                _running = false;
            }*/
        }
        private ActiveSub(Client c, string messageType, AMPSExcel.AMPSAddin.SubscriptionDefinition d, Excel.Workbook workbook)
        {
            _def = d;
            _client = c;
            _messageType = MessageTypeFactory.forName(messageType);
            string[] rangeParts = d.WorksheetRange.Split('!');
            _worksheet = workbook.Worksheets[rangeParts[0]];
            Excel.Range where = _worksheet.Range[rangeParts[1]];
            _row = where.Row;
            _col = where.Column;
            _columns["SOWKey"] = where.Column;
            _workbook = workbook;
            where.Value = "AMPS Subscription: "+ d.Name;
            _empty.Clear();
            int count = _worksheet.Rows.Count;
            for (int i = _row; i < count; ++i)
            {
                var cell = _worksheet.Cells[i, _col];
                if (!string.IsNullOrEmpty(cell.Text))
                {
                    string text = cell.Text;
                    if (text == "(deleted)")
                        _empty.Enqueue(i);
                    else
                        _rows[text.Trim('|')] = i;
                }
                else
                {
                    _lastrow = i;
                    break;
                }
            }
            _cmdId = c.sowAndDeltaSubscribe(x => ReceivedMessage(x), d.Topic, d.Filter, 100, true, true, 1000);
            Globals.AMPSAddin.Application.WorkbookBeforeClose += new Excel.AppEvents_WorkbookBeforeCloseEventHandler(Application_WorkbookBeforeClose);
            workbook.BeforeClose += new Excel.WorkbookEvents_BeforeCloseEventHandler(workbook_BeforeClose);

        }
        void Application_WorkbookBeforeClose(Excel.Workbook Wb, ref bool Cancel)
        {
            if (Wb.Equals(_workbook))
            {
                close();
            }
        }

        void Subscription_Disposed(object sender, EventArgs e)
        {
            close();
        }

        void Worksheet_Deactivate()
        {
            close();
        }

        public void close()
        {
            try
            {
                if (_client != null)
                {
                    _client.close();
                    _client = null;
                }
            }
            catch (Exception)
            {
            }

            ActiveSub._actives.Remove(_def);
            
            _running = false;
        }
        void workbook_BeforeClose(ref bool Cancel)
        {
            close();
        }
    }

}
