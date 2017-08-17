using DM_BusinessEntities;
using System.Collections.Generic;

namespace DM_BusinessService
{
    public interface IDataRecon
    {
        List<DataReconEntity> GetTableColumns(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string srcConfig_ID, string tgtConfig_ID, ref string status_Code, ref string message);
        List<DataReconEntity> GetTransRuleData(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string source_Column, string target_Column, ref string status_Code, ref string message);
        //int LoadCompareDataToDOMTable(string client_ID, string project_ID, string srcTable_name, string tgtTable_name, string mapping_name, string srcConfig_ID, string tgtConfig_ID, string created_by, ref string status_Code, ref string message);
        //int UpdateAsCriticalColumnInDOMTable(string client_ID, string project_ID, string table_name, string columns_list, string created_by, ref string status_Code, ref string message);
        //void CompareData(string client_ID, string project_ID, string srcTable_name, string tgtTable_name, string column_list, string mapping_name, string srcConfig_ID, string tgtConfig_ID, string created_by, ref string status_Code, ref string message);
        List<DataReconEntity> GetMetaDataColumnsByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string Config_Id, string Table_Name, ref string status_Code, ref string message);
        List<HXRKeyColumnsEntity> GetKeyColumns(string client_ID, string project_ID, string table_name, long tool_id, ref string status_Code, ref string message);
        List<string> GetTemplateNameList(string client_ID, string project_ID, long tool_ID,long? RoleId);
        List<TemplateDataEntity> GetTemplateDetails(string client_ID, string project_ID, string template_name, ref string status_Code, ref string message);
        //string CompareData(ComparisionData reconsileData, string userName, long tool_ID, long srcConfig_ID, long tgtConfig_ID);
        //string CompareData(ComparisionData reconcileData, string modifiedBy, long tool_ID, long srcConfig_ID, long tgtConfig_ID, ref List<TemplateDataEntity> lst);
        string SaveData(ComparisionData reconcileData, string modifiedBy, long tool_ID, long srcConfig_ID, long tgtConfig_ID);
        string CompareData(string client_Id, string project_id, string template_name, string modifiedBy, long tool_ID, long? RoleId, ref List<TemplateDataEntity> lst);
        void ValidateExpression(string client_ID, string project_ID, long config_ID, string table_name, string Expression, ref string status_Code, ref string message);
        List<DataReconSourceTargetEntity> GetMetaDataTableDetail(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message);
        List<TemplateDataEntity> GetDetailErrorStatus(long runID, string columnName);
    }
}
