using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;
using System.Data;

namespace DM_BusinessService
{
    public interface IDashboard
    {
        List<DashboardReportEntity> GetReportsByTool(string client_ID, string project_ID, long? ToolID, ref string status_Code, ref string message);
        List<DashboardReportEntity> GetReportCategoryByTool(string client_ID, string project_ID, long? ToolID, ref string status_Code, ref string message);
        List<MenuEntity> GetMenus(string user_name, string menu_type, ref string status_Code, ref string message);
    }
}
