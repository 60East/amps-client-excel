namespace AMPSExcel
{
    partial class AMPSRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AMPSRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.amps = this.Factory.CreateRibbonTab();
            this.Topics = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.amps.SuspendLayout();
            this.Topics.SuspendLayout();
            // 
            // amps
            // 
            this.amps.Groups.Add(this.Topics);
            this.amps.Label = "AMPS";
            this.amps.Name = "amps";
            // 
            // Topics
            // 
            this.Topics.Items.Add(this.button1);
            this.Topics.Items.Add(this.button2);
            this.Topics.Label = "Topics";
            this.Topics.Name = "Topics";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = global::AMPSExcel.Properties.Resources.topic;
            this.button1.Label = "New Subscription";
            this.button1.Name = "button1";
            this.button1.ScreenTip = "Creates a new AMPS topic subscription in this worksheet.";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Image = global::AMPSExcel.Properties.Resources.manageSubs;
            this.button2.Label = "Manage Subscriptions";
            this.button2.Name = "button2";
            this.button2.ScreenTip = "Activate, deactivate, and edit subscriptions in this workbook.";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // AMPSRibbon
            // 
            this.Name = "AMPSRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.amps);
            this.amps.ResumeLayout(false);
            this.amps.PerformLayout();
            this.Topics.ResumeLayout(false);
            this.Topics.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab amps;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Topics;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
    }

    partial class ThisRibbonCollection
    {
        internal AMPSRibbon AMPSRibbon
        {
            get { return this.GetRibbon<AMPSRibbon>(); }
        }
    }
}
