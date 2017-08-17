using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities{
    
    public class MenuEntity
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int ParentMenuId { get; set; }
        public string Url { get; set; }
        public string ReportPath { get; set; }
        public long ToolId { get; set; }
    }
}
