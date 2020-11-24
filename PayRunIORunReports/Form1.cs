using System;
using System.Xml;
using PayRunIO.CSharp.SDK;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using PayRunIOClassLibrary;
using System.Windows.Forms;
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
                    comboBoxChooseReport.Text == "Note And Coin Requirement Report")
                {
                    if (txtEditParameter1.Text == "" || comboBoxChooseFrequency.Text == "" | dateStartDate.Text == "" | dateEndDate.Text == "" | btnEditSavePDFReports.Text == "")
                    {
                        allEntered = false;
                    }
                }
            }

            return allEntered;
        }
        private void ProduceReport()
        {
            string startDate = dateStartDate.DateTime.ToString("yyyy-MM-dd");
            string endDate = dateEndDate.DateTime.ToString("yyyy-MM-dd");
            string sortByBranch = "False";
            string prm1 = null;
            string prm2 = null;
            string prm3 = null;
            string prm4 = null;
            string prm5 = null;
            string rptRef = null;
            string url = null;
            if (comboBoxChooseReport.Text == "Combined Payroll Run Report" || 
                comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report" ||
                comboBoxChooseReport.Text == "Note And Coin Requirement Report")
            {
                //I'm using the same PayRun.IO report for all 3 of these reports. I just need to sort the department with branch report differently.
                prm1 = "EmployerKey";
                prm2 = "PayScheduleKey";
                prm3 = "StartDate";
                prm4 = "EndDate";
                prm5 = "SortByBranch";
                rptRef = "Combined Payroll Run Report";

                if (comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
                {
                    sortByBranch = "true";
                }
                rptRef = rptRef.Replace(" ", "");
                rptRef = rptRef.Replace("Report", "");
                url = prm1 + "=" + txtEditParameter1.Text + "&"
                    + prm2 + "=" + comboBoxChooseFrequency.Text + "&"
                    + prm3 + "=" + startDate + "&"
                    + prm4 + "=" + endDate + "&"
                    + prm5 + "=" + sortByBranch;
            }
            else if (comboBoxChooseReport.Text == "P32 Report")
            {
                //I'll get these parameters from the database.
                prm1 = "EmployerKey";
                prm2 = "TaxYear";
                rptRef = comboBoxChooseReport.SelectedText;
                rptRef = rptRef.Replace(" ", "");
                rptRef = rptRef.Replace("Report", "");
                url = prm1 + "=" + txtEditParameter1.Text + "&"
                    + prm2 + "=" + txtEditParameter2.Text;
            }

            XmlDocument xmlReport = null;

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

            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            RPParameters rpParameters = prWG.GetRPParameters(xmlReport);
            RPEmployer rpEmployer = prWG.GetRPEmployer(xmlReport, rpParameters);

            RPSummaryPayRuns rpSummaryPayRuns = new RPSummaryPayRuns();
            string reportCode;

            if (comboBoxChooseReport.Text == "Combined Payroll Run Report")
            {

                reportCode="CPRR";
                rpSummaryPayRuns = CreatePayRunsDataSource(xmlReport, reportCode);

                
                CreateCombinedPayrollRunPDFReport(rpSummaryPayRuns, rpParameters, rpEmployer);
            }
            else if (comboBoxChooseReport.Text == "Department Within Branch Payroll Run Details Report")
            {
                //Department Within Branch (DWB)
                reportCode="DWBPRDR";
                rpSummaryPayRuns = CreatePayRunsDataSource(xmlReport, reportCode);


                CreateDWBPayrollRunPDFReport(rpSummaryPayRuns, rpParameters, rpEmployer);
            }
            else if (comboBoxChooseReport.Text  == "Note And Coin Requirement Report")
            {
                reportCode="NACRR";
                rpSummaryPayRuns = CreatePayRunsDataSource(xmlReport, reportCode);


                CreateNoteAndCoinRequirementPDFReport(rpSummaryPayRuns, rpParameters, rpEmployer);
            }


        }
        private RPSummaryPayRuns CreatePayRunsDataSource(XmlDocument xmlReport, string reportCode)
        {
            PayRunIOWebGlobeClass prWG = new PayRunIOWebGlobeClass();
            RPSummaryPayRuns rpSummaryPayRuns = new RPSummaryPayRuns();
            rpSummaryPayRuns.MaxDepartments = 0;
            
            

            List<RPSummaryPayRun> listRPSummaryPayRuns = new List<RPSummaryPayRun>();

            foreach(XmlElement xmlPayRun in xmlReport.GetElementsByTagName("PayRun"))
            {
                int payRunDepartments = 0;
                RPSummaryPayRun rpSummaryPayRun = new RPSummaryPayRun();
                rpSummaryPayRun.PaymentDate = Convert.ToDateTime(prWG.GetDateElementByTagFromXml(xmlPayRun, "PaymentDate"));
                rpSummaryPayRun.TaxPeriod = prWG.GetIntElementByTagFromXml(xmlPayRun, "TaxPeriod");
                rpSummaryPayRun.TaxYear = prWG.GetIntElementByTagFromXml(xmlPayRun, "TaxYear");
                int payeMonth = rpSummaryPayRun.PaymentDate.Day < 6 ? rpSummaryPayRun.PaymentDate.Month - 4 : rpSummaryPayRun.PaymentDate.Month - 3;
                if (payeMonth <= 0)
                {
                    payeMonth += 12;
                }
                rpSummaryPayRun.PAYEMonth = payeMonth;

                List<RPBranch> rpBranches = new List<RPBranch>();
                RPBranch rpBranch = new RPBranch();
                
                List<RPDepartment> rpDepartments = new List<RPDepartment>();
                RPDepartment rpDepartment = new RPDepartment();
                bool firstDepartment = true;

                List<RPSummaryEmployee> rpSummaryEmployees = new List<RPSummaryEmployee>();

                foreach(XmlElement xmlEmployee in xmlPayRun.GetElementsByTagName("Employee"))
                {

                    RPSummaryEmployee rpSummaryEmployee = new RPSummaryEmployee();
                    rpSummaryEmployee.Code = prWG.GetElementByTagFromXml(xmlEmployee, "Code");
                    rpSummaryEmployee.LastName = prWG.GetElementByTagFromXml(xmlEmployee, "LastName");
                    rpSummaryEmployee.FirstName = prWG.GetElementByTagFromXml(xmlEmployee, "FirstName");
                    rpSummaryEmployee.LastNameFirstName = rpSummaryEmployee.LastName + " " + rpSummaryEmployee.FirstName;
                    rpSummaryEmployee.FirstNameLastName = rpSummaryEmployee.FirstName + " " + rpSummaryEmployee.LastName;
                    rpSummaryEmployee.Branch = prWG.GetElementByTagFromXml(xmlEmployee, "Branch");
                    rpSummaryEmployee.Department = prWG.GetElementByTagFromXml(xmlEmployee, "Department");
                    if(reportCode=="CPRR" || reportCode == "NACRR")
                    {
                        //Neither Combined Payroll Run or Note And Coin Requirement reports - Are by branch/department
                        rpSummaryEmployee.Branch = "[Default]";
                        rpSummaryEmployee.Department = "[Default]";
                    }
                    rpSummaryEmployee.TaxCode = prWG.GetElementByTagFromXml(xmlEmployee, "TaxCode");
                    rpSummaryEmployee.TaxBasis = prWG.GetElementByTagFromXml(xmlEmployee, "TaxBasis");
                    if(rpSummaryEmployee.TaxBasis == "Week1")
                    {
                        rpSummaryEmployee.TaxCode = rpSummaryEmployee.TaxCode + "(1)";
                    }
                    rpSummaryEmployee.NiLetter = prWG.GetElementByTagFromXml(xmlEmployee, "NiLetter");
                    rpSummaryEmployee.PreTaxAddDed = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "PreTaxAddDed"));
                    rpSummaryEmployee.GUCosts = 0;
                    rpSummaryEmployee.AbsencePay = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "AbsencePay"));
                    rpSummaryEmployee.HolidayPay= Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "HolidayPay"));
                    rpSummaryEmployee.PreTaxPension = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "PreTaxPension"));
                    rpSummaryEmployee.TaxablePay = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "TaxablePay"));
                    rpSummaryEmployee.Tax = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "Tax"));
                    rpSummaryEmployee.NetEeNi = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "NetEeNi"));
                    rpSummaryEmployee.PostTaxAddDed = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "PostTaxAddDed"));
                    rpSummaryEmployee.PostTaxPension = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "PostTaxPension"));
                    rpSummaryEmployee.AEO = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "AEO"));
                    rpSummaryEmployee.StudentLoan = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "StudentLoan"));
                    decimal netPay = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "NetPay"));
                    rpSummaryEmployee.NetPay = GetNetPayCashAnalysis(netPay);
                    rpSummaryEmployee.ErNi = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "ErNi"));
                    rpSummaryEmployee.ErPension = Convert.ToDecimal(prWG.GetDecimalElementByTagFromXml(xmlEmployee, "ErPension"));
                    rpSummaryEmployee.PaymentType = prWG.GetElementByTagFromXml(xmlEmployee, "PaymentMethod");
                    //For testing.
                    //rpSummaryEmployee.PaymentType = "Cash";
                    bool include = true;
                    if (reportCode == "NACRR" && rpSummaryEmployee.PaymentType != "Cash")
                    {
                        //Only include employees paid by cash for the Note And Coin Requirement Report
                        include = false;
                    }
                        
                    if (rpSummaryEmployee.NetPay.NetPay != 0 && include)
                    {
                        if (firstDepartment)
                        {
                            rpBranch.Name = rpSummaryEmployee.Branch;
                            rpDepartment.Name = rpSummaryEmployee.Department;
                            firstDepartment = false;
                            payRunDepartments++;
                        }
                        else
                        {
                            if (rpBranch.Name != rpSummaryEmployee.Branch)
                            {
                                rpDepartment.Employees = rpSummaryEmployees;
                                rpDepartments.Add(rpDepartment);

                                rpDepartment = new RPDepartment();
                                rpDepartment.Name = rpSummaryEmployee.Department;
                                rpSummaryEmployees = new List<RPSummaryEmployee>();

                                rpBranch.RPDepartments = rpDepartments;
                                rpBranches.Add(rpBranch);

                                rpBranch = new RPBranch();
                                rpBranch.Name = rpSummaryEmployee.Branch;

                                rpDepartments = new List<RPDepartment>();

                                payRunDepartments++;
                            }
                            else
                            {
                                if (rpDepartment.Name != rpSummaryEmployee.Department)
                                {
                                    rpDepartment.Employees = rpSummaryEmployees;
                                    rpDepartments.Add(rpDepartment);


                                    rpDepartment = new RPDepartment();
                                    rpDepartment.Name = rpSummaryEmployee.Department;
                                    rpSummaryEmployees = new List<RPSummaryEmployee>();

                                    payRunDepartments++;
                                }
                            }
                        }
                        rpSummaryEmployees.Add(rpSummaryEmployee);
                        
                    }
                }
                rpDepartment.Employees = rpSummaryEmployees;
                rpDepartments.Add(rpDepartment);

                rpBranch.RPDepartments = rpDepartments;
                rpBranches.Add(rpBranch);

                rpSummaryPayRun.RPBranches = rpBranches;
                listRPSummaryPayRuns.Add(rpSummaryPayRun);

                if(payRunDepartments > rpSummaryPayRuns.MaxDepartments)
                {
                    rpSummaryPayRuns.MaxDepartments = payRunDepartments;
                }
            }

            rpSummaryPayRuns.RPSummaryPayRun = listRPSummaryPayRuns;

            return rpSummaryPayRuns; 
        }
        private RPNetPayCashAnalysis GetNetPayCashAnalysis(decimal netPay)
        {
            RPNetPayCashAnalysis netPayCashAnalysis = new RPNetPayCashAnalysis();
            netPayCashAnalysis.NetPay = netPay;
            int pence = Convert.ToInt32(netPay * 100);
            //£20
            netPayCashAnalysis.TwentyPounds = pence / 2000;
            pence = pence % 2000;
            //£10
            netPayCashAnalysis.TenPounds = pence / 1000;
            pence = pence % 1000;
            //£5
            netPayCashAnalysis.FivePounds = pence / 500;
            pence = pence % 500;
            //£2
            netPayCashAnalysis.TwoPounds = pence / 200;
            pence = pence % 200;
            //£1
            netPayCashAnalysis.OnePounds = pence / 100;
            pence = pence % 100;
            //50p
            netPayCashAnalysis.FiftyPence = pence / 50;
            pence = pence % 50;
            //20p
            netPayCashAnalysis.TwentyPence = pence / 20;
            pence = pence % 20;
            //10p
            netPayCashAnalysis.TenPence = pence / 10;
            pence = pence % 10;
            //5p
            netPayCashAnalysis.FivePence = pence / 5;
            pence = pence % 5;
            //2p
            netPayCashAnalysis.TwoPence = pence / 2;
            //1p
            netPayCashAnalysis.OnePence = pence % 2;
            return netPayCashAnalysis;
        }
        private void CreateCombinedPayrollRunPDFReport(RPSummaryPayRuns rpSummaryPayRuns, RPParameters rpParameters, RPEmployer rpEmployer)
        {
            string pdfReportDir = btnEditSavePDFReports.Text + "\\";
            string reportName = "CombinedPayrollRunReport.repx";
            //Load report
            XtraReport report1 = XtraReport.FromFile(reportName, true);

            report1.Parameters["CmpName"].Value = rpEmployer.Name;
            report1.Parameters["PayeRef"].Value = rpEmployer.PayeRef;
            string freq = rpParameters.PaySchedule + " payroll";
            report1.Parameters["Freq"].Value = freq;
            report1.Parameters["ShowDepartments"].Value = false;
            report1.Parameters["ReportName"].Value = "Combined Payroll Run Report";
            report1.DataSource = rpSummaryPayRuns.RPSummaryPayRun;
            // To show the report designer. You need to uncomment this to design the report.
            // You also need to comment out the report.ExportToPDF line below
            //
            bool designMode = false;
            if (designMode)
            {
                report1.ShowDesigner();
                //report1.ShowPreview();

            }
            else
            {
                // Export to pdf file.

                //
                // I'm going to remove spaces from the document name. I'll replace them with dashes
                //
                //string dirName = "V:\\Payescape\\PayRunIO\\WG\\";
                //string dirName = outgoingFolder + "\\" + coNo + "\\";
                //Directory.CreateDirectory(dirName);
                string docName = rpParameters.ErRef + "_CombinedPayrollRunReport.pdf";

                report1.ExportToPdf(pdfReportDir + docName);
                //if (rpEmployer.ReportsInExcelFormat)
                //{
                //    docName = docName.Replace(".pdf", ".xlsx");
                //    report1.ExportToXlsx(docName);
                //}
            }
        }
        private void CreateDWBPayrollRunPDFReport(RPSummaryPayRuns rpSummaryPayRuns, RPParameters rpParameters, RPEmployer rpEmployer)
        {
            string pdfReportDir = btnEditSavePDFReports.Text + "\\";
            string reportName = "CombinedPayrollRunReport.repx";
            //Load report
            XtraReport report1 = XtraReport.FromFile(reportName, true);

            report1.Parameters["CmpName"].Value = rpEmployer.Name;
            report1.Parameters["PayeRef"].Value = rpEmployer.PayeRef;
            string freq = rpParameters.PaySchedule + " payroll";
            report1.Parameters["Freq"].Value = freq;

            if(rpSummaryPayRuns.MaxDepartments > 1)
            {
                report1.Parameters["ShowDepartments"].Value = true;
            }
            else
            {
                report1.Parameters["ShowDepartments"].Value = false;
            }
            
            report1.Parameters["ReportName"].Value = "Department Within Branch Payroll Run Details Report";
            report1.DataSource = rpSummaryPayRuns.RPSummaryPayRun;
            // To show the report designer. You need to uncomment this to design the report.
            // You also need to comment out the report.ExportToPDF line below
            //
            bool designMode = false;
            if (designMode)
            {
                report1.ShowDesigner();
                //report1.ShowPreview();

            }
            else
            {
                // Export to pdf file.

                //
                // I'm going to remove spaces from the document name. I'll replace them with dashes
                //
                //string dirName = "V:\\Payescape\\PayRunIO\\WG\\";
                //string dirName = outgoingFolder + "\\" + coNo + "\\";
                //Directory.CreateDirectory(dirName);
                string docName = rpParameters.ErRef + "_DWBPayrollRunReport.pdf";

                report1.ExportToPdf(pdfReportDir + docName);
                //if (rpEmployer.ReportsInExcelFormat)
                //{
                //    docName = docName.Replace(".pdf", ".xlsx");
                //    report1.ExportToXlsx(docName);
                //}
            }
        }
        private void CreateNoteAndCoinRequirementPDFReport(RPSummaryPayRuns rpSummaryPayRuns, RPParameters rpParameters, RPEmployer rpEmployer)
        {
            string pdfReportDir = btnEditSavePDFReports.Text + "\\";
            string reportName = "NoteAndCoinRequirementReport.repx";
            //Load report
            XtraReport report1 = XtraReport.FromFile(reportName, true);

            report1.Parameters["CmpName"].Value = rpEmployer.Name;
            report1.Parameters["PayeRef"].Value = rpEmployer.PayeRef;
            string freq = rpParameters.PaySchedule + " payroll";
            report1.Parameters["Freq"].Value = freq;
            report1.Parameters["ShowDepartments"].Value = false;
            report1.Parameters["ReportName"].Value = "Note And Coin Requirement Report";
            report1.DataSource = rpSummaryPayRuns.RPSummaryPayRun;
            // To show the report designer. You need to uncomment this to design the report.
            // You also need to comment out the report.ExportToPDF line below
            //
            bool designMode = false;
            if (designMode)
            {
                report1.ShowDesigner();
                //report1.ShowPreview();

            }
            else
            {
                // Export to pdf file.

                //
                // I'm going to remove spaces from the document name. I'll replace them with dashes
                //
                //string dirName = "V:\\Payescape\\PayRunIO\\WG\\";
                //string dirName = outgoingFolder + "\\" + coNo + "\\";
                //Directory.CreateDirectory(dirName);
                string docName = rpParameters.ErRef + "_NoteAndCoinRequirementReport.pdf";

                report1.ExportToPdf(pdfReportDir + docName);
                //if (rpEmployer.ReportsInExcelFormat)
                //{
                //    docName = docName.Replace(".pdf", ".xlsx");
                //    report1.ExportToXlsx(docName);
                //}
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxChooseReport_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBoxChooseReport.SelectedText=="Combined Payroll Run Report" || 
               comboBoxChooseReport.SelectedText == "Department Within Branch Payroll Run Details Report" ||
               comboBoxChooseReport.SelectedText == "Note And Coin Requirement Report")
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
            else if (comboBoxChooseReport.SelectedText == "Something else")
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
