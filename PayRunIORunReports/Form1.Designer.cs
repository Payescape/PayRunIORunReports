namespace PayRunIORunReports
{
    partial class Form1
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
            this.comboBoxChooseReport = new DevExpress.XtraEditors.ComboBoxEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabMainForm = new DevExpress.XtraTab.XtraTabPage();
            this.dateEndDate = new DevExpress.XtraEditors.DateEdit();
            this.dateStartDate = new DevExpress.XtraEditors.DateEdit();
            this.comboBoxChooseFrequency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblParameter6 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter6 = new DevExpress.XtraEditors.TextEdit();
            this.lblParameter5 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter5 = new DevExpress.XtraEditors.TextEdit();
            this.lblParameter2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter3 = new DevExpress.XtraEditors.TextEdit();
            this.lblParameter3 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter2 = new DevExpress.XtraEditors.TextEdit();
            this.lblParameter4 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter4 = new DevExpress.XtraEditors.TextEdit();
            this.lblParameter1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEditParameter1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabSettings = new DevExpress.XtraTab.XtraTabPage();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtLiveConsumerSecret = new DevExpress.XtraEditors.TextEdit();
            this.txtLiveConsumerKey = new DevExpress.XtraEditors.TextEdit();
            this.txtLiveUrl = new DevExpress.XtraEditors.TextEdit();
            this.txtTestConsumerSecret = new DevExpress.XtraEditors.TextEdit();
            this.txtTestConsumerKey = new DevExpress.XtraEditors.TextEdit();
            this.txtTestUrl = new DevExpress.XtraEditors.TextEdit();
            this.btnRunReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.chkUseLive = new DevExpress.XtraEditors.CheckEdit();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSavePDFReports = new DevExpress.XtraEditors.LabelControl();
            this.btnEditSavePDFReports = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChooseReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabMainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChooseFrequency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter1.Properties)).BeginInit();
            this.xtraTabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveConsumerSecret.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveConsumerKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestConsumerSecret.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestConsumerKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseLive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditSavePDFReports.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxChooseReport
            // 
            this.comboBoxChooseReport.Location = new System.Drawing.Point(57, 32);
            this.comboBoxChooseReport.Name = "comboBoxChooseReport";
            this.comboBoxChooseReport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxChooseReport.Properties.Items.AddRange(new object[] {
            "Combined Payroll Run Report",
            "Department Within Branch Payroll Run Details Report",
            "Note And Coin Requirement Report",
            "Pension Contributions To Date Report",
            "PAPDIS Report"});
            this.comboBoxChooseReport.Size = new System.Drawing.Size(482, 20);
            this.comboBoxChooseReport.TabIndex = 1;
            this.comboBoxChooseReport.SelectedValueChanged += new System.EventHandler(this.comboBoxChooseReport_SelectedValueChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 56);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabMainForm;
            this.xtraTabControl1.Size = new System.Drawing.Size(619, 325);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabMainForm,
            this.xtraTabSettings});
            // 
            // xtraTabMainForm
            // 
            this.xtraTabMainForm.Controls.Add(this.dateEndDate);
            this.xtraTabMainForm.Controls.Add(this.dateStartDate);
            this.xtraTabMainForm.Controls.Add(this.comboBoxChooseFrequency);
            this.xtraTabMainForm.Controls.Add(this.lblParameter6);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter6);
            this.xtraTabMainForm.Controls.Add(this.lblParameter5);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter5);
            this.xtraTabMainForm.Controls.Add(this.lblParameter2);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter3);
            this.xtraTabMainForm.Controls.Add(this.lblParameter3);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter2);
            this.xtraTabMainForm.Controls.Add(this.lblParameter4);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter4);
            this.xtraTabMainForm.Controls.Add(this.lblParameter1);
            this.xtraTabMainForm.Controls.Add(this.txtEditParameter1);
            this.xtraTabMainForm.Controls.Add(this.labelControl1);
            this.xtraTabMainForm.Controls.Add(this.comboBoxChooseReport);
            this.xtraTabMainForm.Name = "xtraTabMainForm";
            this.xtraTabMainForm.Size = new System.Drawing.Size(613, 297);
            this.xtraTabMainForm.Text = "Main form.";
            // 
            // dateEndDate
            // 
            this.dateEndDate.EditValue = null;
            this.dateEndDate.Location = new System.Drawing.Point(57, 190);
            this.dateEndDate.Name = "dateEndDate";
            this.dateEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEndDate.Size = new System.Drawing.Size(482, 20);
            this.dateEndDate.TabIndex = 10;
            // 
            // dateStartDate
            // 
            this.dateStartDate.EditValue = null;
            this.dateStartDate.Location = new System.Drawing.Point(57, 153);
            this.dateStartDate.Name = "dateStartDate";
            this.dateStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStartDate.Size = new System.Drawing.Size(482, 20);
            this.dateStartDate.TabIndex = 9;
            // 
            // comboBoxChooseFrequency
            // 
            this.comboBoxChooseFrequency.Location = new System.Drawing.Point(57, 116);
            this.comboBoxChooseFrequency.Name = "comboBoxChooseFrequency";
            this.comboBoxChooseFrequency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxChooseFrequency.Properties.Items.AddRange(new object[] {
            "Weekly",
            "Monthly",
            "Fortnightly",
            "Four Weekly",
            "Quarterly",
            "Annually"});
            this.comboBoxChooseFrequency.Size = new System.Drawing.Size(482, 20);
            this.comboBoxChooseFrequency.TabIndex = 8;
            // 
            // lblParameter6
            // 
            this.lblParameter6.Location = new System.Drawing.Point(57, 257);
            this.lblParameter6.Name = "lblParameter6";
            this.lblParameter6.Size = new System.Drawing.Size(92, 13);
            this.lblParameter6.TabIndex = 0;
            this.lblParameter6.Text = "Parameter 6 name.";
            // 
            // txtEditParameter6
            // 
            this.txtEditParameter6.Location = new System.Drawing.Point(57, 272);
            this.txtEditParameter6.Name = "txtEditParameter6";
            this.txtEditParameter6.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter6.TabIndex = 7;
            // 
            // lblParameter5
            // 
            this.lblParameter5.Location = new System.Drawing.Point(57, 216);
            this.lblParameter5.Name = "lblParameter5";
            this.lblParameter5.Size = new System.Drawing.Size(92, 13);
            this.lblParameter5.TabIndex = 0;
            this.lblParameter5.Text = "Parameter 5 name.";
            // 
            // txtEditParameter5
            // 
            this.txtEditParameter5.Location = new System.Drawing.Point(57, 231);
            this.txtEditParameter5.Name = "txtEditParameter5";
            this.txtEditParameter5.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter5.TabIndex = 6;
            // 
            // lblParameter2
            // 
            this.lblParameter2.Location = new System.Drawing.Point(57, 101);
            this.lblParameter2.Name = "lblParameter2";
            this.lblParameter2.Size = new System.Drawing.Size(92, 13);
            this.lblParameter2.TabIndex = 0;
            this.lblParameter2.Text = "Parameter 2 name.";
            // 
            // txtEditParameter3
            // 
            this.txtEditParameter3.Location = new System.Drawing.Point(57, 153);
            this.txtEditParameter3.Name = "txtEditParameter3";
            this.txtEditParameter3.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter3.TabIndex = 4;
            // 
            // lblParameter3
            // 
            this.lblParameter3.Location = new System.Drawing.Point(57, 138);
            this.lblParameter3.Name = "lblParameter3";
            this.lblParameter3.Size = new System.Drawing.Size(92, 13);
            this.lblParameter3.TabIndex = 0;
            this.lblParameter3.Text = "Parameter 3 name.";
            // 
            // txtEditParameter2
            // 
            this.txtEditParameter2.Location = new System.Drawing.Point(57, 116);
            this.txtEditParameter2.Name = "txtEditParameter2";
            this.txtEditParameter2.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter2.TabIndex = 3;
            // 
            // lblParameter4
            // 
            this.lblParameter4.Location = new System.Drawing.Point(57, 175);
            this.lblParameter4.Name = "lblParameter4";
            this.lblParameter4.Size = new System.Drawing.Size(92, 13);
            this.lblParameter4.TabIndex = 0;
            this.lblParameter4.Text = "Parameter 4 name.";
            // 
            // txtEditParameter4
            // 
            this.txtEditParameter4.Location = new System.Drawing.Point(57, 190);
            this.txtEditParameter4.Name = "txtEditParameter4";
            this.txtEditParameter4.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter4.TabIndex = 5;
            // 
            // lblParameter1
            // 
            this.lblParameter1.Location = new System.Drawing.Point(57, 64);
            this.lblParameter1.Name = "lblParameter1";
            this.lblParameter1.Size = new System.Drawing.Size(92, 13);
            this.lblParameter1.TabIndex = 0;
            this.lblParameter1.Text = "Parameter 1 name.";
            // 
            // txtEditParameter1
            // 
            this.txtEditParameter1.Location = new System.Drawing.Point(57, 79);
            this.txtEditParameter1.Name = "txtEditParameter1";
            this.txtEditParameter1.Size = new System.Drawing.Size(482, 20);
            this.txtEditParameter1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(57, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Choose a report.";
            // 
            // xtraTabSettings
            // 
            this.xtraTabSettings.Controls.Add(this.separatorControl1);
            this.xtraTabSettings.Controls.Add(this.labelControl15);
            this.xtraTabSettings.Controls.Add(this.labelControl14);
            this.xtraTabSettings.Controls.Add(this.labelControl13);
            this.xtraTabSettings.Controls.Add(this.labelControl12);
            this.xtraTabSettings.Controls.Add(this.labelControl11);
            this.xtraTabSettings.Controls.Add(this.labelControl10);
            this.xtraTabSettings.Controls.Add(this.txtLiveConsumerSecret);
            this.xtraTabSettings.Controls.Add(this.txtLiveConsumerKey);
            this.xtraTabSettings.Controls.Add(this.txtLiveUrl);
            this.xtraTabSettings.Controls.Add(this.txtTestConsumerSecret);
            this.xtraTabSettings.Controls.Add(this.txtTestConsumerKey);
            this.xtraTabSettings.Controls.Add(this.txtTestUrl);
            this.xtraTabSettings.Name = "xtraTabSettings";
            this.xtraTabSettings.Size = new System.Drawing.Size(613, 297);
            this.xtraTabSettings.Text = "Settings.";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(46, 125);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(518, 23);
            this.separatorControl1.TabIndex = 26;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(46, 223);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(142, 13);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "Live system consumer secret.";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(46, 184);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(129, 13);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "Live system consumer key.";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(46, 145);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(75, 13);
            this.labelControl13.TabIndex = 0;
            this.labelControl13.Text = "Live system url.";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(46, 83);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(144, 13);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "Test system consumer secret.";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(46, 44);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(131, 13);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "Test system consumer key.";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(46, 5);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(77, 13);
            this.labelControl10.TabIndex = 0;
            this.labelControl10.Text = "Test system url.";
            // 
            // txtLiveConsumerSecret
            // 
            this.txtLiveConsumerSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLiveConsumerSecret.Location = new System.Drawing.Point(46, 239);
            this.txtLiveConsumerSecret.Name = "txtLiveConsumerSecret";
            this.txtLiveConsumerSecret.Size = new System.Drawing.Size(503, 20);
            this.txtLiveConsumerSecret.TabIndex = 6;
            // 
            // txtLiveConsumerKey
            // 
            this.txtLiveConsumerKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLiveConsumerKey.Location = new System.Drawing.Point(46, 200);
            this.txtLiveConsumerKey.Name = "txtLiveConsumerKey";
            this.txtLiveConsumerKey.Size = new System.Drawing.Size(503, 20);
            this.txtLiveConsumerKey.TabIndex = 5;
            // 
            // txtLiveUrl
            // 
            this.txtLiveUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLiveUrl.Location = new System.Drawing.Point(46, 161);
            this.txtLiveUrl.Name = "txtLiveUrl";
            this.txtLiveUrl.Size = new System.Drawing.Size(503, 20);
            this.txtLiveUrl.TabIndex = 4;
            // 
            // txtTestConsumerSecret
            // 
            this.txtTestConsumerSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestConsumerSecret.Location = new System.Drawing.Point(46, 99);
            this.txtTestConsumerSecret.Name = "txtTestConsumerSecret";
            this.txtTestConsumerSecret.Size = new System.Drawing.Size(503, 20);
            this.txtTestConsumerSecret.TabIndex = 3;
            // 
            // txtTestConsumerKey
            // 
            this.txtTestConsumerKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestConsumerKey.Location = new System.Drawing.Point(46, 60);
            this.txtTestConsumerKey.Name = "txtTestConsumerKey";
            this.txtTestConsumerKey.Size = new System.Drawing.Size(503, 20);
            this.txtTestConsumerKey.TabIndex = 2;
            // 
            // txtTestUrl
            // 
            this.txtTestUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestUrl.Location = new System.Drawing.Point(46, 21);
            this.txtTestUrl.Name = "txtTestUrl";
            this.txtTestUrl.Size = new System.Drawing.Size(503, 20);
            this.txtTestUrl.TabIndex = 1;
            // 
            // btnRunReport
            // 
            this.btnRunReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunReport.Location = new System.Drawing.Point(398, 461);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(150, 59);
            this.btnRunReport.TabIndex = 5;
            this.btnRunReport.Text = "Run Report";
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(45, 461);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 59);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkUseLive
            // 
            this.chkUseLive.Location = new System.Drawing.Point(243, 491);
            this.chkUseLive.Name = "chkUseLive";
            this.chkUseLive.Properties.Caption = "Use live system.";
            this.chkUseLive.Size = new System.Drawing.Size(105, 19);
            this.chkUseLive.TabIndex = 7;
            // 
            // lblSavePDFReports
            // 
            this.lblSavePDFReports.Location = new System.Drawing.Point(47, 396);
            this.lblSavePDFReports.Name = "lblSavePDFReports";
            this.lblSavePDFReports.Size = new System.Drawing.Size(110, 13);
            this.lblSavePDFReports.TabIndex = 30;
            this.lblSavePDFReports.Text = "Save pdf reports here.";
            // 
            // btnEditSavePDFReports
            // 
            this.btnEditSavePDFReports.Location = new System.Drawing.Point(45, 415);
            this.btnEditSavePDFReports.Name = "btnEditSavePDFReports";
            this.btnEditSavePDFReports.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditSavePDFReports.Size = new System.Drawing.Size(503, 20);
            this.btnEditSavePDFReports.TabIndex = 29;
            this.btnEditSavePDFReports.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditSavePDFReports_ButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 613);
            this.Controls.Add(this.lblSavePDFReports);
            this.Controls.Add(this.btnEditSavePDFReports);
            this.Controls.Add(this.chkUseLive);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRunReport);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "Form1";
            this.Text = "PayRun.IO run reports.";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChooseReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabMainForm.ResumeLayout(false);
            this.xtraTabMainForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxChooseFrequency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditParameter1.Properties)).EndInit();
            this.xtraTabSettings.ResumeLayout(false);
            this.xtraTabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveConsumerSecret.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveConsumerKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLiveUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestConsumerSecret.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestConsumerKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseLive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditSavePDFReports.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxChooseReport;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabMainForm;
        private DevExpress.XtraTab.XtraTabPage xtraTabSettings;
        private DevExpress.XtraEditors.LabelControl lblParameter1;
        private DevExpress.XtraEditors.TextEdit txtEditParameter1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblParameter2;
        private DevExpress.XtraEditors.TextEdit txtEditParameter3;
        private DevExpress.XtraEditors.LabelControl lblParameter3;
        private DevExpress.XtraEditors.TextEdit txtEditParameter2;
        private DevExpress.XtraEditors.LabelControl lblParameter4;
        private DevExpress.XtraEditors.TextEdit txtEditParameter4;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtLiveConsumerSecret;
        private DevExpress.XtraEditors.TextEdit txtLiveConsumerKey;
        private DevExpress.XtraEditors.TextEdit txtLiveUrl;
        private DevExpress.XtraEditors.TextEdit txtTestConsumerSecret;
        private DevExpress.XtraEditors.TextEdit txtTestConsumerKey;
        private DevExpress.XtraEditors.TextEdit txtTestUrl;
        private DevExpress.XtraEditors.SimpleButton btnRunReport;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.CheckEdit chkUseLive;
        private DevExpress.XtraEditors.LabelControl lblParameter6;
        private DevExpress.XtraEditors.TextEdit txtEditParameter6;
        private DevExpress.XtraEditors.LabelControl lblParameter5;
        private DevExpress.XtraEditors.TextEdit txtEditParameter5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraEditors.LabelControl lblSavePDFReports;
        private DevExpress.XtraEditors.ButtonEdit btnEditSavePDFReports;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxChooseFrequency;
        private DevExpress.XtraEditors.DateEdit dateEndDate;
        private DevExpress.XtraEditors.DateEdit dateStartDate;
    }
}

