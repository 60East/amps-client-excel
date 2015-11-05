using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMPS.Client;
using AMPS.Client.Exceptions;
using Excel = Microsoft.Office.Interop.Excel;

namespace AMPSExcel
{
    public partial class SubscriptionForm : Form
    {
        public const string NEW_SERVER = "< New... >";
        private Client _ampsClient;
        private Excel.Workbook _workbook;

        public SubscriptionForm(Excel.Workbook workbook)
        {
            _workbook = workbook;
            InitializeComponent();
            // configure servers
            cmbServer.Items.Add(NEW_SERVER);
            foreach (var serverName in Globals.AMPSAddin.getWorkbookInfo(_workbook).Servers.Keys)
            {
                cmbServer.Items.Add(serverName);
            }
        }

        public string SubscriptionName
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        }

        public string ServerName
        {
            get
            {
                return cmbServer.Text;
            }
            set
            {
                cmbServer.SelectedItem = value;
                cmbServer.Text = value;
            }
        }

        public string WorksheetRange
        {
            get
            {
                return txtRange.Text;
            }
            set
            {
                txtRange.Text = value;
            }
        }

        public string Topic
        {
            get
            {
                return cmbTopic.Text;
            }
            set
            {
                cmbTopic.Text = value;
            }
        }

        public string Filter
        {
            get
            {
                return txtFilter.Text;
            }
            set
            {
                txtFilter.Text = value;
            }
        }

        private void SubscriptionForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServer.Text == NEW_SERVER)
            {
                // go create a new server
                ServerForm f = new ServerForm();
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var sd = new AMPSAddin.ServerDefinition
                    {
                        Name = f.ServerName,
                        URL = f.getURL(),
                        MessageType = f.MessageType
                    };
                    Globals.AMPSAddin.getWorkbookInfo(_workbook).createOrUpdate(sd);
                    Globals.AMPSAddin.getWorkbookInfo(_workbook).Servers[f.ServerName] = sd;

                    cmbServer.Items.Add(f.ServerName);
                    cmbServer.Text = f.ServerName;
                }
                else
                {
                    cmbServer.SelectedItem = null;
                    cmbServer.Text = null;
                }
            }
            // all topics are gone -- reset them.
            cmbTopic.Items.Clear();
            if (!string.IsNullOrEmpty(cmbServer.Text))
            {
                string url = Globals.AMPSAddin.getWorkbookInfo(_workbook).Servers[cmbServer.Text].URL;
                beginNewTopicSubscription(url);
            }

            updateControls();
        }

        private void beginNewTopicSubscription(string url)
        {
            try
            {
                if(_ampsClient!=null)
                {
                    _ampsClient.disconnect();
                }
            }
            catch (Exception )
            {
            }

            try
            {
                _ampsClient = new Client(Guid.NewGuid().ToString());
                _ampsClient.connect(url);
                _ampsClient.logon();
                // make it quick!  Don't want to see the ui lockup.
                _ampsClient.sowAndSubscribe(msg => processTopic(msg), "/AMPS/SOWStats", 1000);
            }
            catch (Exception)
            {
            }
        }

        private void processTopic(AMPS.Client.Message message)
        {
            try
            {
                if (message.Data.Length > 0)
                {
                    string data = message.Data;
                    int idx = data.IndexOf("20066=");
                    idx += 6;
                    string topicName = data.Substring(idx, data.IndexOf((char)0x01, idx) - idx);
                    this.BeginInvoke(new Action(() =>
                        {
                            if (!this.cmbTopic.Items.Contains(topicName))
                            {
                                this.cmbTopic.Items.Add(topicName);
                            }
                        }
                    ));
                }
            }
            catch (Exception)
            {
            }
        }

        private void updateControls()
        {
            if (!string.IsNullOrEmpty(cmbServer.Text) && !Globals.AMPSAddin.getWorkbookInfo(_workbook).Servers.ContainsKey(cmbServer.Text))
            {
                // server is now invalid
                cmbServer.Text = "";
            }
            if (txtName.Text != "" && cmbServer.Text != "" && cmbTopic.Text != ""
                && txtRange.Text != "")
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void SubscriptionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _ampsClient.close();
            }
            catch (Exception) { }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            updateControls();
        }

        private void txtRange_TextChanged(object sender, EventArgs e)
        {
            updateControls();
        }

        private void cmbTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateControls();
        }

        private void cmbTopic_TextChanged(object sender, EventArgs e)
        {
            updateControls();
        }

    }
}
