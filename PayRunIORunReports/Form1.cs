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

            if (comboBoxChooseReport.SelectedText == "")
            {
                allEntered = false;
            }
            else
            {
                //Employer number, frequency, start date, end date
                if (comboBoxChooseReport.SelectedText == "Combined Payroll Run Report" ||
                    comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report" ||
                    comboBoxChooseReport.SelectedText == "Pension Contributions To Date Report" ||
                    comboBoxChooseReport.SelectedText == "Pre Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || dateEndDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
                //Employer number, frequency, payment date
                else if (comboBoxChooseReport.Text == "Note And Coin Requirement Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
                //Employer number, frequency, payment date, tax year, pension key
                else if (comboBoxChooseReport.Text == "PAPDIS Report" ||
                         comboBoxChooseReport.Text == "Royal London Pension Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || dateStartDate.Text == "" || txtEditParameter4.Text == "" || txtEditParameter5.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
                //Employer number, frequency, tax year
                else if (comboBoxChooseReport.Text == "P11 Substitute" || comboBoxChooseReport.Text == "Apprenticeship Levy Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" || txtEditParameter3.Text == "" || btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
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
            if (comboBoxChooseReport.Text == "Combined Payroll Run Report" ||
                comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
            {
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
            }
            else if (comboBoxChooseReport.Text == "Note And Coin Requirement Report")
            {
                //PSCOIN2 - Note And Coin Requirement Report
                reportType = "xml";
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
            }
            else if (comboBoxChooseReport.Text == "Pre Report")
            {
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
            }
            else if (comboBoxChooseReport.Text == "P11 Substitute")
            {
                //PSP11 - P11 Substitute"
                reportType = "xml";
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "TaxYear";
                rptRef = "PSP11";
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + txtEditParameter3.Text;                          //Tax Year
            }
            else if (comboBoxChooseReport.Text == "Apprenticeship Levy Report")
            {
                //APPLEVYANNUAL - Apprenticeship Levy Report"
                reportType = "xml";
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "TaxYear";
                rptRef = "Jim-AppLevyAnnual";
                url = prm1 + "=" + txtEditParameter1.Text + "&"                     //Employer
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"               //Pay schedule
                    + prm3 + "=" + txtEditParameter3.Text;                          //Tax Year
            }
            else if(comboBoxChooseReport.Text == "PAPDIS Report")
            {
                //PAPDIS report
                reportType = "txt";
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
            else //(comboBoxChooseReport.Text == "Royal London Pension Report")
            {
                //PAPDIS report
                reportType = "txt";
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
                    + prm6 + "=" + "RL-PENSION-CSV";                                //Transform definition key
            }

            if (reportType == "xml")
            {
                //xml report
                XmlDocument xmlReport = null;

                xmlReport = GetXmlReport(rptRef, url);

                CreatePDFReports(xmlReport);
            }
            if (reportType == "xlsx")
            {
                //xml report
                XmlDocument xmlReport = null;

                xmlReport = GetXmlReport(rptRef, url);

                CreateXlsxReports(xmlReport);
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
            string assemblyName = "PayRunIOClassLibrary";
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
            else if (comboBoxChooseReport.Text == "P11 Substitute")
            {
                reportName = "P11Substitute";
                assemblyName = "PayRunIOClassLibrary";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
            }
            else if (comboBoxChooseReport.Text == "Apprenticeship Levy Report")
            {
                reportName = "ApprenticeshipLevyReport";
                assemblyName = "PayRunIOClassLibrary";
                xtraReport = prWG.CreatePDFReport(xmlReport, reportName, assemblyName);
            }
            string docName = btnEditSavePDFReports.Text + "//" + txtEditParameter1.Text + "_" + reportName + ".pdf";
            SavePDFReport(xtraReport, docName);
        }
        private void CreateXlsxReports(XmlDocument xmlReport)
        {
            //Get employer number.
            string coNo = GetEmployerNumber(xmlReport);
            //Create the Excel workbook
            string outgoingFolder = btnEditSavePDFReports.Text;
            string startDate = dateStartDate.Text.Replace('/', '.');
            string endDate = dateEndDate.Text.Replace('/', '.');
            string workBookName = outgoingFolder + "\\" + coNo + "_PreReport(" + startDate + "-" + endDate + ").xlsx";
            PicoXLSX.Workbook workbook = new PicoXLSX.Workbook(workBookName, "Pre");

            if (comboBoxChooseReport.Text == "Pre Report")
            {
               //Will need to return the xlsx file
                workbook = PreparePreReport(xmlReport, workbook);
            }

            workbook.Save();
        }
        private PicoXLSX.Workbook PreparePreReport(XmlDocument xmlReport, PicoXLSX.Workbook workbook)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();

            //Get employer number.
            string coNo = GetEmployerNumber(xmlReport);
            //Create a list of pay codes that are in use.
            List<RPPreSamplePayCode> rpPreSamplePayCodes = CreateListOfRequiredColumns(xmlReport);
            //Create a list of the fixed columns required.
            List<string> fixCol = prWG.CreateListOfFixedColumns();
            //Create a list of the variable columns required.
            List<string> varCol = prWG.CreateListOfVariableColumns(rpPreSamplePayCodes);
            //Create a list of employee period object within each pay run.
            List<RPEmployeePeriod> rpEmployeePeriods = CreateListOfEmployeePeriods(xmlReport);
            
            //Create a workbook.
            workbook = CreatePreXLSX(rpEmployeePeriods, coNo, rpPreSamplePayCodes, fixCol, varCol, workbook);
            
            return workbook;
        }
        private PicoXLSX.Workbook CreatePreXLSX(List<RPEmployeePeriod> rpEmployeePeriodList,
                                       string coNo, List<RPPreSamplePayCode> rpPreSamplePayCodes,
                                       List<string> fixCol, List<string> varCol,
                                       PicoXLSX.Workbook workbook)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            string outgoingFolder = btnEditSavePDFReports.Text;
            string startDate = dateStartDate.Text.Replace('/', '.');
            string endDate = dateEndDate.Text.Replace('/', '.');
            string workBookName = outgoingFolder + "\\" + coNo + "_PreReport(" + startDate + "-" + endDate + ").xlsx";

            //Add the fixed headings
            foreach (string col in fixCol)
            {
                workbook.CurrentWorksheet.AddNextCell(col, PicoXLSX.Style.BasicStyles.Bold);
                
            }
            //Add the variable headings
            foreach (string col in varCol)
            {
                workbook.CurrentWorksheet.AddNextCell(col, PicoXLSX.Style.BasicStyles.Bold);
            }

            //Now for each employee create a row and add in the values for each column
            foreach (RPEmployeePeriod rpEmployeePeriod in rpEmployeePeriodList)
            {
                workbook.CurrentWorksheet.GoToNextRow();

                workbook = prWG.CreateFixedWorkbookColumns(workbook, rpEmployeePeriod);
                workbook = prWG.CreateVariableWorkbookColumns(workbook, rpEmployeePeriod, varCol);

            }
            //Try adding a formula
            workbook.CurrentWorksheet.GoToNextRow();
            workbook.CurrentWorksheet.GoToNextRow();

            workbook.CurrentWorksheet.AddNextCell("Totals", PicoXLSX.Style.BasicStyles.ColorizedText("990000"));

            //From Reference column to NILetter column
            for (int i = 0; i < 8; i++)
            {
                workbook.CurrentWorksheet.AddNextCell("");
            }
            //The first 9 columns are text and cannot be summed. 
            //The can be summed using a formula in the form =SUM(J2:J61). Column J is column 10 and is the first summable column.
            int rows = rpEmployeePeriodList.Count + 1;
            int cols = fixCol.Count + varCol.Count - 9;
            for (int i = 10; i < cols + 10; i++)
            {
                string colName = GetExcelColumnName(i);
                string formula = "=SUM(" + colName + "2:" + colName + rows + ")";
                workbook.WS.Formula(formula, PicoXLSX.Style.BasicStyles.ColorizedText("990000"));
            }

            return workbook;
            
        }
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        private string GetEmployerNumber(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            string coNo = null;
            foreach(XmlElement parameters in xmlReport.GetElementsByTagName("Parameters"))
            {
                coNo = prWG.GetElementByTagFromXml(parameters, "EmployerCode");
            }
            return coNo;
        }
        private List<RPPreSamplePayCode> CreateListOfRequiredColumns(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            RPPreSamplePayCode rpPreSamplePayCode;
            //Create a list of all possible pay codes. For this purpose pensions can be turned into pay codes.
            List<RPPreSamplePayCode> rpPreSamplePayCodes = new List<RPPreSamplePayCode>();
            
            //There could be multiple payruns in this xml file.
            foreach (XmlElement payRun in xmlReport.GetElementsByTagName("PayRun"))
            {
                DateTime payRunDate = Convert.ToDateTime(prWG.GetDateElementByTagFromXml(payRun,"PayRunDate"));
                //There could be multiple employees in each pay run.
                foreach (XmlElement employee in payRun.GetElementsByTagName("Employee"))
                {
                    //There could be multiple pensions in each employee.
                    foreach (XmlElement pension in employee.GetElementsByTagName("Pension"))
                    {
                        string eeCode = prWG.GetElementByTagFromXml(pension, "Code") +
                                   prWG.GetElementByTagFromXml(pension, "ProviderName");
                        string eeDesc = prWG.GetElementByTagFromXml(pension, "SchemeName");
                        string erCode = eeCode + "(Er)";
                        string erDesc = eeDesc + "(Er)";
                        //Add the Er pension
                        rpPreSamplePayCode = new RPPreSamplePayCode()
                        {
                            Code = erCode,
                            Description = erDesc,
                            InUse = true
                        };
                        rpPreSamplePayCodes = CheckAddToList(rpPreSamplePayCodes, rpPreSamplePayCode);
                        //Add the Ee Pension
                        rpPreSamplePayCode = new RPPreSamplePayCode()
                        {
                            Code = eeCode,
                            Description = eeDesc,
                            InUse = true
                        };
                        rpPreSamplePayCodes = CheckAddToList(rpPreSamplePayCodes, rpPreSamplePayCode);
                    }
                    //There could be multiple pay codes in each employee.
                    foreach (XmlElement payCode in employee.GetElementsByTagName("PayCode"))
                    {
                        rpPreSamplePayCode = new RPPreSamplePayCode()
                        {
                            Code = prWG.GetElementByTagFromXml(payCode, "Code"),
                            Description = prWG.GetElementByTagFromXml(payCode, "Description"),
                            InUse = true
                        };
                        rpPreSamplePayCodes = CheckAddToList(rpPreSamplePayCodes, rpPreSamplePayCode);
                    }
                }
            }
            return rpPreSamplePayCodes;
        }
        private List<RPEmployeePeriod> CreateListOfEmployeePeriods(XmlDocument xmlReport)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            //Create a list of all the employees within each pay run date.
            List<RPEmployeePeriod> rpEmployeePeriods = new List<RPEmployeePeriod>();
            //There could be multiple payruns in this xml file.
            foreach (XmlElement payRun in xmlReport.GetElementsByTagName("PayRun"))
            {
                DateTime payRunDate = Convert.ToDateTime(prWG.GetDateElementByTagFromXml(payRun, "PaymentDate"));
                //There could be multiple employees in each pay run.
                foreach (XmlElement employee in payRun.GetElementsByTagName("Employee"))
                {
                    RPEmployeePeriod rpEmployeePeriod = new RPEmployeePeriod();
                    rpEmployeePeriod.PayRunDate = payRunDate;
                    rpEmployeePeriod.Reference = prWG.GetElementByTagFromXml(employee, "Code");
                    rpEmployeePeriod.Fullname = prWG.GetElementByTagFromXml(employee, "LastName") +
                                                       " " +
                                                       prWG.GetElementByTagFromXml(employee, "FirstName");
                    rpEmployeePeriod.TaxCode = prWG.GetElementByTagFromXml(employee, "PayLineTaxCode");
                    rpEmployeePeriod.NILetter = prWG.GetElementByTagFromXml(employee, "NiLetter");
                    rpEmployeePeriod.PreTaxAddDed = prWG.GetDecimalElementByTagFromXml(employee,"PreTaxAddDed");
                    rpEmployeePeriod.AbsencePay = prWG.GetDecimalElementByTagFromXml(employee, "AbsencePay");
                    rpEmployeePeriod.HolidayPay = prWG.GetDecimalElementByTagFromXml(employee, "HolidayPay");
                    rpEmployeePeriod.PreTaxPension = prWG.GetDecimalElementByTagFromXml(employee, "PreTaxPension");
                    rpEmployeePeriod.TaxablePayTP = prWG.GetDecimalElementByTagFromXml(employee, "TaxablePay");
                    rpEmployeePeriod.Tax = prWG.GetDecimalElementByTagFromXml(employee, "Tax");
                    rpEmployeePeriod.NetNI = prWG.GetDecimalElementByTagFromXml(employee, "NetEeNi");
                    rpEmployeePeriod.PostTaxAddDed = prWG.GetDecimalElementByTagFromXml(employee, "PostTaxAddDed");
                    rpEmployeePeriod.PostTaxPension = prWG.GetDecimalElementByTagFromXml(employee, "PostTaxPension");
                    rpEmployeePeriod.AEO = prWG.GetDecimalElementByTagFromXml(employee, "AEO");
                    rpEmployeePeriod.StudentLoan = prWG.GetDecimalElementByTagFromXml(employee, "StudentLoan");
                    rpEmployeePeriod.NetPayTP = prWG.GetDecimalElementByTagFromXml(employee, "NetPay");
                    rpEmployeePeriod.ErNICTP = prWG.GetDecimalElementByTagFromXml(employee, "ErNi");
                    rpEmployeePeriod.ErPensionTotalTP = prWG.GetDecimalElementByTagFromXml(employee, "ErPension");

                    List<RPAddition> rpAdditions = new List<RPAddition>();
                    List<RPDeduction> rpDeductions = new List<RPDeduction>();
                    List<RPPensionPeriod> rpPensionPeriods = new List<RPPensionPeriod>();
                    //There could be multiple pensions in each employee.
                    foreach (XmlElement pension in employee.GetElementsByTagName("Pension"))
                    {
                        RPPensionPeriod rpPensionPeriod = new RPPensionPeriod();
                        rpPensionPeriod.Key = Convert.ToInt32(pension.GetAttribute("Key"));
                        rpPensionPeriod.Code = prWG.GetElementByTagFromXml(pension, "Code");
                        rpPensionPeriod.ProviderName = prWG.GetElementByTagFromXml(pension, "ProviderName");
                        rpPensionPeriod.SchemeName = prWG.GetElementByTagFromXml(pension, "SchemeName");
                        rpPensionPeriod.StartJoinDate = prWG.GetDateElementByTagFromXml(pension, "StartJoinDate");
                        rpPensionPeriod.IsJoiner = prWG.GetBooleanElementByTagFromXml(pension, "IsJoiner");
                        rpPensionPeriod.ProviderEmployerReference = prWG.GetElementByTagFromXml(pension, "ProviderEmployerRef");
                        rpPensionPeriod.EePensionYtd = prWG.GetDecimalElementByTagFromXml(pension, "EePensionYtd");
                        rpPensionPeriod.ErPensionYtd = prWG.GetDecimalElementByTagFromXml(pension, "ErPensionYtd");
                        rpPensionPeriod.PensionablePayYtd = prWG.GetDecimalElementByTagFromXml(pension, "PensionablePayYtd");
                        rpPensionPeriod.EePensionTaxPeriod = prWG.GetDecimalElementByTagFromXml(pension, "EePensionTaxPeriod");
                        rpPensionPeriod.ErPensionTaxPeriod = prWG.GetDecimalElementByTagFromXml(pension, "ErPensionTaxPeriod");
                        rpPensionPeriod.PensionablePayTaxPeriod = prWG.GetDecimalElementByTagFromXml(pension, "PensionablePayTaxPeriod");
                        rpPensionPeriod.EePensionPayRunDate = prWG.GetDecimalElementByTagFromXml(pension, "EePensionPayRunDate");
                        rpPensionPeriod.ErPensionPayRunDate = prWG.GetDecimalElementByTagFromXml(pension, "ErPensionPayRunDate");
                        rpPensionPeriod.PensionablePayPayRunDate = prWG.GetDecimalElementByTagFromXml(pension, "PensionablePayDate");
                        rpPensionPeriod.EeContibutionPercent = prWG.GetDecimalElementByTagFromXml(pension, "EeContributionPercent") * 100;
                        rpPensionPeriod.ErContributionPercent = prWG.GetDecimalElementByTagFromXml(pension, "ErContributionPercent") * 100;
                        rpPensionPeriod.TotalPayTaxPeriod = rpEmployeePeriod.Gross;
                        
                        rpPensionPeriods.Add(rpPensionPeriod);

                        string eeCode = prWG.GetElementByTagFromXml(pension, "Code") +
                                   prWG.GetElementByTagFromXml(pension, "ProviderName");
                        string eeDesc = prWG.GetElementByTagFromXml(pension, "SchemeName");
                        string erCode = eeCode + "(Er)";
                        string erDesc = eeDesc + "(Er)";
                        //Add as an addition to the employee period object for Ee pension
                        RPAddition rpAddition = new RPAddition()
                        {
                            Code = erCode,
                            Description=erDesc,
                            AmountTP = prWG.GetDecimalElementByTagFromXml(pension, "ErPensionTaxPeriod")
                        };
                        rpAdditions.Add(rpAddition);
                        //Add as an addition to the employee period object for Ee pension
                        rpAddition = new RPAddition()
                        {
                            Code = eeCode,
                            Description = eeDesc,
                            AmountTP = prWG.GetDecimalElementByTagFromXml(pension, "EePensionTaxPeriod")
                        };
                        rpAdditions.Add(rpAddition);
                    }
                    rpEmployeePeriod.Pensions = rpPensionPeriods;
                    //There could be multiple pay codes in each employee.
                    foreach (XmlElement payCode in employee.GetElementsByTagName("PayCode"))
                    {
                        //Add them all as additions for the purposes of this report
                        RPAddition rpAddition = new RPAddition()
                        {
                            Code = prWG.GetElementByTagFromXml(payCode, "Code"),
                            Description = prWG.GetElementByTagFromXml(payCode, "Description"),
                            AmountTP = prWG.GetDecimalElementByTagFromXml(payCode, "Amount")
                        };
                        rpAdditions.Add(rpAddition);
  
                    }
                    rpEmployeePeriod.Additions = rpAdditions;
                    rpEmployeePeriod.Deductions = rpDeductions;
                    rpEmployeePeriods.Add(rpEmployeePeriod);
                }

            }
            return rpEmployeePeriods;
        }
        private List<RPPreSamplePayCode> CheckAddToList(List<RPPreSamplePayCode> rpPreSamplePayCodes, RPPreSamplePayCode rpPreSampleNewPayCode)
        {
            bool inList = false;
            foreach(RPPreSamplePayCode rpPreSamplePayCode in rpPreSamplePayCodes)
            {
                if(rpPreSampleNewPayCode.Code == rpPreSamplePayCode.Code)
                {
                    inList = true;
                    break;
                }
            }
            if(!inList)
            {
                rpPreSamplePayCodes.Add(rpPreSampleNewPayCode);
            }
            return rpPreSamplePayCodes;
        }
        private void CreateTxtReports(string txtReport)
        {
            string reportName = null;
            
            if (comboBoxChooseReport.Text == "PAPDIS Report")
            {
                reportName = "PAPDISReport";
            }
            else if (comboBoxChooseReport.Text == "Royal London Pension Report")
            {
                reportName = "RoyalLondonPensionReport";
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

            if (comboBoxChooseReport.SelectedText=="Combined Payroll Run Report" || 
                comboBoxChooseReport.SelectedText == "Department Within Branch Payroll Run Details Report" ||
                comboBoxChooseReport.SelectedText == "Pension Contributions To Date Report" ||
                comboBoxChooseReport.SelectedText == "Pre Report")
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
            else if (comboBoxChooseReport.SelectedText == "P11 Substitute" ||
                     comboBoxChooseReport.SelectedText == "Apprenticeship Levy Report")
            {
                txtEditParameter1.Visible = true;
                comboBoxChooseFrequency.Visible = true;
                txtEditParameter3.Visible = true;
                lblParameter1.Text = "Enter employer Number in the form nnnn.";
                lblParameter2.Text = "Enter pay schedule or frequency e.g. Weekly or Monthly.";
                lblParameter3.Text = "Enter tax year.";
                lblParameter1.Visible = true;
                lblParameter2.Visible = true;
                lblParameter3.Visible = true;
                
            }
            else if (comboBoxChooseReport.SelectedText == "PAPDIS Report" ||
                     comboBoxChooseReport.SelectedText == "Royal London Pension Report")
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
