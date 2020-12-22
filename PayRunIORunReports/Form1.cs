using System;
using System.Xml;
using PayRunIO.CSharp.SDK;
using DevExpress.XtraReports.UI;
using PayRunIOClassLibrary;
using System.Windows.Forms;
using System.IO;

namespace PayRunIORunReports
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            txtTestUrl.Text = Properties.Settings.Default.TestUrl;
            txtTestConsumerKey.Text = Properties.Settings.Default.TestConsumerKey;
            txtTestConsumerSecret.Text = Properties.Settings.Default.TestConsumerSecret;
            txtLiveUrl.Text = Properties.Settings.Default.LiveUrl;
            txtLiveConsumerKey.Text = Properties.Settings.Default.LiveConsumerKey;
            txtLiveConsumerSecret.Text = Properties.Settings.Default.LiveConsumerSecret;
            txtEditParameter1.Visible = false;
            txtEditParameter2.Visible = false;
            txtEditParameter3.Visible = false;
            txtEditParameter4.Visible = false;
            txtEditParameter5.Visible = false;
            txtEditParameter6.Visible = false;
            lblParameter1.Visible = false;
            lblParameter2.Visible = false;
            lblParameter3.Visible = false;
            lblParameter4.Visible = false;
            lblParameter5.Visible = false;
            lblParameter6.Visible = false;
            comboBoxChooseFrequency.Visible = false;
            dateStartDate.Visible = false;
            dateEndDate.Visible = false;
            btnEditSavePDFReports.Text = Properties.Settings.Default.pdfReportFolder;
        }
        private RestApiHelper ApiHelper()
        {
            bool live = Convert.ToBoolean(chkUseLive.EditValue);
            string consumerKey;
            string consumerSecret;
            string url;
            if (live)
            {
                consumerKey = txtLiveConsumerKey.Text;
                consumerSecret = txtLiveConsumerSecret.Text;
                url = txtLiveUrl.Text;
            }
            else
            {
                consumerKey = txtTestConsumerKey.Text;
                consumerSecret = txtTestConsumerSecret.Text;
                url = txtTestUrl.Text;
            }
           
            
            RestApiHelper apiHelper = new RestApiHelper(
                    new PayRunIO.OAuth1.OAuthSignatureGenerator(),
                    consumerKey,
                    consumerSecret,
                    url,
                    "application/xml",
                    "application/xml");
            return apiHelper;
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            bool allEntered = CheckAllFieldsAreEntered();
            
            
            if(allEntered)
            {
                ProduceReport();
                MessageBox.Show("Report created successfully.");
            }
            else
            {
                MessageBox.Show("Please enter all required fields.");
            }
            
        }
        private bool CheckAllFieldsAreEntered()
        {
            bool allEntered = true;

            if (comboBoxChooseReport.SelectedText == "")
            {
                allEntered = false;
            }
            else
            {
                if (comboBoxChooseReport.SelectedText == "Combined Payroll Run Report" ||
                    comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report" ||
                    comboBoxChooseReport.SelectedText == "Pension Contributions To Date Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || dateEndDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
                else if(comboBoxChooseReport.Text == "Note And Coin Requirement Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
                else if (comboBoxChooseReport.Text == "PAPDIS Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || txtEditParameter5.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
            }

            return allEntered;
        }
        private void ProduceReport()
        {
            bool reportTypeXml;
            string startDate = dateStartDate.DateTime.ToString("yyyy-MM-dd");
            string endDate = dateEndDate.DateTime.ToString("yyyy-MM-dd");
            string sortByBranch = "False";
            string prm1 = null;
            string prm2 = null;
            string prm3 = null;
            string prm4 = null;
            string prm5 = null;
            string prm6 = null;
            string rptRef = null;
            string url = null;
            if (comboBoxChooseReport.Text == "Combined Payroll Run Report" || 
                comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
            {
                //PSCRN1 - Combined Payroll Run Report or Department Within Branch Payroll Run Details Report
                //I'm using the same PayRun.IO report for these 2 reports. I just need to sort the department within branch report differently.
                reportTypeXml = true;
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "StartDate";
                prm4 = "EndDate";
                prm5 = "SortByBranch";
                rptRef = "PSCRN1";

                if (comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
                {
                    sortByBranch = "True";
                }
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer number
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + startDate + "&"                                  //Start date
                    + prm4 + "=" + endDate + "&"                                    //End date
                    + prm5 + "=" + sortByBranch;                                    //Sort by branch
            }
            else if (comboBoxChooseReport.Text == "Note And Coin Requirement Report")
            {
                //PSCOIN2 - Note And Coin Requirement Report
                reportTypeXml = true;
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "PaymentDate";
                rptRef = "PSCOIN2";
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + startDate;                                       //Payment date
            }
            else if (comboBoxChooseReport.Text == "Pension Contributions To Date Report")
            {
                //PSPEN2 - Pension Contributions To Date Report"
                reportTypeXml = true;
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "StartDate";
                prm4 = "EndDate";
                rptRef = "PSPEN2";
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + startDate + "&"                                  //Start date
                    + prm4 + "=" + endDate;                                         //End date
            }
            else //(comboBoxChooseReport.Text == "PAPDIS Report")
            {
                //PAPDIS report
                reportTypeXml = false;
                prm1 = "EmployerKey";                                               
                prm2 = "PayScheduleKey";
                prm3 = "TaxYear";
                prm4 = "PaymentDate";
                prm5 = "PensionKey";                                                
                prm6 = "TransformDefinitionKey";                                    
                rptRef = "PAPDIS";
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + txtEditParameter4.Text + "&"                     //Tax Year
                    + prm4 + "=" + startDate + "&"                                  //Payment date
                    + prm5 + "=" + txtEditParameter5.Text + "&"                     //Pension key
                    + prm6 + "=" + "PAPDIS-CSV";                                    //Transform definition key
            }

            if (reportTypeXml)
            {
                //xml report
                XmlDocument xmlReport = null;

                xmlReport = GetXmlReport(rptRef, url);

                CreatePDFReports(xmlReport);
            }
            else
            {
                //text report
                string txtReport = null;
                txtReport = GetTxtReport(rptRef, url);

                CreateTxtReports(txtReport);
            }
            
            
            


        }
        private void CreatePDFReports(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            string reportName = null;
            string assemblyName= "PayRunIOClassLibrary";
            XtraReport xtraReport = new XtraReport();

            if (comboBoxChooseReport.Text == "Combined Payroll Run Report")
            {
                
                reportName = "CombinedPayrollRunReport";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                reportName = "CombinedPayrollRunReport";
            }
            else if (comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
            {
                reportName = "CombinedPayrollRunReport";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                reportName = "DepartmentWithinBranchPayrollRunDetailsReport";
            }
            else if (comboBoxChooseReport.Text == "Note And Coin Requirement Report")
            {
                reportName = "NoteAndCoinRequirementReport";
                assemblyName = "PayRunIOClassLibrary";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
            }
            else if (comboBoxChooseReport.Text == "Pension Contributions To Date Report")
            {
                reportName = "PensionContributionsToDateReport";
                assemblyName = "PayRunIOClassLibrary";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
            }
            string docName = btnEditSavePDFReports.Text + "//" + txtEditParameter1.Text + "_" + reportName + ".pdf";
            SavePDFReport(xtraReport, docName);
        }
        private void CreateTxtReports(string txtReport)
        {
            string reportName = null;
            
            if (comboBoxChooseReport.Text == "PAPDIS Report")
            {
                reportName = "PAPDISReport";
            }
           string docName = btnEditSavePDFReports.Text + "\\" + txtEditParameter1.Text + "_" + reportName + ".csv";
           SaveTxtReport(txtReport, docName);
        }
        private XmlDocument GetXmlReport(string rptRef, string url)
        {
            XmlDocument xmlReport = new XmlDocument();

            if (url != null)
            {
                var apiHelper = ApiHelper();
                try
                {
                    xmlReport = apiHelper.GetRawXml("/Report/" + rptRef + "/run?" + url);

                }
                catch (Exception ex)
                {

                }
            }

            return xmlReport;
        }
        private string GetTxtReport(string rptRef, string url)
        {
           string txtReport = null;

            if (url != null)
            {
                var apiHelper = ApiHelper();
                try
                {
                    txtReport = apiHelper.GetRawText("/Report/" + rptRef + "/run?" + url);

                }
                catch (Exception ex)
                {

                }
            }

            return txtReport;
        }
        private void SavePDFReport(XtraReport xtraReport, string docName)
        {
            // To show the report designer. You need to uncomment this to design the report.
            bool designMode = false;
            if (designMode)
            {
                xtraReport.ShowDesigner();
                //report1.ShowPreview();

            }
            else
            {
                //Save the report in a pdf format
                xtraReport.ExportToPdf(docName);

            }
        }
        private void SaveTxtReport(string txtReport, string docName)
        {
            // Save the text report.
            File.WriteAllText(docName, txtReport);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxChooseReport_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBoxChooseReport.SelectedText=="Combined Payroll Run Report" || 
               comboBoxChooseReport.SelectedText == "Department Within Branch Payroll Run Details Report")
            {
                //I'll these from the database
                txtEditParameter1.Visible = true;
                comboBoxChooseFrequency.Visible = true;
                dateStartDate.Visible = true;
                dateEndDate.Visible = true;
                lblParameter1.Text = "Enter employer Number in the form nnnn.";
                lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                lblParameter3.Text = "Enter start date.";
                lblParameter4.Text = "Enter end date.";
                lblParameter1.Visible = true;
                lblParameter2.Visible = true;
                lblParameter3.Visible = true;
                lblParameter4.Visible = true;
                
            }
            else if (comboBoxChooseReport.SelectedText == "Note And Coin Requirement Report")
            {
                txtEditParameter1.Visible = true;
                comboBoxChooseFrequency.Visible = true;
                dateStartDate.Visible = true;
                lblParameter1.Text = "Enter employer Number in the form nnnn.";
                lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                lblParameter3.Text = "Enter payment date.";
                lblParameter1.Visible = true;
                lblParameter2.Visible = true;
                lblParameter3.Visible = true;
                
            }
            else if (comboBoxChooseReport.SelectedText == "Pension Contributions To Date Report")
            {
                txtEditParameter1.Visible = true;
                comboBoxChooseFrequency.Visible = true;
                dateStartDate.Visible = true;
                dateEndDate.Visible = true;
                lblParameter1.Text = "Enter employer Number in the form nnnn.";
                lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                lblParameter3.Text = "Enter start date.";
                lblParameter4.Text = "Enter end date.";
                lblParameter1.Visible = true;
                lblParameter2.Visible = true;
                lblParameter3.Visible = true;
                lblParameter4.Visible = true;

            }
            else if (comboBoxChooseReport.SelectedText == "PAPDIS Report")
            {
               txtEditParameter1.Visible = true;
                comboBoxChooseFrequency.Visible = true;
                dateStartDate.Visible = true;
                txtEditParameter4.Visible = true;
                txtEditParameter5.Visible = true;
                lblParameter1.Text = "Enter employer Number in the form nnnn.";
                lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                lblParameter3.Text = "Enter payment date.";
                lblParameter4.Text = "Enter tax year.";
                lblParameter5.Text = "Enter pension key.";
                lblParameter1.Visible = true;
                lblParameter2.Visible = true;
                lblParameter3.Visible = true;
                lblParameter4.Visible = true;
                lblParameter5.Visible = true;
            }
        }
        
        private void btnEditSavePDFReports_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            btnEditSavePDFReports.Text = folder.SelectedPath;
            if (btnEditSavePDFReports.Text != Properties.Settings.Default.pdfReportFolder)
            {
                Properties.Settings.Default.pdfReportFolder = btnEditSavePDFReports.Text;
                Properties.Settings.Default.Save();
            }
        }

        
    }
}
