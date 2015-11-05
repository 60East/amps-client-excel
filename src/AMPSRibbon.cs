using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;
using AMPS.Client;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;

namespace AMPSExcel
{
    public partial class AMPSRibbon
    {
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            //var topicStore = Globals.AMPSAddin.GetTopicStoreForActiveWorkbook();
            Excel.Range activeCell = Globals.AMPSAddin.Application.ActiveWindow.ActiveCell;

            try
            {
                Excel.Workbook activeWorkbook = Globals.AMPSAddin.Application.ActiveWorkbook;
                var form = new SubscriptionForm(activeWorkbook);
                form.WorksheetRange = Globals.AMPSAddin.Application.ActiveWindow.ActiveSheet.Name+"!"+ activeCell.Address;
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AMPSAddin.ServerDefinition serverDef = Globals.AMPSAddin.getWorkbookInfo(activeWorkbook).Servers[form.ServerName]; 
                    Client c = new Client(Guid.NewGuid().ToString());
                    c.connect(serverDef.URL);
                    c.logon();
                    string[] worksheetNames = form.WorksheetRange.Split('!');
                    AMPSAddin.SubscriptionDefinition def = new AMPSAddin.SubscriptionDefinition
                    {
                        Name = form.SubscriptionName,
                        ServerName = form.ServerName,
                        Topic = form.Topic,
                        WorksheetRange = form.WorksheetRange,
                        Filter = form.Filter
                    };

                    Globals.AMPSAddin.getWorkbookInfo(activeWorkbook).createOrUpdate(def);

                    ActiveSub.activate(def, c, serverDef.MessageType, Globals.AMPSAddin.Application.ActiveWorkbook);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            new ManageSubsForm().ShowDialog();

        }

    }
}
