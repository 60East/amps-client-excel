namespace AMPSExcel
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblName;
            System.Windows.Forms.Label lblHostName;
            System.Windows.Forms.Label lblPort;
            System.Windows.Forms.Label lblProtocol;
            System.Windows.Forms.Label lbMessageType;
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.grpConnectionDetails = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.MaskedTextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.chkLogon = new System.Windows.Forms.CheckBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.cmbMessageType = new System.Windows.Forms.ComboBox();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            lblName = new System.Windows.Forms.Label();
            lblHostName = new System.Windows.Forms.Label();
            lblPort = new System.Windows.Forms.Label();
            lblProtocol = new System.Windows.Forms.Label();
            lbMessageType = new System.Windows.Forms.Label();
            this.grpConnectionDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(19, 25);
            lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(49, 17);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // lblHostName
            // 
            lblHostName.AutoSize = true;
            lblHostName.Location = new System.Drawing.Point(8, 30);
            lblHostName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblHostName.Name = "lblHostName";
            lblHostName.Size = new System.Drawing.Size(80, 17);
            lblHostName.TabIndex = 2;
            lblHostName.Text = "Host name:";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new System.Drawing.Point(8, 59);
            lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new System.Drawing.Size(38, 17);
            lblPort.TabIndex = 4;
            lblPort.Text = "Port:";
            // 
            // lblProtocol
            // 
            lblProtocol.AutoSize = true;
            lblProtocol.Location = new System.Drawing.Point(8, 89);
            lblProtocol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblProtocol.Name = "lblProtocol";
            lblProtocol.Size = new System.Drawing.Size(64, 17);
            lblProtocol.TabIndex = 6;
            lblProtocol.Text = "Protocol:";
            // 
            // lbMessageType
            // 
            lbMessageType.AutoSize = true;
            lbMessageType.Location = new System.Drawing.Point(8, 121);
            lbMessageType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbMessageType.Name = "lbMessageType";
            lbMessageType.Size = new System.Drawing.Size(100, 17);
            lbMessageType.TabIndex = 6;
            lbMessageType.Text = "Message type:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(108, 21);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 22);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtHostName
            // 
            this.txtHostName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtHostName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtHostName.Location = new System.Drawing.Point(125, 26);
            this.txtHostName.Margin = new System.Windows.Forms.Padding(4);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(160, 22);
            this.txtHostName.TabIndex = 2;
            this.txtHostName.TextChanged += new System.EventHandler(this.txtHostName_TextChanged);
            // 
            // grpConnectionDetails
            // 
            this.grpConnectionDetails.Controls.Add(this.txtPort);
            this.grpConnectionDetails.Controls.Add(this.txtPassword);
            this.grpConnectionDetails.Controls.Add(this.txtUserName);
            this.grpConnectionDetails.Controls.Add(this.lblPassword);
            this.grpConnectionDetails.Controls.Add(this.lblUserName);
            this.grpConnectionDetails.Controls.Add(this.chkLogon);
            this.grpConnectionDetails.Controls.Add(this.btnTestConnection);
            this.grpConnectionDetails.Controls.Add(this.cmbMessageType);
            this.grpConnectionDetails.Controls.Add(lbMessageType);
            this.grpConnectionDetails.Controls.Add(this.cmbProtocol);
            this.grpConnectionDetails.Controls.Add(lblProtocol);
            this.grpConnectionDetails.Controls.Add(lblPort);
            this.grpConnectionDetails.Controls.Add(lblHostName);
            this.grpConnectionDetails.Controls.Add(this.txtHostName);
            this.grpConnectionDetails.Location = new System.Drawing.Point(16, 60);
            this.grpConnectionDetails.Margin = new System.Windows.Forms.Padding(4);
            this.grpConnectionDetails.Name = "grpConnectionDetails";
            this.grpConnectionDetails.Padding = new System.Windows.Forms.Padding(4);
            this.grpConnectionDetails.Size = new System.Drawing.Size(373, 304);
            this.grpConnectionDetails.TabIndex = 6;
            this.grpConnectionDetails.TabStop = false;
            this.grpConnectionDetails.Text = "Connection Details";
            // 
            // txtPort
            // 
            this.txtPort.AllowPromptAsInput = false;
            this.txtPort.Location = new System.Drawing.Point(125, 55);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Mask = "00000";
            this.txtPort.Name = "txtPort";
            this.txtPort.PromptChar = ' ';
            this.txtPort.Size = new System.Drawing.Size(132, 22);
            this.txtPort.TabIndex = 3;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(125, 211);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(160, 22);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(125, 180);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(160, 22);
            this.txtUserName.TabIndex = 7;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Enabled = false;
            this.lblPassword.Location = new System.Drawing.Point(33, 215);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 17);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Password:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Enabled = false;
            this.lblUserName.Location = new System.Drawing.Point(33, 184);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(81, 17);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "User name:";
            // 
            // chkLogon
            // 
            this.chkLogon.AutoSize = true;
            this.chkLogon.Location = new System.Drawing.Point(12, 154);
            this.chkLogon.Margin = new System.Windows.Forms.Padding(4);
            this.chkLogon.Name = "chkLogon";
            this.chkLogon.Size = new System.Drawing.Size(226, 21);
            this.chkLogon.TabIndex = 6;
            this.chkLogon.Text = "My server requires me to logon";
            this.chkLogon.UseVisualStyleBackColor = true;
            this.chkLogon.CheckedChanged += new System.EventHandler(this.chkLogon_CheckedChanged);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(213, 268);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(152, 28);
            this.btnTestConnection.TabIndex = 9;
            this.btnTestConnection.Text = "Test Connectivity";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // cmbMessageType
            // 
            this.cmbMessageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMessageType.FormattingEnabled = true;
            this.cmbMessageType.Items.AddRange(new object[] {
            "json",
            "nvfix",
            "fix",
            "other"});
            this.cmbMessageType.Location = new System.Drawing.Point(125, 117);
            this.cmbMessageType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMessageType.Name = "cmbMessageType";
            this.cmbMessageType.Size = new System.Drawing.Size(160, 24);
            this.cmbMessageType.TabIndex = 5;
            this.cmbMessageType.SelectedIndexChanged += new System.EventHandler(this.cmbMessageType_SelectedIndexChanged);
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "amps",
            "fix",
            "nvfix"});
            this.cmbProtocol.Location = new System.Drawing.Point(125, 85);
            this.cmbProtocol.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(160, 24);
            this.cmbProtocol.TabIndex = 4;
            this.cmbProtocol.SelectedIndexChanged += new System.EventHandler(this.cmbMessageType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(291, 378);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(183, 378);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ServerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(403, 416);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpConnectionDetails);
            this.Controls.Add(this.txtName);
            this.Controls.Add(lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ServerForm";
            this.Text = "AMPS Server Properties";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.grpConnectionDetails.ResumeLayout(false);
            this.grpConnectionDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.GroupBox grpConnectionDetails;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkLogon;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.MaskedTextBox txtPort;
        private System.Windows.Forms.ComboBox cmbMessageType;
    }
}