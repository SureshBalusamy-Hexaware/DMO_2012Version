using DM_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DM_BusinessService
{
    public interface IAutomaton
    {
        List<string> GetMetaDataTableNameList(string client_ID, string project_ID, string source_Target, long Tool_Id, long? RoleId, ref string status_Code, ref string message);
        List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message);
        List<string> GetMetaDataConnectionList(string client_ID, string project_ID, long Tool_ID, long? RoleId, string Sourcetarget, ref string status_Code, ref string message);
        List<string> GetTransformationDesc(string client_ID, string project_ID, long Tool_ID, ref string status_Code, ref string message);
        List<string> GetTransSuorceTargetTable(string client_ID, string project_ID, long Tool_ID, string TemplateName, string type, ref string status_Code, ref string message);
        List<string> GetSourceTargetColumnsByTableName(string client_ID, string project_ID, long Tool_ID, string TemplateName, string Table_Name, string type, ref string status_Code, ref string message);
        List<AutomatonSourceEntity> GetMetaDataTableDetail(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message);
        List<TransformEntity> GetTransTableData(string client_ID, string project_ID, string TemplateName, ref string status_Code, ref string message);
        List<TemplateSourceTargetEntity> GetSourceTemplateList(string Client_ID, string Project_ID, string Template_id, string Template_Name, string sourceTargettype, ref string status_Code, ref string message);
        List<TemplateSourceTargetEntity> GetTargetTemplateList(string Client_ID, string Project_ID, string Template_id, string Template_Name, string sourceTargettype, ref string status_Code, ref string message);
        List<TemplateSourceTargetEntity> GetTemplateSourceTargetTableList(string client_ID, string project_ID, string Template_Id, ref string status_Code, ref string message);
        List<TransformEntity> GetTransTemplateList(string Client_ID, string Project_ID, string Template_id, ref string status_Code, ref string message);
        void SaveSourceGrid(List<SourceEntity> entity, ref string Source_code, ref string Message);
        void SaveTargetGrid(List<TargetEntity> targetentity, ref string status_Code, ref string message);
        void SaveTransGrid(List<TransformEntity> targetentity, ref string status_Code, ref string message);
        void TransAdd(TransitionAdd transentity, ref string status_Code, ref string message);
        void ModifySourceGrid(List<SourceEntity> sourceentity, ref string status_Code, ref string message);
        void ModifyTargetGrid(List<TargetEntity> targetentity, ref string status_Code, ref string message);
        void ModifyTransGrid(List<TransformEntity> transentity, ref string status_Code, ref string message);
        void DeleteSourceGrid(long RowID, ref string Source_code, ref string Message);
        void DeleteTargetGrid(long RowID, ref string Source_code, ref string Message);
        void DeleteTransGrid(long Trans_ID, ref string Source_code, ref string Message);
        List<string> GetTemplateList(string client_id, string Project_id, string Type, ref string status_Code, ref string message);
        List<string> GetTemplateSourceTableList(string client_id, string Project_id, string Template_Name, ref string status_Code, ref string message);
        List<string> GetTemplateTargetTableList(string client_id, string Project_id, string Template_Name, string type, ref string status_Code, ref string message);
        List<string> GetTemplateTransTableList(string client_id, string Project_id, string Template_Name, string type, ref string status_Code, ref string message);
        List<TemplateList> GetTemplateNameList(string client_id, string Project_id, string Type, ref string Source_code, ref string Message);
        void CopyTemplate(string client_ID, string project_ID, long OldTemplateID, string NewTemplateName, string CreatedBy, ref long New_Template_ID, ref string status_Code, ref string message);
        List<TransformEntity> GetColumnDetail(string client_ID, string project_ID, string TemplateName, string TableName, string FieldName, ref string status_Code, ref string message);
        DataTable GetBatchData(string client_ID, string project_ID, long? config_ID, string table_name, string BatchID, int page_no, int recordsperpage, ref string status_Code, ref string message, ref long TotalCount);
        List<AutomatonExportStatus> GetExportStatus(AutomatonExportStatus _s, ref string status_Code, ref string message);
        void SubmitDownloadRequest(AutomatonExportStatus _s, ref string status_Code, ref string message);
        DataTable GetTableData(string client_ID, string project_ID, string config_ID, string table_name, string ColumnList, byte IsDistinct, ref string status_Code, ref string message);
        void LogRunAudit(string client_ID, string project_ID, string TemplateName, string Modified_by, long? Run_ID, ref long Run_ID_OUTPUT, long? RoleId, ref string status_Code, ref string message);
        void LogExcelRunAudit(string client_ID, string project_ID, string Tool_ID, string Source_Table_Name, string Source_Column_count, string Source_Record_Count, long? Batch_ID, string Upload_status,
            string Target_Table_name, string Modified_by, long? Run_ID, ref long Run_ID_OUTPUT, long? RoleId, ref string status_Code, ref string message);
        DataTable Get_Mask_Table_Columns(string client_ID, string project_ID, string table_name, long? config_ID, DataTable Temptable, ref  string status_Code, ref string message);
        List<MaskEntity> GetMaskTypes(string column_Type, ref string status_Code, ref string message);
        List<BusinessNameEntity> GetEntityList(string client_ID, string project_ID, long Config_ID, ref string status_Code, ref string message);
        List<BusinessColumnEntity> GetEntityColList(string client_ID, string project_ID, Nullable<long> entity_ID, long Config_ID, ref string status_Code, ref string message);
        string SaveEntityName(List<BusinessNameEntity> lstBname);
        void SaveEntityColData(DataTable dt, ref string status_Code, ref string message);
        List<ParametersEntity> GetTemplateParameterList(string client_ID, string project_ID, string templateName, ref string status_Code, ref string message);
        void SaveParameter(ParametersEntity entity, ref string Source_code, ref string Message);
        void DeleteParameter(long ParameterID, string ModifiedBy, ref string status_Code, ref string Message);
        long InsertTemplate(string client_ID, string project_ID, string TemplateName, string Created_By, string Type);
        void InsertOfflineBatchJobs(string client_ID, string project_ID, long? ToolID, string TemplateName, string TemplatePath, string Modified_by, string RunStatus, string JobDescription, Nullable<DateTime> scheduled_date, ref string status_Code, ref string message);
        DataTable GetAutomatonBatchStatus(string client_id, string project_id, long? ToolID);
        List<AutomatonSourceEntity> GetMetaDataTableBusinessName(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message);
        List<BusinessEntitySuggestedAttributes> GetEntitySuggestedAttr(string client_id, string project_id, string table_name, string column_name, ref string status_Code, ref string message);
        List<BusinessEntityTargetDBDataType> GetTargetDBDataTypes(string client_id, string project_id, Nullable<long> config_ID, ref string status_Code, ref string message);
        int GetTargetDBtblClmnCount(string client_ID, string project_ID, Nullable<long> config_ID, string table_Name,
            ref Nullable<long> clmnCnt, ref string status_Code, ref string message);


        void GenerateDesignDocument(string client_id, string project_id, long ToolID, long template_id, string template_name, string designTemplate, long? RoleId, ref string status_Code, ref string message);
        List<SchedulerEntity> GetScheduledTransformation(string client_ID, string project_ID, Nullable<long> tool_ID, string trans_type, ref string status_Code, ref string message);
        string UpdateScheduleTransformation(long jobID, Nullable<DateTime> scheduleDate, char isDel, ref string status_Code, ref string message);
        //void GenerateDRTemplate(string Template_ID);
        //void SaveFileUploadTemplate(string client_ID, string project_ID, AutomatonFileUploadTemplateEntity entity, ref string StatusCode, ref string Message);
        void SaveFileUploadTemplate(AutomatonFileUploadTemplateEntity entity, ref string Template_ID, ref string StatusCode, ref string Message);
        void CheckETLThresholdLimit(string client_ID, string project_ID, Nullable<long> template_ID, ref string StatusCode, ref string Message);
        int DateColumnCheck(string client_ID, string project_ID, string table_Name, string column_name, Nullable<long> config_ID, string columnType, ref string status_Code,
            ref string message);
        List<AutomatonSourceEntity> GetMetaDataTableDetailAutomatic(string client_ID, string project_ID, long? template_ID, long? Role_ID, string Table_name, string Flag_type, string connectionid, ref string status_Code, ref string message);
        void GenerateReconcile(string client_ID, string project_ID, string template_ID, long? RoleId, string UpdatedBy, ref string StatusCode, ref string Message);
    }
}

