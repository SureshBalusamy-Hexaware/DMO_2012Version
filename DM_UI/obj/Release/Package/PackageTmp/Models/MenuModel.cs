using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM_UI.Models
{  
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int ParentMenuId { get; set; }
        public int MenuOrderId { get; set; }
        public int ToolId { get; set; }
        public int ActiveFlag { get; set; }
        public htmlAttr HtmlAttribute { get; set; }
    }
    public class htmlAttr
    {
        public bool Selected { get; set; }
        public string Style { get; set; }

    }
}