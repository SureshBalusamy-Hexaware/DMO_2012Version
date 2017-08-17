using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_DataModel.UnitOfWork;
using DM_DataModel;
using AutoMapper;
using DM_BusinessEntities;
using System.Data;

namespace DM_BusinessService
{
    public class DashboardService : IDashboard
    {
        public readonly Dashboard _dashboard;
        public DashboardService()
        {
            _dashboard = new Dashboard();
        }
        public List<DM_BusinessEntities.DashboardReportEntity> GetReportsByTool(string client_ID, string project_ID, long? ToolID, ref string status_Code, ref string message)
        {
            var _Reportdetails = _dashboard.GetReportsByTool(client_ID, project_ID, ToolID, ref status_Code, ref message);

            if (_Reportdetails != null)
            {
                Mapper.CreateMap<RPT_GET_REPORT_DETAILS_SP_Result, DashboardReportEntity>();
                var __ReportdetailsModel = Mapper.Map<List<RPT_GET_REPORT_DETAILS_SP_Result>, List<DashboardReportEntity>>(_Reportdetails);
                return __ReportdetailsModel;
            }
            return null;
        }

        public List<DashboardReportEntity> GetReportCategoryByTool(string client_ID, string project_ID, long? ToolID, ref string status_Code, ref string message)
        {
            List<DM_BusinessEntities.DashboardReportEntity> _lstReports = GetReportsByTool(client_ID, project_ID, ToolID, ref status_Code, ref message);

            var res = _lstReports
                .OrderBy(r => r.Report_Category)
                .GroupBy(r => r.Report_Category);                
            return _lstReports;
        }

        public List<MenuEntity> GetMenus(string user_name, string menu_type, ref string status_Code, ref string message)
        {
            var _menu = _dashboard.GetMenus(user_name, menu_type, ref status_Code, ref message);

            if (_menu != null)
            {
                Mapper.CreateMap<CMN_GET_USR_RLE_MNU_BY_TYPE_SP_Result, MenuEntity>();
                var _menuModel = Mapper.Map<List<CMN_GET_USR_RLE_MNU_BY_TYPE_SP_Result>, List<MenuEntity>>(_menu);
                return _menuModel;
            }
            return null;
        }
    }
}
