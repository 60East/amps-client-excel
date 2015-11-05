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

namespace AMPSExcel
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        public string getURL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("tcp://");
            if (chkLogon.Checked)
            {
                sb.Append(txtUserName.Text);
                if(txtPassword.Text.Length > 0)
                {
                    sb.AppendFormat(":{0}", txtPassword.Text);
                }
                sb.Append('@');
            }
            sb.AppendFormat("{0}:{1}/{2}", txtHostName.Text, txtPort.Text, cmbProtocol.Text);
            return sb.ToString();
        }

        public string MessageType
        {
            get
            {
                return cmbMessageType.Text;
            }
        }
        public string ServerName
        {
            get
            {
                return txtName.Text;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            // Try to create a new client and logon.
            this.Cursor = Cursors.WaitCursor;
            Globals.AMPSAddin.Application.StatusBar = "Testing connnectivity...";
            try
            {
                Client aClient = new Client(Guid.NewGuid().ToString());
                aClient.connect(getURL());
                aClient.logon();
                this.Cursor = Cursors.Default;
                MessageBox.Show(this, "Test connection succeeded.", "AMPS Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                aClient.close();
            }
            catch (AMPSException ae)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(this, string.Format("Error connecting to server: {0}{1}",
                     ae.Message, ae.InnerException != null ? (" (" + ae.InnerException.Message + ")") : ""),
                     "AMPS Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Globals.AMPSAddin.Application.StatusBar = false;
        }

        private void updateControlStates()
        {
            if (txtName.Text.Length > 0 &&
                txtHostName.Text.Length > 0 &&
                txtPort.Text.Length > 0 &&
                cmbProtocol.Text.Length > 0)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
            if (chkLogon.Checked)
            {
                lblPassword.Enabled = lblUserName.Enabled =
                    txtPassword.Enabled = txtUserName.Enabled = true;
            }
            else
            {
                lblPassword.Enabled = lblUserName.Enabled =
                    txtPassword.Enabled = txtUserName.Enabled = false;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            updateControlStates();
        }

        private void txtHostName_TextChanged(object sender, EventArgs e)
        {
            updateControlStates();
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            updateControlStates();
        }

        private void cmbMessageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateControlStates();
        }

        private void chkLogon_CheckedChanged(object sender, EventArgs e)
        {
            updateControlStates();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            // Select "amps" protocol by default
            cmbProtocol.SelectedIndex = 0;
            cmbProtocol.Text = cmbProtocol.Items[0].ToString();
            cmbMessageType.SelectedIndex = 0;
            cmbMessageType.Text = cmbMessageType.Items[0].ToString();
            updateControlStates();
        }

    }
}
