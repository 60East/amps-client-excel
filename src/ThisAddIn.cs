using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;

namespace AMPSExcel
{
    public partial class AMPSAddin
    {
        public class SubscriptionDefinition
        {
            public string Name { get; set; }
            public string ServerName { get; set; }
            public string Topic { get; set; }
            public string Filter { get; set; }
            public string WorksheetRange { get; set; }
            public int Row { get; set; }
        }

        public class ServerDefinition
        {
            public string Name { get; set; }
            public string URL { get; set; }
            public string MessageType { get; set; }
            public int Row { get; set; }
        }

        public class WorkbookInfo
        {
            Excel.Workbook _workbook;
            public Dictionary<string, ServerDefinition> Servers = new Dictionary<string,ServerDefinition>();
            public Dictionary<string, SubscriptionDefinition> Subscriptions = new Dictionary<string,SubscriptionDefinition>();
            public WorkbookInfo(Excel.Workbook workbook)
            {
                // make the special worksheet
                _workbook = workbook;
                Excel.Worksheet subsSheet = getWorksheet("amps-subs");
                Excel.Worksheet serversSheet = getWorksheet("amps-servers");
                for (int i = 1; i < serversSheet.Cells.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(serversSheet.Cells[i, 1].Value)) break;
                    string name = serversSheet.Cells[i, 1].Value;
                    string url = serversSheet.Cells[i, 2].Value;
                    string messageType = serversSheet.Cells[i, 3].Value;
                    this.Servers[name] = new ServerDefinition
                    {
                        Name = name,
                        URL = url,
                        MessageType = messageType,
                        Row = i
                    };
                }
                for (int i = 1; i < subsSheet.Cells.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(subsSheet.Cells[i, 1].Value)) break;
                    string serverName = subsSheet.Cells[i, 1].Value;
                    this.Subscriptions[subsSheet.Cells[i, 1].Value] = new SubscriptionDefinition
                    {
                        Name = subsSheet.Cells[i,1].Value,
                        ServerName = subsSheet.Cells[i, 2].Value,
                        Topic = subsSheet.Cells[i, 3].Value,
                        Filter = subsSheet.Cells[i, 4].Value,
                        WorksheetRange = subsSheet.Cells[i, 5].Value,
                        Row = i
                    };
                }

            }

            private Excel.Worksheet getWorksheet(string worksheetName)
            {
                Excel.Worksheet worksheet;
                try
                {
                    worksheet = _workbook.Worksheets[worksheetName];
                }
                catch (Exception)
                {
                    worksheet = _workbook.Worksheets.Add();
                    worksheet.Name = worksheetName;
                }
                worksheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                return worksheet;
            }

            public void createOrUpdate(SubscriptionDefinition d)
            {
                var subs = getWorksheet("amps-subs");
                int row = 1;
                if (!Subscriptions.ContainsKey(d.Name))
                {
                    // go make a new row.
                    while (true)
                    {
                        if (string.IsNullOrEmpty(subs.Cells[row, 1].Value)) break;
                        row++;
                    }
                }
                else
                {
                    row = Subscriptions[d.Name].Row;
                }

                subs.Cells[row, 1].Value = d.Name;
                subs.Cells[row, 2].Value = d.ServerName;
                subs.Cells[row, 3].Value = d.Topic;
                subs.Cells[row, 4].Value = d.Filter;
                subs.Cells[row, 5].Value = d.WorksheetRange;
                d.Row = row;
                Subscriptions[d.Name] = d;
            }

            public void createOrUpdate(ServerDefinition d)
            {
                var servers = getWorksheet("amps-servers");
                int row = 1;
                if (!Servers.ContainsKey(d.Name))
                {
                    while (true)
                    {
                        if (string.IsNullOrEmpty(servers.Cells[row, 1].Value)) break;
                        row++;
                    }
                }
                else
                {
                    row = Servers[d.Name].Row;
                }
                servers.Cells[row, 1].Value = d.Name;
                servers.Cells[row, 2].Value = d.URL;
                servers.Cells[row, 3].Value = d.MessageType;
                d.Row = row;
                Servers[d.Name] = d;
            }
        }
        public Dictionary<Excel.Workbook, WorkbookInfo> _workbooks = new Dictionary<Excel.Workbook,WorkbookInfo>();

        public WorkbookInfo getWorkbookInfo(Excel.Workbook workbook)
        {
            if (!_workbooks.ContainsKey(workbook))
            {
                _workbooks[workbook] = new WorkbookInfo(workbook);
            }
            return _workbooks[workbook];
        }


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
          
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
