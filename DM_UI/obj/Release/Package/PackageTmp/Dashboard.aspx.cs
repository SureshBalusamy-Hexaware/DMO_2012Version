using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.IO;

namespace DM_UI
{
    public partial class Dashboard1 : System.Web.UI.Page
    {
        private readonly IDashboard _dashboard;
        private readonly IHXRConfigurationMS _configMS;              

        public int SelectedMenuId
        {
            get { return ViewState["SelectedMenuId"] == null ? 1 : Convert.ToInt16(ViewState["SelectedMenuId"].ToString()); }
            set { ViewState["SelectedMenuId"] = value; }
        }

        private List<MenuEntity> MenuList
        {
            get { return (List<MenuEntity>)Session["MenuItem"]; }
            set { Session["MenuItem"] = value; }
        }

        private int ToolID { get; set; }

        #region Public Constructor
        public Dashboard1()
        {
            _dashboard = new DashboardService();
            _configMS = new HXRConfigurationMSService();

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UIProperties.Sessions.UserName) || string.IsNullOrEmpty(UIProperties.Sessions.ToolID))
                Response.Redirect("Home/Login");
            

            if (!IsPostBack)
            {                
                InitializeScreen();

                //InitializeTools();                

                string toolid = UIProperties.Sessions.ToolID;

                if (toolid != null)
                {
                    int toolID;
                    int.TryParse(toolid, out toolID);
                    ToolID = toolID;
                    //GetReportsByTool();
                }

                this.GetUserMenus();
            }
        }

        private void InitializeScreen()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName, ref StatusCode, ref  Message);
        }

        private void ShowReport(string RptName, string RptPath)
        {
            try
            {
                //report url  
                //string urlReportServer = "http://Servername/Reportserver";
                string urlReportServer = RptPath;


                // ProcessingMode will be Either Remote or Local  
                // rptViewer.ProcessingMode = ProcessingMode.Remote;

                //Set the ReportServer Url  
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer);

                // setting report path  
                //Passing the Report Path with report name no need to add report extension   
                rptViewer.ServerReport.ReportPath = RptName;


                //Set report Parameter  
                //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report  
                //ArrayList reportParam = new ArrayList();  
                //reportParam = ReportDefaultPatam();  

                //ReportParameter[] param = new ReportParameter[reportParam.Count];  
                //for (int k = 0; k < reportParam.Count; k++)  
                //{  
                //    param[k] = (ReportParameter)reportParam[k];  
                //}  

                // pass credential as if any... no need to pass anything if we use windows authentication  
                //rptViewer.ServerReport.ReportServerCredentials =   
                //  new ReportServerCredentials("UserName", "Password", "domainname");  

                //pass parameters to report  
                //rptViewer.ServerReport.SetParameters(param);   

                ReportParameterInfoCollection rptparam = rptViewer.ServerReport.GetParameters();
                foreach (ReportParameterInfo param in rptparam)
                {
                    switch (param.Name)
                    {
                        case "ClientID":
                            rptViewer.ServerReport.SetParameters(new ReportParameter("ClientID", UIProperties.Sessions.Client.Client_ID));
                            break;
                        case "ProjectID":
                            rptViewer.ServerReport.SetParameters(new ReportParameter("ProjectID", UIProperties.Sessions.Client.project_ID));
                            break;
                    }
                }  
                rptViewer.Visible = true;
                rptViewer.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                //throw ex;
                WriteErrorLog(ex);

            }
        }

        #region Commented old methodology
        /*

        private void ResetReportViewer()
        {
            rptViewer.Visible = false;
        }

        private void SetcssSelectedTab()
        {
            string Defaultcss = "ui-state-default ui-corner-top shadow ";
            string Selectedcss = "ui-tabs-selected";
            switch (SelectedMenuId)
            {
                case 1:
                    liHexarule.Attributes.Add("class", Defaultcss);
                    // liAutomaton.Attributes.Add("class", Defaultcss);
                    liDIMAPlus.Attributes.Add("class", Defaultcss);
                    liDart.Attributes.Add("class", Defaultcss);
                    // liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss + Selectedcss);
                    liDatarecon.Attributes.Add("class", Defaultcss);
                    break;
                case 2:
                    liHexarule.Attributes.Add("class", Defaultcss + Selectedcss);
                    // liAutomaton.Attributes.Add("class", Defaultcss );
                    liDIMAPlus.Attributes.Add("class", Defaultcss);
                    liDart.Attributes.Add("class", Defaultcss);
                    // liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss);
                    liDatarecon.Attributes.Add("class", Defaultcss);
                    break;
                case 3:
                    //liAutomaton.Attributes.Add("class", Defaultcss);
                    liHexarule.Attributes.Add("class", Defaultcss);
                    liDIMAPlus.Attributes.Add("class", Defaultcss + Selectedcss);
                    liDart.Attributes.Add("class", Defaultcss);
                    // liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss);
                    liDatarecon.Attributes.Add("class", Defaultcss);
                    break;
                case 4:
                    // liAutomaton.Attributes.Add("class", Defaultcss);
                    liHexarule.Attributes.Add("class", Defaultcss);
                    liDIMAPlus.Attributes.Add("class", Defaultcss);
                    liDart.Attributes.Add("class", Defaultcss);
                    //liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss);
                    liDatarecon.Attributes.Add("class", Defaultcss + Selectedcss);
                    break;
                case 5:
                    //  liAutomaton.Attributes.Add("class", Defaultcss);
                    liHexarule.Attributes.Add("class", Defaultcss);
                    liDIMAPlus.Attributes.Add("class", Defaultcss);
                    liDart.Attributes.Add("class", Defaultcss + Selectedcss);
                    // liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss);
                    liDatarecon.Attributes.Add("class", Defaultcss);
                    break;
                //case 6:
                //    liAutomaton.Attributes.Add("class", Defaultcss);
                //    liHexarule.Attributes.Add("class", Defaultcss);
                //    liDIMAPlus.Attributes.Add("class", Defaultcss);
                //    liDart.Attributes.Add("class", Defaultcss);
                //    liDima.Attributes.Add("class", Defaultcss + Selectedcss);
                //    liDataProfiler.Attributes.Add("class", Defaultcss);
                //    liDatarecon.Attributes.Add("class", Defaultcss);
                //    break;

                //case 7:
                //    liAutomaton.Attributes.Add("class", Defaultcss);
                //    liHexarule.Attributes.Add("class", Defaultcss);
                //    liDIMAPlus.Attributes.Add("class", Defaultcss);
                //    liDart.Attributes.Add("class", Defaultcss);
                //    liDima.Attributes.Add("class", Defaultcss);
                //    liDataProfiler.Attributes.Add("class", Defaultcss);
                //    liDatarecon.Attributes.Add("class", Defaultcss + Selectedcss);
                //  break;
                default:
                    //   liAutomaton.Attributes.Add("class", Defaultcss );
                    liHexarule.Attributes.Add("class", Defaultcss);
                    liDIMAPlus.Attributes.Add("class", Defaultcss);
                    liDart.Attributes.Add("class", Defaultcss);
                    // liDima.Attributes.Add("class", Defaultcss);
                    liDataProfiler.Attributes.Add("class", Defaultcss + Selectedcss);
                    liDatarecon.Attributes.Add("class", Defaultcss);
                    break;
            }

        }

        private void InitializeTools()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            ToolID = Convert.ToInt16(UIProperties.Sessions.ToolID);
            MasterSetupService _masterSetup = new MasterSetupService();
            string _clientId = UIProperties.Sessions.Client.Client_ID;
            string _projectId = UIProperties.Sessions.Client.project_ID;
            string _UserName = UIProperties.Sessions.UserName;

            List<DM_BusinessEntities.UserToolEntity> _userTools =
                _masterSetup.GetTools(_clientId, _projectId, _UserName, ref StatusCode, ref Message).Where(r => r.ACTIVE_FLAG == 1).ToList();


            
            ////Automaton            
            //if (_userTools.Exists(r => r.TOOL_ID == 1))
            //    liAutomaton.Visible = true;
            //else
            //    liAutomaton.Visible = false;

            //DART
            if (_userTools.Exists(r => r.TOOL_ID == 2))
            {
                if (ToolID == 2 || ToolID == 3)
                    liDart.Visible = true;
                else
                    liDart.Visible = false;
            }
            else
                liDart.Visible = false;

            ////Dashboard & Reports
            //if (_userTools.Exists(r => r.TOOL_ID == 3))
            //    lidashboardrpts.Visible = true;
            //else
            //    lidashboardrpts.Visible = false;


            //Data Recon
            if (_userTools.Exists(r => r.TOOL_ID == 4))
            {
                if (ToolID == 4 || ToolID == 3)
                    liDatarecon.Visible = true;
                else
                    liDatarecon.Visible = false;

            }
            else
                liDatarecon.Visible = false;

            ////DIMA
            //if (_userTools.Exists(r => r.TOOL_ID == 5))
            //    lidima.Visible = true;
            //else
            //    lidima.Visible = false;

            //DIMA PLUS
            if (_userTools.Exists(r => r.TOOL_ID == 6))
            {
                if (ToolID == 6 || ToolID == 3)
                    liDIMAPlus.Visible = true;
                else
                    liDIMAPlus.Visible = false;

            }
            else
                liDIMAPlus.Visible = false;

            //HexaRule
            if (_userTools.Exists(r => r.TOOL_ID == 7))
            {
                if (ToolID == 7 || ToolID == 3)
                    liHexarule.Visible = true;
                else
                    liHexarule.Visible = false;
            }
            else
                liHexarule.Visible = false;

            //InfaGen
            //if (Tools.Exists(r => r.ToolID == 8))
            //    liHexaRule.Visible = true;                
            //else
            //    liHexaRule.Visible = false;

            ////SQL PARSER
            //if (_userTools.Exists(r => r.TOOL_ID == 9))
            //    lisqlparser.Visible = true;
            //else
            //    lisqlparser.Visible = false;

            //Data Profiler
            if (_userTools.Exists(r => r.TOOL_ID == 11))
            {
                if (ToolID == 11 || ToolID == 3)
                    liDataProfiler.Visible = true;
                else
                    liDataProfiler.Visible = false;
            }
            else
                liDataProfiler.Visible = false;


        }        
        
        private void GetReportsByTool()
        {
            string _clientId = UIProperties.Sessions.Client.Client_ID;
            string _projectId = UIProperties.Sessions.Client.project_ID;
            string StatusCode = string.Empty, Message = string.Empty;

            List<DashboardReportEntity> _dashboardEntity = _dashboard.GetReportsByTool(_clientId, _projectId, ToolID, ref StatusCode, ref Message);
            rptReports.DataSource = _dashboardEntity;
            rptReports.DataBind();
        }        

        protected void lnkbtnHexarule_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.HexaRule;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 2;
                SetcssSelectedTab();
            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnAutomaton_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.Automaton;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 2;
                SetcssSelectedTab();
            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnDimaPlus_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.DIMAPLUS;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 3;
                SetcssSelectedTab();

            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnDima_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.DIMA;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 5;
                SetcssSelectedTab();
            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnSQLParser_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.SQLPARSER;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 6;
                SetcssSelectedTab();
            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnDatarecon_Click(object sender, EventArgs e)
        {
            try
            {

                ToolID = (int)UIProperties.Tools.DataRecon;
                GetReportsByTool();

                ResetReportViewer();
                SelectedMenuId = 4;
                SetcssSelectedTab();

            }
            catch (Exception _e)
            {

            }
        }

        protected void lnkbtnDataProfiler_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.DataProfiler;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 1;
                SetcssSelectedTab();
            }
            catch (Exception _ex)
            {

            }
        }

        protected void lnkbtnDart_Click(object sender, EventArgs e)
        {
            try
            {
                ToolID = (int)UIProperties.Tools.DART;
                GetReportsByTool();
                ResetReportViewer();
                SelectedMenuId = 5;
                SetcssSelectedTab();
            }
            catch (Exception _ex)
            {

            }
        }
         
        protected void lnkbtnRptCategory_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
            }
            catch (Exception _ex)
            {

            }
        }
        
        */
        #endregion

        private void GetUserMenus()
        {
            List<MenuEntity> _menu;  
            string StatusCode = string.Empty, Message = string.Empty;
            MenuList = _menu = _dashboard.GetMenus(UIProperties.Sessions.UserName, "Report", ref StatusCode, ref Message);

            var _pMenu = _menu.Where(m => m.ParentMenuId == 0).ToList().Where(n => ToolID != 3 ? n.ToolId == ToolID : 1 == 1).ToList();
            
            rptParentMenu.DataSource = _pMenu;
            rptParentMenu.DataBind();

            //For viewing only the tool specific report then by default it will shown.
            if (_pMenu.Count == 1)
                lnkbtnParentMenu_Click(rptParentMenu.Items[0].FindControl("li").FindControl("lnkbtnParentMenu"), null);
            
        }

        protected void lnkbtnParentMenu_Click(object sender, EventArgs e)
        {
            string Defaultcss = "ui-state-default ui-corner-top shadow ";
            string Selectedcss = "ui-tabs-selected";
            hdnRpt.Value = "0"; //Hide Tableau report

            LinkButton lnkbtn = (LinkButton)sender;

            int menuid = Convert.ToInt32(lnkbtn.Attributes["MenuID"]);
            var _childMenu = MenuList.Where(m => m.ParentMenuId == menuid).ToList();

            //Set menu items for repeater control.
            rptReports.DataSource = _childMenu;
            rptReports.DataBind();

            //Reset report viewer
            //rptViewer.Visible = false;

            foreach (RepeaterItem item in rptParentMenu.Items)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl cntrl = (System.Web.UI.HtmlControls.HtmlGenericControl)item.FindControl("li");

                string cssClass = cntrl.ClientID == ((System.Web.UI.HtmlControls.HtmlGenericControl)lnkbtn.Parent).ClientID ? Defaultcss + Selectedcss : Defaultcss;

                cntrl.Attributes["class"] = cssClass;
            }
        }
        
        protected void lnkbtnReports_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string rpt_Name = lnkbtn.Attributes["RptName"];
                string serverPath = lnkbtn.Attributes["ServerPath"];

                hdnRpt.Value = "0"; //Hide Tableau report

                if (lnkbtn.Text.Contains("Tableau"))
                {
                    hdnRpt.Value = "1"; //show tableau report in client side
                    rptViewer.Visible = false;                                        
                }
                else
                {                    
                    ShowReport(rpt_Name, serverPath);
                }
            }
            catch (Exception _e)
            {

            }
        }

        public void WriteErrorLog(Exception ex)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string _filePath = Path.GetFullPath(Path.Combine(filePath, @"ErrorLog.txt"));

            if (!File.Exists(_filePath))
                File.Create(_filePath);
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine("Date :" + DateTime.Now.ToString() +
                    " Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine);
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

        }
    }
}