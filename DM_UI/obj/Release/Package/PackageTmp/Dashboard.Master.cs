using DM_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DM_UI
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          
            lnkUserName.InnerText = UIProperties.Sessions.UserName;
            dynamic _Tool = UIProperties.Tool_Logos.GetToolLogo(Convert.ToInt16(UIProperties.Sessions.ToolID));
            imglogo.ImageUrl = _Tool.Logo;
            imglogo.PostBackUrl = _Tool.PostBackUrl;


           
        }


        public static string GetHomePage
        {
            get { return ConfigurationManager.AppSettings["UrlHomePage"]; }
        }
        public static string GetHostIP
        {
            get { return ConfigurationManager.AppSettings["HostIP"]; }
        }
        public static string GetLoginPage
        {
            get { return ConfigurationManager.AppSettings["UrlLoginPage"].ToString(); }
        }
       
    }
}