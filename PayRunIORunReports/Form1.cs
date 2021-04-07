using System;
using System.Xml;
using PayRunIO.CSharp.SDK;
using DevExpress.XtraReports.UI;
using PayRunIOClassLibrary;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

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
            btnEditSavePDFReports.Text = Properties.Settings.Default.pdfReportFolder;
            HideControls();
        }
        private void HideControls()
        {
            txtEditParameter1.Visible = false;
            txtEditParameter2.Visible = false;
            txtEditParameter3.Visible = false;
            txtEditParameter4.Visible = false;
            txtEditParameter5.Visible = false;
            txtEditParameter6.Visible = false;
            chkBoxSorted.Visible = false;
            chkBoxSummary.Visible = false;
            lblParameter1.Visible = false;
            lblParameter2.Visible = false;
            lblParameter3.Visible = false;
            lblParameter4.Visible = false;
            lblParameter5.Visible = false;
            lblParameter6.Visible = false;
            comboBoxChooseFrequency.Visible = false;
            dateStartDate.Visible = false;
            dateEndDate.Visible = false;
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


            if (allEntered)
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
            switch(comboBoxChooseReport.SelectedText)
            {
                case "":
                    allEntered = false;
                    break;
                //Employer number, frequency, start date, end date
                case "Combined Payroll Run Report":
                case "Department Within Branch Payroll Run Details Report":
                case "Pay Component Exception Report":
                case "Pension Contributions To Date Report":
                case "Statutory Absence Report":
                case "Statutory Sick Pay Paid Report":
                case "Pre Report":
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || dateEndDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                    break;
                //Employer number, frequency, payment date
                case "Note And Coin Requirement Report":
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                    break;
                //Employer number, frequency, payment date, tax year, pension key
                case "PAPDIS Report":
                case "Royal London Pension Report":
                case "Now Pension Report":
                case "Legal & General Pension Report":
                case "Aegon Pension Report":
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || txtEditParameter4.Text == "" || txtEditParameter5.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                    break;
                //Employer number, frequency, tax year
                case "P11 Substitute":
                case "Apprenticeship Levy Report":
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || txtEditParameter3.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                    break;
                //Tax year, tax month
                case "EPS Report":
                    if (txtEditParameter1.Text == "" || txtEditParameter2.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                    break;
            }
            return allEntered;
        }
        private void ProduceReport()
        {
            string reportType;
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
            switch(comboBoxChooseReport.Text)
            {
                case "Combined Payroll Run Report":
                case "Department Within Branch Payroll Run Details Report":
                    //PSCRN1 - Combined Payroll Run Report or Department Within Branch Payroll Run Details Report
                    //I'm using the same PayRun.IO report for these 2 reports. I just need to sort the department within branch report differently.
                    reportType = "xml";
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
                    break;
                case "Note And Coin Requirement Report":
                    //PSCOIN2 - Note And Coin Requirement Report
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "PaymentDate";
                    rptRef = "PSCOIN2";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate;                                       //Payment date
                    break;
                case "Pay Component Exception Report":
                    //PSEXCPC - Pay Component Exception Report"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "PaymentDate1";
                    prm4 = "PaymentDate2";
                    prm5 = "SortByBranch";
                    prm6 = "SummaryOnly";
                    rptRef = "PSEXCPC";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate + "&"                                  //Payment date 1
                        + prm4 + "=" + endDate + "&"                                    //Payment date 2
                        + prm5 + "=" + chkBoxSorted.Checked + "&"                       //Sorted By Branch
                        + prm6 + "=" + chkBoxSummary.Checked;                           //Summary only.
                    break;
                case "Pension Contributions To Date Report":
                    //PSPEN2 - Pension Contributions To Date Report"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "StartDate";
                    prm4 = "EndDate";
                    rptRef = "PSPEN2";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate + "&"                                  //Start date
                        + prm4 + "=" + endDate;                                         //End date
                    break;
                case "Statutory Absence Report":
                    //PSSPAMS - Statutory Absence Report"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "StartDate";
                    prm4 = "EndDate";
                    rptRef = "PSSPAMS";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate + "&"                                  //Start date
                        + prm4 + "=" + endDate;                                         //End date
                    break;
                case "Statutory Sick Pay Paid Report":
                    //PSSSPRUN - Statutory Absence Report"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "StartDate";
                    prm4 = "EndDate";
                    rptRef = "PSSSPRUN";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate + "&"                                  //Start date
                        + prm4 + "=" + endDate;                                         //End date
                    break;
                case "Pre Report":
                    //PSPRE - Pre Report"
                    reportType = "xlsx";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "StartDate";
                    prm4 = "EndDate";
                    rptRef = "PSPRE";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + startDate + "&"                                  //Start date
                        + prm4 + "=" + endDate;                                         //End date
                    break;
                case "P11 Substitute":
                    //PSP11 - P11 Substitute"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "TaxYear";
                    rptRef = "PSP11";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + txtEditParameter3.Text;                          //Tax Year
                    break;
                case "EPS Report":
                    //PSEPS - EPS Report"
                    reportType = "xml";
                    prm1 = "TaxYear";
                    prm2 = "TaxMonth";
                    rptRef = "PSEPS";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Tax Year
                        + prm2 + "=" + txtEditParameter2.Text;                          //Tax Month
                    break;
                case "Apprenticeship Levy Report":
                    //APPLEVYANNUAL - Apprenticeship Levy Report"
                    reportType = "xml";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "TaxYear";
                    rptRef = "PE-AppLevyAnnual";
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + txtEditParameter3.Text;                          //Tax Year
                    break;
                default:
                    //Royal London Pension Report
                    //Now Pension Report
                    //Legal & General Pension Report
                    //Aegon Pension Report
                    //PAPDIS report
                    reportType = "txt";
                    prm1 = "EmployerKey";
                    prm2 = "PayScheduleKey";
                    prm3 = "TaxYear";
                    prm4 = "PaymentDate";
                    prm5 = "PensionKey";
                    prm6 = "TransformDefinitionKey";
                    rptRef = "PAPDIS";
                    string transformKey;
                    switch (comboBoxChooseReport.Text)
                    {
                        case "Royal London Pension Report":
                            transformKey = "RL-PENSION-CSV";
                            break;
                        case "Now Pension Report":
                            transformKey = "RL-PENSION-CSV"; //TODO change this to NOW-PENSION-CSV or whatever Tim calls it.
                            break;
                        case "Legal & General Pension Report":
                            transformKey = "RL-PENSION-CSV"; //TODO change this to LG-PENSION-CSV or whatever Tim calls it.
                            break;
                        case "Aegon Pension Report":
                            transformKey = "RL-PENSION-CSV"; //TODO change this to AE-PENSION-CSV or whatever Tim calls it.
                            break;
                        default:
                            //PAPDIS report for Smart and maybe others.
                            transformKey = "PAPDIS-CSV";
                            break;
                    }
                    url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                        + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                        + prm3 + "=" + txtEditParameter4.Text + "&"                     //Tax Year
                        + prm4 + "=" + startDate + "&"                                  //Payment date
                        + prm5 + "=" + txtEditParameter5.Text + "&"                     //Pension key
                        + prm6 + "=" + transformKey;                                    //Transform definition key
                    break;
            }
            XmlDocument xmlReport;
            string txtReport;
            switch (reportType)
            {
                case "xml":
                    //xml report
                    xmlReport = GetXmlReport(rptRef, url);
                    CreatePDFReports(xmlReport);
                    break;
                case "xlsx":
                    //xml report
                    xmlReport = GetXmlReport(rptRef, url);
                    CreateXlsxReports(xmlReport);
                    break;
                default:
                    //text report
                    txtReport = GetTxtReport(rptRef, url);
                    CreateTxtReports(txtReport);
                    break;
            }
            
        }
        private void CreatePDFReports(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            string reportName = null;
            string assemblyName = "PayRunIOClassLibrary";
            XtraReport xtraReport = new XtraReport();

            switch(comboBoxChooseReport.Text)
            {
                case "Combined Payroll Run Report":
                    reportName = "CombinedPayrollRunReport";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    reportName = "CombinedPayrollRunReport";
                    break;
                case "Department Within Branch Payroll Run Details Report":
                    reportName = "CombinedPayrollRunReport";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    reportName = "DepartmentWithinBranchPayrollRunDetailsReport";
                    break;
                case "Note And Coin Requirement Report":
                    reportName = "NoteAndCoinRequirementReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "Pay Component Exception Report":
                    reportName = "PayComponentExceptionReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "Pension Contributions To Date Report":
                    reportName = "PensionContributionsToDateReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "P11 Substitute":
                    reportName = "P11Substitute";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "EPS Report":
                    reportName = "EPSReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "Apprenticeship Levy Report":
                    reportName = "ApprenticeshipLevyReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "Statutory Absence Report":
                    reportName = "StatutoryAbsenceReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                case "Statutory Sick Pay Paid Report":
                    reportName = "StatutorySickPayPaidReport";
                    assemblyName = "PayRunIOClassLibrary";
                    xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
                    break;
                default:
                    break;
            }

            string docName = btnEditSavePDFReports.Text + "\\" + txtEditParameter1.Text + "_" + reportName + ".pdf";
            SavePDFReport(xtraReport, docName);
        }
        private void CreateXlsxReports(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            //Get employer number.
            string coNo = prWG.GetEmployerNumber(xmlReport);
            //Create the Excel workbook
            string outgoingFolder = btnEditSavePDFReports.Text;
            string startDate = dateStartDate.Text.Replace('/', '.');
            string endDate = dateEndDate.Text.Replace('/', '.');
            string workBookName = outgoingFolder + "\\" + coNo + "_PreReport(" + startDate + "-" + endDate + ").xlsx";

            PicoXLSX.Workbook workbook = prWG.CreatePreReportWorkbook(xmlReport, workBookName);

            workbook.Save();
            
        }
        private void CreateTxtReports(string txtReport)
        {
            string reportName;

            switch(comboBoxChooseReport.Text)
            {
                case "Royal London Pension Report":
                    reportName = "RoyalLondonPensionReport";
                    break;
                case "Now Pension Report":
                    reportName = "NowPensionReport";
                    break;
                case "Legal & General Pension Report":
                    reportName = "Legal&GeneralPensionReport";
                    break;
                case "Aegon Pension Report":
                    reportName = "AegonPensionReport";
                    break;
                default:
                    //PAPDIS report for Smart and maybe others.
                    reportName = "PAPDISReport";
                    break;
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
                //EPS report in Excel format
                if(comboBoxChooseReport.Text == "EPS Report")
                {
                    docName = docName.Replace("pdf", "xlsx");
                    xtraReport.ExportToXlsx(docName);
                }

            }
        }
        private void SaveTxtReport(string txtReport, string docName)
        {
            // Save the text report.
            File.WriteAllText(docName, txtReport);
        }
        private void SaveXlsxReport(string txtReport, string docName)
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
            HideControls();

            switch(comboBoxChooseReport.SelectedText)
            {
                case "Combined Payroll Run Report":
                case "Department Within Branch Payroll Run Details Report":
                case "Pension Contributions To Date Report":
                case "Statutory Absence Report":
                case "Statutory Sick Pay Paid Report":
                case "Pre Report":
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
                    break;
                case "Pay Component Exception Report":
                    txtEditParameter1.Visible = true;
                    comboBoxChooseFrequency.Visible = true;
                    dateStartDate.Visible = true;
                    dateEndDate.Visible = true;
                    chkBoxSorted.Visible = true;
                    chkBoxSummary.Visible = true;
                    lblParameter1.Text = "Enter employer Number in the form nnnn.";
                    lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                    lblParameter3.Text = "Enter first payment date.";
                    lblParameter4.Text = "Enter second payment date.";
                    lblParameter5.Text = "Sort by branch.";
                    lblParameter6.Text = "Summary only.";
                    lblParameter1.Visible = true;
                    lblParameter2.Visible = true;
                    lblParameter3.Visible = true;
                    lblParameter4.Visible = true;
                    break;
                case "Note And Coin Requirement Report":
                    txtEditParameter1.Visible = true;
                    comboBoxChooseFrequency.Visible = true;
                    dateStartDate.Visible = true;
                    lblParameter1.Text = "Enter employer Number in the form nnnn.";
                    lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                    lblParameter3.Text = "Enter payment date.";
                    lblParameter1.Visible = true;
                    lblParameter2.Visible = true;
                    lblParameter3.Visible = true;
                    break;
                case "P11 Substitute":
                case "Apprenticeship Levy Report":
                    txtEditParameter1.Visible = true;
                    comboBoxChooseFrequency.Visible = true;
                    txtEditParameter3.Visible = true;
                    lblParameter1.Text = "Enter employer Number in the form nnnn.";
                    lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                    lblParameter3.Text = "Enter tax year.";
                    lblParameter1.Visible = true;
                    lblParameter2.Visible = true;
                    lblParameter3.Visible = true;
                    break;
                case "EPS Report":
                    txtEditParameter1.Visible = true;
                    txtEditParameter2.Visible = true;
                    lblParameter1.Text = "Enter tax year.";
                    lblParameter2.Text = "Enter tax month.";
                    lblParameter1.Visible = true;
                    lblParameter2.Visible = true;
                    break;
                case "PAPDIS Report":
                case "Royal London Pension Report":
                case "Now Pension Report":
                case "Legal & General Pension Report":
                case "Aegon Pension Report":
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
                    break;
                default:
                    break;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Properties.Settings.Default.TestUrl != txtTestUrl.Text ||
               Properties.Settings.Default.TestConsumerKey != txtTestConsumerKey.Text ||
               Properties.Settings.Default.TestConsumerSecret != txtTestConsumerSecret.Text ||
               Properties.Settings.Default.LiveUrl != txtLiveUrl.Text ||
               Properties.Settings.Default.LiveConsumerKey != txtLiveConsumerKey.Text ||
               Properties.Settings.Default.LiveConsumerSecret != txtLiveConsumerSecret.Text ||
               Properties.Settings.Default.pdfReportFolder != btnEditSavePDFReports.Text)
            {
                Properties.Settings.Default.TestUrl = txtTestUrl.Text;
                Properties.Settings.Default.TestConsumerKey = txtTestConsumerKey.Text;
                Properties.Settings.Default.TestConsumerSecret = txtTestConsumerSecret.Text;
                Properties.Settings.Default.LiveUrl = txtLiveUrl.Text;
                Properties.Settings.Default.LiveConsumerKey = txtLiveConsumerKey.Text;
                Properties.Settings.Default.LiveConsumerSecret = txtLiveConsumerSecret.Text;
                Properties.Settings.Default.pdfReportFolder = btnEditSavePDFReports.Text;
                Properties.Settings.Default.Save();
            }
            
        }
    }
    
}
