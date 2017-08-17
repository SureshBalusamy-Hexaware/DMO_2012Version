using AutoMapper;
using DM_BusinessEntities;
using DM_DataModel;
using DM_DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM_BusinessService
{
    public class DataReconService : IDataRecon
    {
        private readonly DataRecon _recon;

        public DataReconService()
        {
            _recon = new DataRecon();
        }

        public List<DataReconEntity> GetTableColumns(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string srcConfig_ID, string tgtConfig_ID, ref string status_Code, ref string message)
        {
            var TableColumn = _recon.GetTableColumns(client_ID, project_ID, srcTable_Name, tgtTable_Name, srcConfig_ID, tgtConfig_ID, ref status_Code, ref message);

            if (TableColumn != null)
            {
                Mapper.CreateMap<DRD_GET_SOURCE_TARGET_TABLE_COLUMNS_SP_Result, DataReconEntity>();
                var _TableColumn = Mapper.Map<List<DRD_GET_SOURCE_TARGET_TABLE_COLUMNS_SP_Result>, List<DataReconEntity>>(TableColumn);
                return _TableColumn;
            }
            return null;
        }

        public List<DataReconEntity> GetTransRuleData(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string source_Column, string target_Column, ref string status_Code, ref string message)
        {
            var transRuleData = _recon.GetTransRuleData(client_ID, project_ID, srcTable_Name, tgtTable_Name, source_Column, target_Column, ref status_Code, ref message);

            if (transRuleData != null)
            {
                Mapper.CreateMap<DRD_GET_SOURCE_TARGET_TABLE_TRANS_SP_Result, DataReconEntity>();
                var _TransRuleData = Mapper.Map<List<DRD_GET_SOURCE_TARGET_TABLE_TRANS_SP_Result>, List<DataReconEntity>>(transRuleData);
                return _TransRuleData;
            }
            return null;
        }

        public List<DataReconEntity> GetMetaDataColumnsByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string Config_Id, string Table_Name, ref string status_Code, ref string message)
        {
            var columnList = _recon.GetMetaDataColumnNamesByTableName(client_ID, project_ID, database_IP, source_Target, database_Name, Config_Id, Table_Name, ref status_Code, ref message);

            if (columnList != null)
            {
                Mapper.CreateMap<COMMON_GET_TABLE_COLUMN_LIST_SP_Result, DataReconEntity>();
                var _tableColumn = Mapper.Map<List<COMMON_GET_TABLE_COLUMN_LIST_SP_Result>, List<DataReconEntity>>(columnList);
                return _tableColumn;
            }
            return null;
        }

        public List<HXRKeyColumnsEntity> GetKeyColumns(string client_ID, string project_ID, string table_name, long tool_id, ref string status_Code, ref string message)
        {
            var KeyColumns = _recon.GetKeyColumns(client_ID, project_ID, table_name, tool_id, ref status_Code, ref message);
            if (KeyColumns != null)
            {
                Mapper.CreateMap<COMMON_GET_KEY_COLUMNS_SP_Result, HXRKeyColumnsEntity>();
                var _KeyColumnsModel = Mapper.Map<List<COMMON_GET_KEY_COLUMNS_SP_Result>, List<HXRKeyColumnsEntity>>(KeyColumns);
                return _KeyColumnsModel;
            }
            return null;
        }

        public List<string> GetTemplateNameList(string client_ID, string project_ID, long tool_ID, long? RoleId)
        {
            var ProfilerTemplate = _recon.GetTemplateNameList(client_ID, project_ID, tool_ID, RoleId);
            return ProfilerTemplate;
        }

        public List<TemplateDataEntity> GetTemplateDetails(string client_ID, string project_ID, string template_name, ref string status_Code, ref string message)
        {
            var templateDetails = _recon.GetTemplateDetails(client_ID, project_ID, template_name, ref status_Code, ref message);
            if (templateDetails != null)
            {
                Mapper.CreateMap<DRD_GET_TEMPLATE_DETAILS_SP_Result, TemplateDataEntity>();
                var _templateDetailsModel = Mapper.Map<List<DRD_GET_TEMPLATE_DETAILS_SP_Result>, List<TemplateDataEntity>>(templateDetails);
                return _templateDetailsModel;
            }
            return null;
        }

        public string CompareData(ComparisionData reconcileData, string modifiedBy, long tool_ID, long srcConfig_ID, long tgtConfig_ID, long? RoleId)
        {

            //int status = 0;
            string StatusCode = string.Empty, Message = string.Empty;

            try
            {
                var result = reconcileData.SourceData
                            .Where(source => reconcileData.TargetData
                            .Any(target => source.SeqNo == target.SeqNo)
                            && reconcileData.SourceData.Count() == reconcileData.TargetData.Count());

                var keyResult = reconcileData.SourceKeyColumn
                            .Where(source => reconcileData.TargetKeyColumn
                            .Any(target => source.SeqNo == target.SeqNo)
                            && reconcileData.SourceKeyColumn.Count() == reconcileData.TargetKeyColumn.Count());

                if (keyResult.Count() != reconcileData.SourceKeyColumn.Count())
                {
                    return "Invalid Key column selection.";
                }

                ///check Count of both source and target selected rows and seq numbers are equal
                if (result.Count() != reconcileData.SourceData.Count() || (reconcileData.SourceData.Count() < 1 | reconcileData.TargetData.Count() < 1))
                {
                    return "Invalid column selection. Please select appropriate column for comparision.";
                }
                else
                {
                    int maxlen = reconcileData.SourceData.Max(x => x.SeqNo.Length);

                    var srcData = reconcileData.SourceData.OrderBy(s => s.SeqNo.PadLeft(maxlen, '0')).ThenBy(k => k.KeyColumn).ToList();
                    var tgtData = reconcileData.TargetData.OrderBy(s => s.SeqNo.PadLeft(maxlen, '0')).ThenBy(k => k.KeyColumn).ToList();

                    var srcKeyColumn = reconcileData.SourceKeyColumn.Select(x => x.ColumnName).ToList();
                    var tgtKeyColumn = reconcileData.TargetKeyColumn.Select(x => x.ColumnName).ToList();

                    var uniqKeyColumn = srcKeyColumn.Union(tgtKeyColumn).ToList();

                    DataTable dt = TemplateMS(reconcileData.ClientID, reconcileData.ProjectID, tool_ID, reconcileData.TemplateName, srcData, tgtData, uniqKeyColumn, modifiedBy);

                    if (srcKeyColumn.Count > 0)
                        InsertKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, srcConfig_ID, srcData[0].TableName, modifiedBy, srcKeyColumn, ref StatusCode, ref Message);
                    if (tgtKeyColumn.Count > 0)
                        InsertKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, tgtConfig_ID, tgtData[0].TableName, modifiedBy, tgtKeyColumn, ref StatusCode, ref Message);

                    _recon.InsertReconcileData(dt, ref StatusCode, ref Message);

                    if (StatusCode == "0")
                    {
                        _recon.CompareData(reconcileData.ClientID, reconcileData.ProjectID, reconcileData.TemplateName, tool_ID, RoleId, modifiedBy, ref StatusCode, ref Message);

                        if (StatusCode == "1")
                            return "Data reconcillation completed successfully.";
                        else
                            return "Data reconcillation failed. Error: " + Message;
                    }
                    else
                    {
                        return "Data reconcillation failed. Error: " + Message;
                    }
                }
            }
            catch (Exception _e)
            {
                return "Data reconcillation failed. Error: " + _e.InnerException.Message;
            }

        }

        public string SaveData(ComparisionData reconcileData, string modifiedBy, long tool_ID, long srcConfig_ID, long tgtConfig_ID)
        {
            //int status = 0;
            string StatusCode = string.Empty, Message = string.Empty;

            try
            {
                //var result = reconcileData.SourceData
                //            .Where(source => reconcileData.TargetData
                //            .Any(target => source.SeqNo == target.SeqNo)
                //            && reconcileData.SourceData.Count() == reconcileData.TargetData.Count());

                //var keyResult = reconcileData.SourceKeyColumn
                //            .Where(source => reconcileData.TargetKeyColumn
                //            .Any(target => source.SeqNo == target.SeqNo)
                //            && reconcileData.SourceKeyColumn.Count() == reconcileData.TargetKeyColumn.Count());

                //if (keyResult.Count() != reconcileData.SourceKeyColumn.Count())
                //{
                //    return "Invalid Key column selection.";
                //}

                /////check Count of both source and target selected rows and seq numbers are equal
                //if (result.Count() != reconcileData.SourceData.Count() || (reconcileData.SourceData.Count() < 1 | reconcileData.TargetData.Count() < 1))
                //{
                //    return "Invalid column selection. Please select appropriate column for comparision.";
                //}
                //else
                //{
                //Getting the maximum length of string in list
                int maxlen = reconcileData.SourceData.Max(x => x.SeqNo.Length);

                //For sorting the length count of '0' added on left side of the value.
                //var srcData = reconcileData.SourceData.OrderBy(s => s.SeqNo.PadLeft(maxlen, '0')).ThenBy(k => k.KeyColumn).ToList();
                //var tgtData = reconcileData.TargetData.OrderBy(s => s.SeqNo.PadLeft(maxlen, '0')).ThenBy(k => k.KeyColumn).ToList();

                //Retriving only the column name from the lists.
                var srcKeyColumn = reconcileData.SourceKeyColumn.Select(x => x.ColumnName).ToList();
                var tgtKeyColumn = reconcileData.TargetKeyColumn.Select(x => x.ColumnName).ToList();

                var uniqKeyColumn = srcKeyColumn.Union(tgtKeyColumn).ToList();

                DataTable dt = TemplateMS(reconcileData.ClientID, reconcileData.ProjectID, tool_ID, reconcileData.TemplateName, reconcileData.SourceData, reconcileData.TargetData, uniqKeyColumn, modifiedBy);

                string isKey = "";
                if (srcKeyColumn.Count > 0)
                {
                    this.ValidateKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, reconcileData.SourceData[0].TableName, (int)srcConfig_ID, srcKeyColumn, ref isKey, ref StatusCode, ref Message);
                    if (isKey == "True")
                        InsertKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, srcConfig_ID, reconcileData.SourceData[0].TableName, modifiedBy, srcKeyColumn, ref StatusCode, ref Message);
                    else
                        return "Invalid Source key column selection.";

                }
                if (tgtKeyColumn.Count > 0)
                {
                    isKey = "";
                    this.ValidateKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, reconcileData.TargetData[0].TableName, (int)tgtConfig_ID, tgtKeyColumn, ref isKey, ref StatusCode, ref Message);
                    if (isKey == "True")
                        InsertKeyColumns(reconcileData.ClientID, reconcileData.ProjectID, tgtConfig_ID, reconcileData.TargetData[0].TableName, modifiedBy, tgtKeyColumn, ref StatusCode, ref Message);
                    else
                        return "Invalid target key column selection.";
                }

                _recon.InsertReconcileData(dt, ref StatusCode, ref Message);

                if (StatusCode == "0")
                {
                    return StatusCode;
                }
                else
                {
                    return "Data reconcillation failed. Error: " + Message;
                }
                // }
            }
            catch (Exception _e)
            {
                string errMsg = _e.InnerException != null ? _e.InnerException.Message : _e.Message;
                return "Data reconcillation failed. Error: " + errMsg;
            }
        }

        public string CompareData(string client_Id, string project_id, string template_name, string modifiedBy, long tool_ID, long? RoleId, ref List<TemplateDataEntity> lst)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var dtResult = _recon.CompareData(client_Id, project_id, template_name, tool_ID, RoleId, modifiedBy, ref StatusCode, ref Message);

            lst = (from drt in dtResult.AsEnumerable()
                   select new TemplateDataEntity
                   {
                       Source_Table_Name = drt.Field<string>("Source_Table_Name"),
                       Source_Column_Name = drt.Field<string>("Source_Column_Name"),
                       Target_Table_Name = drt.Field<string>("Target_Table_Name"),
                       Target_Column_Name = drt.Field<string>("Target_Column_Name"),
                       Error_Count = drt.Field<long>("Error_Count"),
                       Status = Convert.ToString(drt.Field<long>("Error_Count")),
                       Run_ID = Convert.ToInt64("0" + drt["Run_ID"])
                   }).ToList();


            if (StatusCode == "1")
                return "Data reconcillation completed successfully.";
            else
                return "Data reconcillation failed. Error: " + Message;
        }

        private DataTable TemplateMS(string _clientID, string _projectID, long _toolID, string _templateName, List<SourceDataEntity> source, List<TargetDataEntity> target, List<string> uniqKeyColumns, string modifiedBy)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Client_ID", Type.GetType("System.String"));
            dt.Columns.Add("Project_ID", Type.GetType("System.String"));
            dt.Columns.Add("Template_Name", Type.GetType("System.String"));
            dt.Columns.Add("Source_Table_Name", Type.GetType("System.String"));
            dt.Columns.Add("Source_Column_Name", Type.GetType("System.String"));
            dt.Columns.Add("Source_Col_Seq_No", Type.GetType("System.Int64"));
            dt.Columns.Add("Source_Data_Type", Type.GetType("System.String"));
            dt.Columns.Add("Source_Expression", Type.GetType("System.String"));
            dt.Columns.Add("Target_Table_Name", Type.GetType("System.String"));
            dt.Columns.Add("Target_Column_Name", Type.GetType("System.String"));
            dt.Columns.Add("Target_Col_Seq_No", Type.GetType("System.Int64"));
            dt.Columns.Add("Target_Data_Type", Type.GetType("System.String"));
            dt.Columns.Add("Target_Expression", Type.GetType("System.String"));
            dt.Columns.Add("IS_Key_Column", Type.GetType("System.Char"));
            dt.Columns.Add("Modified_By", Type.GetType("System.String"));
            dt.Columns.Add("Tool_ID", typeof(long));

            for (int i = 0; i < source.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Client_ID"] = _clientID;
                dr["Project_ID"] = _projectID;
                dr["Template_Name"] = _templateName;
                dr["Source_Table_Name"] = source[i].TableName;
                dr["Source_Column_Name"] = source[i].ColumnName;
                dr["Source_Col_Seq_No"] = Convert.ToInt64(source[i].SeqNo);
                dr["Source_Data_Type"] = source[i].DataType;
                dr["Source_Expression"] = source[i].Expression;

                dr["Target_Table_Name"] = target[i].TableName;
                dr["Target_Column_Name"] = target[i].ColumnName;
                dr["Target_Col_Seq_No"] = Convert.ToInt64(target[i].SeqNo);
                dr["Target_Data_Type"] = target[i].DataType;
                dr["Target_Expression"] = target[i].Expression;
                dr["IS_Key_Column"] = source[i].KeyColumn;
                dr["Modified_By"] = modifiedBy;
                dr["Tool_ID"] = _toolID;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public void ValidateKeyColumns(string client_ID, string project_ID, string table_name, int Config_Id, List<string> keyColumnList, ref string is_key_columns, ref string status_Code, ref string message)
        {
            string key1 = string.Empty, key2 = string.Empty, key3 = string.Empty, key4 = string.Empty, key5 = string.Empty;

            key1 = keyColumnList.Count > 0 ? keyColumnList[0] : null;
            key2 = keyColumnList.Count > 1 ? keyColumnList[1] : null;
            key3 = keyColumnList.Count > 2 ? keyColumnList[2] : null;
            key4 = keyColumnList.Count > 3 ? keyColumnList[3] : null;
            key5 = keyColumnList.Count > 4 ? keyColumnList[4] : null;

            _recon.ValidateKeyColumns(client_ID, project_ID, table_name, Config_Id, key1, key2, key3, key4, key5, ref is_key_columns, ref status_Code, ref message);
        }

        private void InsertKeyColumns(string client_id, string project_id, long config_id, string table_name, string modifiedBy, List<string> keyColumnList, ref string StatusCode, ref string Message)
        {
            string key1 = string.Empty, key2 = string.Empty, key3 = string.Empty, key4 = string.Empty, key5 = string.Empty;

            key1 = keyColumnList.Count > 0 ? keyColumnList[0] : "";
            key2 = keyColumnList.Count > 1 ? keyColumnList[1] : "";
            key3 = keyColumnList.Count > 2 ? keyColumnList[2] : "";
            key4 = keyColumnList.Count > 3 ? keyColumnList[3] : "";
            key5 = keyColumnList.Count > 4 ? keyColumnList[4] : "";

            _recon.InsertKeyColumns(client_id, project_id, config_id, table_name, modifiedBy, ref StatusCode, ref Message, key1, key2, key3, key4, key5);
        }

        public void ValidateExpression(string client_ID, string project_ID, long config_ID, string table_name, string Expression, ref string status_Code, ref string message)
        {
            _recon.ValidateExpression(client_ID, project_ID, config_ID, table_name, Expression, ref status_Code, ref message);
        }

        public List<DataReconSourceTargetEntity> GetMetaDataTableDetail(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message)
        {
            var GetSource = _recon.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<COMMON_GET_META_DATA_SP_Result, DataReconSourceTargetEntity>();
                var _GetSourceModel = Mapper.Map<List<COMMON_GET_META_DATA_SP_Result>, List<DataReconSourceTargetEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }

        public List<TemplateDataEntity> GetDetailErrorStatus(long runID, string columnName)
        {
            var getData = _recon.GetDetailErrorStatus(runID, columnName);

            if (getData != null)
            {
                Mapper.CreateMap<DRD_GET_DATARECON_REPORT_SP_Result, TemplateDataEntity>();
                var _GetData = Mapper.Map<List<DRD_GET_DATARECON_REPORT_SP_Result>, List<TemplateDataEntity>>(getData);
                return _GetData;
            }
            return null;
        }

    }


}
