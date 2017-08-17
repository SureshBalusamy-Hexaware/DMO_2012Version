using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;
using System.Data;

namespace DM_BusinessService
{
    public interface IDASEM
    {
        List<DASEMTemplateEntity> GetAllTemplates(string client_ID, string project_ID,long? RoleId, ref string status_Code, ref string message);
        //JA:20170313
        List<DASEMTemplateEntity> GetAllMaskingTemplates(string client_ID, string project_ID,long? RoleId, ref string status_Code, ref string message);
        //List<DASEMTemplateEntity> GetAllMaskingTemplateDetails(string client_ID, string project_ID, string Template_ID, string Tool_ID, ref string status_Code, ref string message);
        void SaveMaskingTemplate(DataTable Masking, ref string StatusCode, ref string Message);

        List<string> GetSlicingColumns(string client_ID, string project_ID, string constraint_Type, long config_ID, ref string status_Code, ref string message);
        List<string> GetSlicingColumnValues(string client_ID, string project_ID, string Column_name, int Config_ID, ref string StatusCode, ref string Message);
        DataTable GetCriteria(int page, int rows, string client_ID, string project_ID, string Object_Type, string Template_Name, long config_ID, string Column_name,
            string SlicingValue, int ToolId, string Expression, ref string StatusCode, ref string Message);
        string GetCriteria(string Type, ref string StatusCode, ref string Message);
        void SaveUpdateCriteria(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message);
        void UpdateCriteriaSourceDelete(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message);
        List<DIMAPLUSCriteriaEntity> GetAllCriteria(int page, int rows, string client_ID, string project_ID, ref string status_Code, ref string message);

        List<DIMAPLUSCopySliceEntity> GetSourceObjectDetails(string client_ID, string project_ID, long? Template_Id);
        List<DIMAPLUSCopySliceEntity> GetTargetServerDetails(string client_ID, string project_ID,long? RoleId);
        bool CopySlicedData(string client_ID, string project_ID, string ToolID, long? template_ID, long? RoleId, string UserName, ref string message, ref string status_Code);
        List<DIMAPLUSReportEntity> GetTransferResultReport(string client_ID, string project_ID, long? Template_ID, long? Run_ID);
        List<DIMAPLUSReportEntity> GetAuditReport(string client_ID, string project_ID, long? Template_ID);
        List<DIMAPLUSCriteriaEntity> GetDeleteList(int page, int rows, string client_ID, string project_ID, long? TemplateId, long? Run_ID, ref string status_Code, ref string message);
        void PurgeData(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message);

        void SaveTemplate(DataTable Criteria, ref string StatusCode, ref string Message);
        DataTable GetObjectDetails(DIMAPLUSCriteriaEntity _params, ref string status_Code, ref string message);

        List<DASEMSliceRunIDsEntity> GetRunIDList(string _ClientID, string _ProjectID, long? Template_ID, ref string StatusCode, ref string Message);
        bool DBDeployment(string client_ID, string project_ID, string ToolID, string ConnectionString, string EncryptedFile, string DecryptedFile, ref string message, ref string status_code);
        void EncryptFile(string file, string fileEncrypted);
        void CheckCopyCount(string client_ID, string project_ID, long? template_ID, ref string message, ref string status_Code);
    }
}
