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
    public class DASEMService : IDASEM
    {
        private readonly DASEM _dimaplus;
        public DASEMService()
        {
            _dimaplus = new DASEM();
        }

        public List<string> GetSlicingColumns(string client_ID, string project_ID, string constraint_Type, long config_ID, ref string status_Code, ref string message)
        {
            return _dimaplus.GetSlicingColumns(client_ID, project_ID, constraint_Type, config_ID, ref status_Code, ref message);
        }
        public List<string> GetSlicingColumnValues(string client_ID, string project_ID, string Column_name, int Config_ID, ref string StatusCode, ref string Message)
        {
            return _dimaplus.GetSlicingColumnValues(client_ID, project_ID, Column_name, Config_ID, ref StatusCode, ref Message);
        }
        public DataTable GetCriteria(int page, int rows, string client_ID, string project_ID, string Object_Type, string Template_Name, long config_ID, string Column_name,
            string SlicingValue, int ToolId, string Expression, ref string StatusCode, ref string Message)
        {
            return _dimaplus.GetCriteria(page, rows, client_ID, project_ID, Object_Type, Template_Name, config_ID, Column_name, SlicingValue, ToolId, Expression,
                ref StatusCode, ref Message);

        }
        /// <summary>
        /// Get display column names
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="StatusCode"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public string GetCriteria(string Type, ref string StatusCode, ref string Message)
        {
            DataTable dtCriteriaColumns = _dimaplus.GetCriteria(Type, ref StatusCode, ref Message);
            string[] Columns = dtCriteriaColumns.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
            return string.Join(",", Columns);
        }
        public void SaveUpdateCriteria(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            _dimaplus.SaveUpdateCriteria(Criteria, ref StatusCode, ref Message);
        }
        public void UpdateCriteriaSourceDelete(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            _dimaplus.UpdateCriteriaSourceDelete(Criteria, ref StatusCode, ref Message);
        }
        public List<DIMAPLUSCriteriaEntity> GetAllCriteria(int page, int rows, string client_ID, string project_ID, ref string status_Code, ref string message)
        {
            var _Criteria = _dimaplus.GetAllCriteria(page, rows, client_ID, project_ID, ref status_Code, ref message);

            if (_Criteria != null)
            {
                Mapper.CreateMap<HXR_GET_CRITERIA_ALL_SP_Result, DIMAPLUSCriteriaEntity>();
                var _CriteriaModel = Mapper.Map<List<HXR_GET_CRITERIA_ALL_SP_Result>, List<DIMAPLUSCriteriaEntity>>(_Criteria);
                return _CriteriaModel;
            }
            return null;
        }
        public List<DIMAPLUSCopySliceEntity> GetSourceObjectDetails(string client_ID, string project_ID, long? Template_Id)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetSourceObjectDetails(client_ID, project_ID, Template_Id, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_CRI_OBJ_COUNT_SP_Result, DIMAPLUSCopySliceEntity>();
            var _lst = Mapper.Map<List<DASM_GET_CRI_OBJ_COUNT_SP_Result>, List<DIMAPLUSCopySliceEntity>>(lst);
            return _lst;
        }

        public List<DIMAPLUSCopySliceEntity> GetTargetServerDetails(string client_ID, string project_ID, long? RoleId)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetTargetServerDetails(client_ID, project_ID, RoleId, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_TARGETSERVER_SP_Result, DIMAPLUSCopySliceEntity>();
            var _lst = Mapper.Map<List<DASM_GET_TARGETSERVER_SP_Result>, List<DIMAPLUSCopySliceEntity>>(lst);
            return _lst;
        }


        public List<DIMAPLUSReportEntity> GetAuditReport(string client_ID, string project_ID, long? Template_ID)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetAuditReport(client_ID, project_ID, Template_ID, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_AUDITREPORT_SP_Result, DIMAPLUSReportEntity>();
            var _lst = Mapper.Map<List<DASM_GET_AUDITREPORT_SP_Result>, List<DIMAPLUSReportEntity>>(lst);
            return _lst;
        }

        public List<DIMAPLUSReportEntity> GetTransferResultReport(string client_ID, string project_ID, long? Template_ID, long? Run_ID)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetTransferResultReport(client_ID, project_ID, Template_ID, Run_ID, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_TRANSFERRESULT_SP_Result, DIMAPLUSReportEntity>();
            var _lst = Mapper.Map<List<DASM_GET_TRANSFERRESULT_SP_Result>, List<DIMAPLUSReportEntity>>(lst);
            return _lst;
        }


        public bool CopySlicedData(string client_ID, string project_ID, string ToolID, long? template_ID, long? RoleId, string UserName, ref string message, ref string status_code)
        {
            _dimaplus.CopySlicedData(client_ID, project_ID, ToolID, template_ID, RoleId, UserName, ref message, ref status_code);
            return true;
        }


        public List<DIMAPLUSCriteriaEntity> GetDeleteList(int page, int rows, string client_ID, string project_ID, long? TemplateId, long? Run_ID, ref string status_Code, ref string message)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetDeleteList(page, rows, client_ID, project_ID, TemplateId, Run_ID, ref StatusCode, ref Message);

            Mapper.CreateMap<DASM_GET_DELETELIST_SP_Result, DIMAPLUSCriteriaEntity>()
                .ForMember(entity => entity.Objects, dbEntity => dbEntity.MapFrom(src => src.Object_Name))
                .ForMember(entity => entity.SlicingField, dbEntity => dbEntity.MapFrom(src => src.Slicing_Field))
                .ForMember(entity => entity.SlicingValue, dbEntity => dbEntity.MapFrom(src => src.Slicing_Value))
                .ForMember(entity => entity.SourceDelete, dbEntity => dbEntity.MapFrom(src => src.Source_Delete));
            var _lst = Mapper.Map<List<DASM_GET_DELETELIST_SP_Result>, List<DIMAPLUSCriteriaEntity>>(lst);

            return _lst;
        }
        public void PurgeData(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            _dimaplus.PurgeData(Criteria, ref  StatusCode, ref  Message);
        }

        public List<DASEMTemplateEntity> GetAllTemplates(string client_ID, string project_ID, long? RoleId, ref string status_Code, ref string message)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetAllTemplates(client_ID, project_ID, RoleId, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_TEMPLATE_SP_Result, DASEMTemplateEntity>();
            var _lst = Mapper.Map<List<DASM_GET_TEMPLATE_SP_Result>, List<DASEMTemplateEntity>>(lst);
            return _lst;
        }

        public List<DASEMSliceRunIDsEntity> GetRunIDList(string client_ID, string project_ID, long? Template_ID, ref string status_Code, ref string message)
        {
            string StatusCode = string.Empty; string Message = string.Empty;
            var lst = _dimaplus.GetRunIDList(client_ID, project_ID, Template_ID, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_SLICING_RUNID_SP_Result, DASEMSliceRunIDsEntity>();
            var _lst = Mapper.Map<List<DASM_GET_SLICING_RUNID_SP_Result>, List<DASEMSliceRunIDsEntity>>(lst);
            return _lst;
        }
        //JA:20170313 
        public List<DASEMTemplateEntity> GetAllMaskingTemplates(string client_ID, string project_ID, long? RoleId, ref string status_Code, ref string message)
        {
            string StatusCode = string.Empty; string Message = string.Empty;

            var lst = _dimaplus.GetAllMaskingTemplates(client_ID, project_ID, RoleId, ref StatusCode, ref Message);
            Mapper.CreateMap<DASM_GET_MASKING_TEMPLATE_SP_Result, DASEMTemplateEntity>();
            var _lst = Mapper.Map<List<DASM_GET_MASKING_TEMPLATE_SP_Result>, List<DASEMTemplateEntity>>(lst);
            return _lst;
        }

        public void SaveMaskingTemplate(DataTable Masking, long? RoleId, ref string StatusCode, ref string Message)
        {
            _dimaplus.SaveMaskingTemplate(Masking, RoleId,ref StatusCode, ref Message);
        }

        public void SaveTemplate(DataTable Criteria, ref string StatusCode, ref string Message)
        {
            _dimaplus.SaveTemplate(Criteria, ref StatusCode, ref Message);
        }
        public DataTable GetObjectDetails(DIMAPLUSCriteriaEntity _params, ref string status_Code, ref string message)
        {
            return _dimaplus.GetObjectDetails(_params, ref status_Code, ref message);
        }

        public bool DBDeployment(string client_ID, string project_ID, string ToolID, string ConnectionString, string EncryptedFile, string DecryptedFile, ref string message, ref string status_code)
        {
            return _dimaplus.DBDeployment(client_ID, project_ID, ToolID, ConnectionString, EncryptedFile, DecryptedFile, ref message, ref status_code);
        }
        public void EncryptFile(string file, string fileEncrypted)
        {
            _dimaplus.EncryptFile(file, fileEncrypted);
        }
        public void CheckCopyCount(string client_ID, string project_ID, long? template_ID, ref string message, ref string status_Code)
        {
            _dimaplus.CheckCopyCount(client_ID, project_ID, template_ID, ref message, ref status_Code);
        }
    }
}
