using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using AMPS.Client;

namespace AMPSExcel
{
    public partial class ManageSubsForm : Form
    {
        AMPSAddin.SubscriptionDefinition[] _subs;

        public ManageSubsForm()
        {
            InitializeComponent();
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = new SubscriptionForm(Globals.AMPSAddin.Application.ActiveWorkbook);
            var sd = _subs[e.RowIndex];
            form.SubscriptionName = sd.Name;
            // don't let the user modify this name.
            form.txtName.Enabled = false;
            form.ServerName = sd.ServerName;
            form.Topic = sd.Topic;
            form.Filter = sd.Filter;
            form.WorksheetRange = sd.WorksheetRange;
            bool wasActive = ActiveSub.isActive(sd);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sd.Name = form.SubscriptionName;
                sd.ServerName = form.ServerName;
                sd.Topic = form.Topic;
                sd.Filter = form.Filter;
                sd.WorksheetRange = form.WorksheetRange;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (wasActive)
                {
                    row.Cells[0].Value = false;
                    deactivate(e.RowIndex);
                    var wbInfo = Globals.AMPSAddin.getWorkbookInfo(
                        Globals.AMPSAddin.Application.ActiveWorkbook);
                    wbInfo.createOrUpdate(sd);
                    if (activate(e.RowIndex))
                    {
                        row.Cells[0].Value = true;
                    }
                }
                row.Cells[1].Value = sd.Name;
                row.Cells[2].Value = sd.ServerName;
                row.Cells[3].Value = sd.Topic;
                row.Cells[4].Value = sd.Filter;
                row.Cells[5].Value = sd.WorksheetRange;
            }
        }

        private bool activate(int index)
        {
            this.Cursor = Cursors.WaitCursor;
            string subName = dataGridView1.Rows[index].Cells[1].Value.ToString();
            Globals.AMPSAddin.Application.StatusBar = "Activating subscription " + subName + "...";

            // activate
            try
            {
                var workbook = Globals.AMPSAddin.Application.ActiveWorkbook;
                var ampsWBInfo = Globals.AMPSAddin.getWorkbookInfo(workbook);
                Client c = new Client(Guid.NewGuid().ToString());
                AMPSAddin.ServerDefinition serverDef = ampsWBInfo.Servers[_subs[index].ServerName];
                c.connect(serverDef.URL);
                c.logon();
                ActiveSub.activate(_subs[index], c, serverDef.MessageType, workbook);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Format("Error activating: {0}", ex.Message),
                    "Error activating subscription "+ subName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Globals.AMPSAddin.Application.StatusBar = false;
                return false;
            }
            Globals.AMPSAddin.Application.StatusBar = false;
            this.Cursor = Cursors.Default;
            return true;
        }

        private void deactivate(int index)
        {
            var activeSub = ActiveSub.find(_subs[index]);
            if (activeSub == null) return;
            activeSub.close();
        }

        private void ManageSubsForm_Load(object sender, EventArgs e)
        {
            // get our current workbook
            var workbook = Globals.AMPSAddin.Application.ActiveWorkbook;
            _subs = new AMPSAddin.SubscriptionDefinition[Globals.AMPSAddin.getWorkbookInfo(workbook).Subscriptions.Count];
            int i = 0;
            foreach (var sub in Globals.AMPSAddin.getWorkbookInfo(workbook).Subscriptions.Values)
            {
                bool active = ActiveSub.isActive(sub);
                this.dataGridView1.Rows.Add(
                    active,
                    sub.Name,
                    sub.ServerName,
                    sub.Topic,
                    sub.Filter,
                    sub.WorksheetRange,
                    "X");
                _subs[i++] = sub;

            }
        }


        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var cell= dataGridView1.CurrentCell;
            if (dataGridView1.IsCurrentCellDirty)
            {
                if (dataGridView1.Columns[cell.ColumnIndex].Name == "Active" && cell.RowIndex >= 0)
                {
                    bool isChecked = (bool)(dataGridView1.Rows[cell.RowIndex].Cells[0].Value);
                    if (!isChecked)
                    {
                        if (activate(cell.RowIndex))
                        {
                            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }
                        else
                        {
                            dataGridView1.CancelEdit();
                        }
                    }
                    else
                    {
                        deactivate(cell.RowIndex);
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }

                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                // delete it.  first deactivate
                deactivate(e.RowIndex);
                var wbInfo = Globals.AMPSAddin.getWorkbookInfo(
                    Globals.AMPSAddin.Application.ActiveWorkbook);
                // then remove
                wbInfo.Subscriptions.Remove(_subs[e.RowIndex].Name);
                // then fix up the subscriptions
                for (int i = e.RowIndex; i < dataGridView1.Rows.Count - 1; i++)
                {
                    _subs[i] = _subs[i + 1];
                }
                _subs[dataGridView1.Rows.Count - 1] = null;
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].Value != true && activate(i))
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
            }

        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].Value != false)
                {
                    deactivate(i);
                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }
    }
}
