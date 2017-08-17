using AutoMapper;
using DM_BusinessEntities;
using DM_DataModel;
using DM_DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode; //Docx library for creating ETL design document

namespace DM_BusinessService
{
    public class HXRAutomatonService : IAutomaton
    {

        private readonly HXRAutomaton _autoMS;

        public HXRAutomatonService()
        {
            _autoMS = new HXRAutomaton();
        }
        public List<string> GetMetaDataConnectionList(string client_ID, string project_ID, long Tool_ID, long? RoleId, string SourceTarget, ref string Source_code, ref string Message)
        {
            return _autoMS.GetMetaDataConnectionList(client_ID, project_ID, Tool_ID, SourceTarget, ref Source_code, RoleId, ref Message);
        }
        public List<string> GetMetaDataTableNameList(string client_ID, string project_ID, string source_Target, long Tool_Id, long? RoleId,
            ref string status_Code, ref string message)
        {
            return _autoMS.GetMetaDataTableNameList(client_ID, project_ID, source_Target, Tool_Id, RoleId, ref status_Code, ref message);
        }
        public List<string> GetTransformationDesc(string client_ID, string project_ID, long Tool_Id, ref string status_Code, ref string message)
        {
            return _autoMS.GetTransformationDesc(client_ID, project_ID, Tool_Id, ref status_Code, ref message);
        }
        public List<string> GetTransSuorceTargetTable(string client_ID, string project_ID, long Tool_Id, string TemplateName, string type, ref string status_Code, ref string message)
        {
            return _autoMS.GetTransSourceTargetTable(client_ID, project_ID, TemplateName, type, ref status_Code, ref message);
        }
        public List<string> GetSourceTargetColumnsByTableName(string client_ID, string project_ID, long Tool_Id, string TemplateName, string TableName, string type, ref string status_Code, ref string message)
        {
            return _autoMS.GetSourceTargetColumnsByTableName(client_ID, project_ID, TemplateName, TableName, type, ref status_Code, ref message);
        }
        public List<string> GetTemplateSourceTableList(string client_ID, string project_ID, string TemplateName, ref string status_Code, ref string message)
        {
            return _autoMS.GetTemplateSourceTableList(client_ID, project_ID, TemplateName, ref status_Code, ref message);
        }
        public List<string> GetTemplateTargetTableList(string client_ID, string project_ID, string TemplateName, string type, ref string status_Code, ref string message)
        {
            return _autoMS.GetTransSourceTargetTable(client_ID, project_ID, TemplateName, type, ref status_Code, ref message);
        }
        public List<string> GetTemplateTransTableList(string client_ID, string project_ID, string TemplateName, string type, ref string status_Code, ref string message)
        {
            return _autoMS.GetTransSourceTargetTable(client_ID, project_ID, TemplateName, type, ref status_Code, ref message);
        }
        public List<AutomatonSourceEntity> GetMetaDataTableDetail(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<COMMON_GET_META_DATA_SP_Result, AutomatonSourceEntity>();
                var _GetSourceModel = Mapper.Map<List<COMMON_GET_META_DATA_SP_Result>, List<AutomatonSourceEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }
        public List<TransformEntity> GetTransTableData(string client_ID, string project_ID, string TemplateName, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetTransTableData(client_ID, project_ID, TemplateName, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<ETL_GET_COLUMN_DATA_TYPE_SP_Result, TransformEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_COLUMN_DATA_TYPE_SP_Result>, List<TransformEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }
        public List<TemplateSourceTargetEntity> GetSourceTemplateList(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype, ref string status_Code, ref string message)
        {

            var GetSource = _autoMS.GetSourceTemplateList(client_ID, project_ID, Template_Id, Template_Name, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<ETL_GET_SOURCE_TEMPLATE_SP_Result, TemplateSourceTargetEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_SOURCE_TEMPLATE_SP_Result>, List<TemplateSourceTargetEntity>>(GetSource);
                return _GetSourceModel;
            }

            return null;
        }
        public List<TemplateSourceTargetEntity> GetTemplateSourceTargetTableList(string client_ID, string project_ID, string Template_Id, ref string status_Code, ref string message)
        {

            var lstsourceModel = _autoMS.GetTemplateSourceTargetTableList(client_ID, project_ID, Template_Id, ref status_Code, ref message);
            if (lstsourceModel != null)
            {
                Mapper.CreateMap<ETL_GET_TEMPLATE_SOURCE_TARGET_TABLE_SP_Result, TemplateSourceTargetEntity>();
                var mModel = Mapper.Map<List<ETL_GET_TEMPLATE_SOURCE_TARGET_TABLE_SP_Result>, List<TemplateSourceTargetEntity>>(lstsourceModel);
                return mModel;
            }

            return null;
        }

        public List<TemplateSourceTargetEntity> GetTargetTemplateList(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetTargetTemplateList(client_ID, project_ID, Template_Id, Template_Name, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<ETL_GET_TARGET_TEMPLATE_SP_Result, TemplateSourceTargetEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_TARGET_TEMPLATE_SP_Result>, List<TemplateSourceTargetEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }
        public List<TransformEntity> GetTransTemplateList(string client_ID, string project_ID, string Template_Id, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetTransTemplateList(client_ID, project_ID, Template_Id, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<ETL_GET_TRANS_TEMPLATE_SP_Result, TransformEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_TRANS_TEMPLATE_SP_Result>, List<TransformEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }
        public List<string> GetTemplateList(string client_id, string Project_id, string Type, ref string Source_code, ref string Message)
        {
            return _autoMS.GetTemplateList(client_id, Project_id, Type, ref Source_code, ref Message);
        }
        public void SaveSourceGrid(List<SourceEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            string tempid = string.Empty;

            foreach (var Field in entity)
            {
                if (Convert.ToInt32(Field.Row_ID) <= 0)
                {
                    if (Convert.ToString(Field.Field_Name).ToLower() == "ignore" || Convert.ToString(Field.Field_Seq_No) == "0")
                    {
                        continue;
                    }
                    _autoMS.SaveSourceGrid(Field.Client_ID, Field.Project_ID, Field.Connection_ID, Convert.ToInt64(Field.Template_ID), Field.Template_Name, Field.Table_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Data_Type, Field.Field_Name, Field.Field_Type, Field.Field_Data, Field.Created_By, ref Source_code, ref Message);
                }
                else
                {
                    if (Convert.ToString(Field.Field_Name).ToLower() == "ignore" || Convert.ToString(Field.Field_Seq_No) == "0")
                    {
                        _autoMS.DeleteSourceGrid(Convert.ToInt64(Field.Row_ID), ref Source_code, ref Message);
                    }
                    else
                    {
                        _autoMS.ModifySourceGrid(Field.Client_ID, Field.Project_ID, Convert.ToInt64(Field.Row_ID), Field.Table_Name, Field.Connection_ID, Field.Template_ID, Field.Template_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Name, Field.Field_Data_Type, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Type, Field.Field_Data, Field.Business_name, Field.Modified_by, ref Source_code, ref Message);
                    }

                }
            }


        }
        public void SaveTargetGrid(List<TargetEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            foreach (var Field in entity)
            {
                if (Convert.ToInt32(Field.Row_ID) <= 0)
                {

                    if (Convert.ToString(Field.Field_Name).ToLower() == "ignore" || Convert.ToString(Field.Field_Seq_No) == "0")
                    {
                        continue;
                    }

                    _autoMS.SaveTargetGrid(Field.Client_ID, Field.Project_ID, Field.Connection_ID, Field.Template_ID, Field.Template_Name, Field.Table_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Data_Type, Field.Field_Name, Field.Field_Data, Field.Created_By, Field.Modified_by, ref Source_code, ref Message);
                }
                else
                {
                    if (Convert.ToString(Field.Field_Name).ToLower() == "ignore" || Convert.ToString(Field.Field_Seq_No) == "0")
                    {
                        _autoMS.DeleteTargetGrid(Convert.ToInt64(Field.Row_ID), ref Source_code, ref Message);
                    }
                    else
                    {
                        _autoMS.ModifyTargetGrid(Field.Client_ID, Field.Project_ID, Convert.ToInt64(Field.Row_ID), Field.Table_Name, Field.Connection_ID, Field.Template_ID, Field.Template_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Name, Field.Field_Data_Type, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Type, Field.Field_Data, Field.Business_name, Field.Modified_by, ref Source_code, ref Message);
                    }
                }
            }
        }
        public void SaveTransGrid(List<TransformEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            foreach (var Field in entity)
            {
                if (Convert.ToInt32(Field.Trans_ID) <= 0)
                {
                    _autoMS.SaveTransformGrid(Field.Client_ID, Field.Project_ID, Field.Template_Name, Field.Table_Name, Field.Field_Name, Field.Field_Data_Type, Field.Trans_Name, Field.Trans_Type, Field.Field_Length, Field.Trans_Order, Field.Trans_Rule, Field.Template_ID, Field.Modified_by, ref Source_code, ref Message);
                }
                else
                {
                    _autoMS.ModifyTransGrid(Field.Client_ID, Field.Project_ID, Convert.ToInt32(Field.Trans_ID), Field.Trans_Name, Field.Field_Name, Field.Trans_Rule, Field.Field_Data_Type, Field.Field_Length, Field.Trans_Type, Field.Trans_Order, Field.Source_Name, Field.Table_Name, Field.Field_Name, Field.Modified_by, ref Source_code, ref Message);
                }
            }
        }
        public void TransAdd(TransitionAdd entity, ref string Source_code, ref string Message)
        {
            _autoMS.TransAdd(entity.Client_ID, entity.Project_Id, entity.Tool_Id, entity.TemplateName, entity.TransName, entity.TransType, entity.TableName, entity.ColumnName, entity.TransRule, ref Source_code, ref Message);
        }
        public void ModifySourceGrid(List<SourceEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            foreach (var Field in entity)
            {
                long? KeyID = null;
                if (Field.Row_ID != null)
                {
                    KeyID = Convert.ToInt64(Field.Row_ID);
                }

                _autoMS.ModifySourceGrid(Field.Client_ID, Field.Project_ID, KeyID, Field.Table_Name, Field.Connection_ID, Field.Template_ID, Field.Template_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Name, Field.Field_Data_Type, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Type, Field.Field_Data, Field.Business_name, Field.Modified_by, ref Source_code, ref Message);
            }
        }
        public void ModifyTargetGrid(List<TargetEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            foreach (var Field in entity)
            {
                long? KeyID = null;
                if (Field.Row_ID != null)
                {
                    KeyID = Convert.ToInt64(Field.Row_ID);
                }

                _autoMS.ModifyTargetGrid(Field.Client_ID, Field.Project_ID, KeyID, Field.Table_Name, Field.Connection_ID, Field.Template_ID, Field.Template_Name, Field.Field_Seq_No, Field.Field_Name, Field.Field_Name, Field.Field_Data_Type, Field.Field_Prec, Field.Field_Scale, Field.Field_Key, Field.Field_Type, Field.Field_Data, Field.Business_name, Field.Modified_by, ref Source_code, ref Message);
            }
        }
        public void ModifyTransGrid(List<TransformEntity> entity, ref string Source_code, ref string Message)
        {
            List<string> list = new List<string>();
            foreach (var Field in entity)
            {
                _autoMS.ModifyTransGrid(Field.Client_ID, Field.Project_ID, Convert.ToInt32(Field.Trans_ID), Field.Trans_Name, Field.Field_Name, Field.Trans_Rule, Field.Field_Type, Field.Field_Length, Field.Trans_Type, Field.Trans_Order, Field.Source_Name, Field.Table_Name, Field.Field_Name, Field.Modified_by, ref Source_code, ref Message);
            }
        }
        public void DeleteSourceGrid(long RowID, ref string Source_code, ref string Message)
        {
            _autoMS.DeleteSourceGrid(RowID, ref Source_code, ref Message);
        }
        public void DeleteTargetGrid(long RowID, ref string Source_code, ref string Message)
        {
            _autoMS.DeleteTargetGrid(RowID, ref Source_code, ref  Message);
        }
        public void DeleteTransGrid(long Trans_ID, ref string Source_code, ref string Message)
        {
            _autoMS.DeleteTransGrid(Trans_ID, ref Source_code, ref  Message);
        }
        public List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message)
        {
            return _autoMS.GetMetaDataTableNames(client_ID, project_ID, config_ID, ref status_Code, ref message);

        }
        public DataTable GetBatchData(string client_ID, string project_ID, long? config_ID, string table_name, string BatchID, int page_no, int recordsperpage, ref string status_Code, ref string message, ref long TotalCount)
        {
            DataTable _dtData = new DataTable();
            _dtData = _autoMS.GetBatchData(client_ID, project_ID, config_ID, table_name, BatchID, page_no, recordsperpage, ref   status_Code, ref  message, ref  TotalCount);
            return _dtData;
        }
        public List<TemplateList> GetTemplateNameList(string client_id, string Project_id, string Type, ref string Source_code, ref string Message)
        {

            var _result = _autoMS.GetTemplateNameList(client_id, Project_id, Type, ref Source_code, ref Message);

            if (_result != null)
            {
                Mapper.CreateMap<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result, TemplateList>();
                var _resultModel = Mapper.Map<List<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result>, List<TemplateList>>(_result);
                return _resultModel;
            }
            return null;

        }
        public void CopyTemplate(string client_ID, string project_ID, long OldTemplateID, string NewTemplateName, string CreatedBy, ref long New_Template_ID, ref string status_Code, ref string message)
        {
            _autoMS.CopyTemplate(client_ID, project_ID, OldTemplateID, NewTemplateName, CreatedBy, ref  New_Template_ID, ref  status_Code, ref  message);
        }
        public List<TransformEntity> GetColumnDetail(string client_ID, string project_ID, string TemplateName, string TableName, string FieldName, ref string status_Code, ref string message)
        {
            var lst = _autoMS.GetColumnDetail(client_ID, project_ID, TemplateName, TableName, FieldName, ref  status_Code, ref  message);

            if (lst != null)
            {
                Mapper.CreateMap<ETL_GET_COLUMN_DETAIL_SP_Result, TransformEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_COLUMN_DETAIL_SP_Result>, List<TransformEntity>>(lst);
                return _GetSourceModel;
            }
            return null;

        }
        public List<AutomatonExportStatus> GetExportStatus(AutomatonExportStatus _s, ref string status_Code, ref string message)
        {
            var _result = _autoMS.GetExportStatus(_s, ref status_Code, ref message);

            if (_result != null)
            {
                Mapper.CreateMap<ATMTN_GET_TBL_EXPORT_STATUS_SP_Result, AutomatonExportStatus>();
                var _resultModel = Mapper.Map<List<ATMTN_GET_TBL_EXPORT_STATUS_SP_Result>, List<AutomatonExportStatus>>(_result);
                return _resultModel;
            }
            return null;
        }
        public void SubmitDownloadRequest(AutomatonExportStatus _s, ref string status_Code, ref string message)
        {
            _autoMS.SubmitDownloadRequest(_s, ref status_Code, ref message);
        }
        public DataTable GetTableData(string client_ID, string project_ID, string config_ID, string table_name, string ColumnList, byte IsDistinct, ref string status_Code, ref string message)
        {
            return _autoMS.GetTableData(client_ID, project_ID, config_ID, table_name, ColumnList, IsDistinct, ref status_Code, ref message);
        }

        public void LogRunAudit(string client_ID, string project_ID, string TemplateName, string Modified_by, long? Run_ID, ref long Run_ID_OUTPUT, long? RoleId, ref string status_Code, ref string message)
        {
            _autoMS.LogRunAudit(client_ID, project_ID, TemplateName, Modified_by, Run_ID, ref Run_ID_OUTPUT, RoleId, ref status_Code, ref message);
        }

        public void LogExcelRunAudit(string client_ID, string project_ID, string Tool_ID, string Source_Table_Name, string Source_Column_count, string Source_Record_Count, 
            long? Batch_ID, string Upload_status, string Target_Table_name,
            string Modified_by, long? Run_ID, ref long Run_ID_OUTPUT, long? RoleId, ref string status_Code, ref string message)
        {
            _autoMS.LogExcelRunAudit(client_ID, project_ID, Tool_ID, Source_Table_Name, Source_Column_count, Source_Record_Count, Batch_ID, Upload_status, Target_Table_name,
                Modified_by, Run_ID, ref Run_ID_OUTPUT, RoleId, ref status_Code, ref message);
        }

        public DataTable Get_Mask_Table_Columns(string client_ID, string project_ID, string table_name, long? config_ID, DataTable Temptable, ref  string status_Code, ref string message)
        {
            return _autoMS.Get_MASK_TABLE_COLUMNS(client_ID, project_ID, table_name, config_ID, Temptable, ref status_Code, ref message);
        }


        public List<MaskEntity> GetMaskTypes(string column_Type, ref string status_Code, ref string message)
        {
            var _result = _autoMS.GetMaskTypes(column_Type, ref status_Code, ref message);

            if (_result != null)
            {
                Mapper.CreateMap<CMN_GET_DATA_MASK_TYPE_SP_Result, MaskEntity>();
                var _resultModel = Mapper.Map<List<CMN_GET_DATA_MASK_TYPE_SP_Result>, List<MaskEntity>>(_result);
                return _resultModel;
            }
            return null;
        }

        public List<BusinessNameEntity> GetEntityList(string client_ID, string project_ID, long Config_ID, ref string status_Code, ref string message)
        {
            var _result = _autoMS.GetEntityList(client_ID, project_ID, Config_ID, ref status_Code, ref message);

            if (_result != null)
            {
                Mapper.CreateMap<CMN_GET_ENTITY_LIST_SP_Result, BusinessNameEntity>();
                var _resultModel = Mapper.Map<List<CMN_GET_ENTITY_LIST_SP_Result>, List<BusinessNameEntity>>(_result);
                return _resultModel;
            }
            return null;
        }

        public string SaveEntityName(List<BusinessNameEntity> lstBname)
        {
            var dt = BuildDataTypeMS(lstBname);
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.SaveEntityData(dt, ref StatusCode, ref Message);

            if (StatusCode == "0")
                return "Saved successfully.";
            else
                return "Save failed. Error: " + Message;


        }
        private DataTable BuildDataTypeMS(List<BusinessNameEntity> lstBname)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Entity_ID", Type.GetType("System.Int64"));
            dt.Columns.Add("Client_ID", Type.GetType("System.String"));
            dt.Columns.Add("Project_ID", Type.GetType("System.String"));
            dt.Columns.Add("Entity_Name", Type.GetType("System.String"));
            dt.Columns.Add("Entity_Business_Name", Type.GetType("System.String"));
            dt.Columns.Add("Entity_Description", Type.GetType("System.String"));
            dt.Columns.Add("Legacy_Appl_Name", Type.GetType("System.String"));
            dt.Columns.Add("Ordinal_Position", Type.GetType("System.Int64"));
            dt.Columns.Add("LOB", Type.GetType("System.String"));
            dt.Columns.Add("Input_Type", Type.GetType("System.String"));
            dt.Columns.Add("Parent_Entity_ID", Type.GetType("System.Int64"));
            dt.Columns.Add("Active_Flag", Type.GetType("System.String"));
            dt.Columns.Add("Modified_By", Type.GetType("System.String"));

            foreach (var obj in lstBname)
            {
                DataRow dr = dt.NewRow();

                dr["Entity_ID"] = Convert.ToInt32(obj.Entity_ID);
                dr["Client_ID"] = obj.Client_ID;
                dr["Project_ID"] = obj.Project_ID;
                dr["Entity_Name"] = obj.Entity_Name;
                dr["Entity_Business_Name"] = obj.Entity_Business_Name;
                dr["Entity_Description"] = obj.Entity_Description;
                dr["Legacy_Appl_Name"] = obj.Legacy_Appl_Name;
                if (obj.Ordinal_Position == null)
                {
                    dr["Ordinal_Position"] = DBNull.Value;
                }
                else
                {
                    dr["Ordinal_Position"] = Convert.ToInt64(obj.Ordinal_Position);

                }
                dr["LOB"] = obj.LOB;
                dr["Input_Type"] = obj.Input_Type;

                if (obj.Parent_Entity_ID == null)
                {
                    dr["Parent_Entity_ID"] = DBNull.Value;
                }
                else
                {
                    dr["Parent_Entity_ID"] = Convert.ToInt64(obj.Parent_Entity_ID);
                }

                dr["Modified_By"] = obj.Modified_by;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public List<BusinessColumnEntity> GetEntityColList(string client_ID, string project_ID, long? entity_ID, long Config_ID, ref string status_Code, ref string message)
        {
            var _result = _autoMS.GetEntityColList(client_ID, project_ID, entity_ID, Config_ID, ref status_Code, ref message);

            if (_result != null)
            {
                Mapper.CreateMap<CMN_GET_ENTITY_COLS_SP_Result, BusinessColumnEntity>();
                var _resultModel = Mapper.Map<List<CMN_GET_ENTITY_COLS_SP_Result>, List<BusinessColumnEntity>>(_result);
                return _resultModel;
            }
            return null;
        }


        public void SaveEntityColData(DataTable dt, ref string status_Code, ref string message)
        {
            _autoMS.SaveEntityColData(dt, ref status_Code, ref message);
        }

        public long InsertTemplate(string client_ID, string project_ID, string TemplateName, string Created_By, string Type)
        {
            string status_Code = string.Empty;
            string message = string.Empty;


            return _autoMS.InsertTemplate(client_ID, project_ID, TemplateName, Created_By, Type, ref status_Code, ref message);
        }


        public void SaveParameter(ParametersEntity entity, ref string status_Code, ref string Message)
        {
            if (Convert.ToInt64(entity.Parameter_ID) > 0)
            {
                _autoMS.UpdateTemplateParameter(Convert.ToInt64(entity.Parameter_ID), entity.Parameter_Name, entity.Parameter_Value, entity.Created_By, ref  status_Code, ref  Message);
            }
            else
            {

                _autoMS.AddTemplateParameter(entity.Client_ID, entity.Project_ID, entity.Tool_ID, Convert.ToInt64(entity.Template_ID), entity.Template_Name, entity.Parameter_Name, entity.Parameter_Value, entity.Package_Save_Location, entity.Created_By, ref status_Code, ref Message);
            }
        }

        public void DeleteParameter(long ParameterID, string ModifiedBy, ref string status_Code, ref string Message)
        {

            _autoMS.DeleteTemplateParameter(ParameterID, ModifiedBy, ref  status_Code, ref  Message);

        }

        public List<ParametersEntity> GetTemplateParameterList(string client_ID, string project_ID, string templateName, ref string status_Code, ref string message)
        {
            var _result = _autoMS.GetTemplateParameterList(client_ID, project_ID, templateName, ref status_Code, ref message);

            if (_result != null)
            {
                Mapper.CreateMap<ETL_GET_VARIABLE_PARAM_SP_Result, ParametersEntity>();
                var _resultModel = Mapper.Map<List<ETL_GET_VARIABLE_PARAM_SP_Result>, List<ParametersEntity>>(_result);
                return _resultModel;
            }
            return null;
        }
        public void InsertOfflineBatchJobs(string client_ID, string project_ID, long? ToolID, string TemplateName, string TemplatePath, string Modified_by, string RunStatus, string JobDescription, Nullable<DateTime> scheduled_date, ref string status_Code, ref string message)
        {
            _autoMS.InsertOfflineBatchJobs(client_ID, project_ID, ToolID, TemplateName, TemplatePath, Modified_by, RunStatus, JobDescription, scheduled_date, ref status_Code, ref message);
        }

        public DataTable GetAutomatonBatchStatus(string client_id, string project_id, long? ToolID)
        {
            string status_Code = string.Empty; string message = string.Empty;

            return _autoMS.GetAutomatonBatchStatus(client_id, project_id, ToolID, ref status_Code, ref message);
        }
        public List<AutomatonSourceEntity> GetMetaDataTableBusinessName(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetMetaDataTableBusinessName(client_ID, project_ID, Table_name, connectionid, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<CMN_GET_METADATA_BUSNAME_SP_Result, AutomatonSourceEntity>();
                var _GetSourceModel = Mapper.Map<List<CMN_GET_METADATA_BUSNAME_SP_Result>, List<AutomatonSourceEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }

        public List<BusinessEntitySuggestedAttributes> GetEntitySuggestedAttr(string client_id, string project_id, string table_name, string column_name, ref string status_Code, ref string message)
        {
            var _businessAttr = _autoMS.GetEntitySuggestedAttr(client_id, project_id, table_name, column_name, ref status_Code, ref message);

            if (_businessAttr != null)
            {
                Mapper.CreateMap<CMN_GUESS_DATA_TYPE_SP_Result, BusinessEntitySuggestedAttributes>();
                var _GetSourceModel = Mapper.Map<List<CMN_GUESS_DATA_TYPE_SP_Result>, List<BusinessEntitySuggestedAttributes>>(_businessAttr);
                return _GetSourceModel;
            }
            return null;
        }

        public List<BusinessEntityTargetDBDataType> GetTargetDBDataTypes(string client_id, string project_id, Nullable<long> config_ID, ref string status_Code, ref string message)
        {
            var _TargetDBDataTypes = _autoMS.GetTargetDBDataTypes(client_id, project_id, config_ID, ref status_Code, ref message);

            if (_TargetDBDataTypes != null)
            {
                Mapper.CreateMap<CMN_GET_COL_DATA_TYPES_SP_Result, BusinessEntityTargetDBDataType>();
                var _GetSourceModel = Mapper.Map<List<CMN_GET_COL_DATA_TYPES_SP_Result>, List<BusinessEntityTargetDBDataType>>(_TargetDBDataTypes);
                return _GetSourceModel;
            }
            return null;
        }
        public int GetTargetDBtblClmnCount(string client_ID, string project_ID, Nullable<long> config_ID, string table_Name,
            ref Nullable<long> clmnCnt, ref string status_Code, ref string message)
        {
            return _autoMS.GetTargetDBtblClmnCount(client_ID, project_ID, config_ID, table_Name, ref clmnCnt, ref status_Code, ref message);
        }
        public void GenerateDesignDocument(string client_id, string project_id, long ToolID, long template_id, string template_name, string designTemplate,
            long? RoleId, ref string status_Code, ref string message)
        {
            DesignWrapper.CreateDesignDocument(client_id, project_id, ToolID, template_id, template_name, designTemplate, RoleId, ref status_Code, ref message);
        }

        public List<SchedulerEntity> GetScheduledTransformation(string client_ID, string project_ID, Nullable<long> tool_id, string trans_type, ref string status_Code, ref string message)
        {
            var GetList = _autoMS.GetScheduledTransformation(client_ID, project_ID, tool_id, trans_type, ref status_Code, ref message);

            if (GetList != null)
            {
                Mapper.CreateMap<CMN_GET_SCHEDULED_JOBS_SP_Result, SchedulerEntity>();
                var _GetList = Mapper.Map<List<CMN_GET_SCHEDULED_JOBS_SP_Result>, List<SchedulerEntity>>(GetList);
                return _GetList;
            }
            return null;
        }

        public string UpdateScheduleTransformation(long jobID, Nullable<DateTime> scheduleDate, char isDel, ref string status_Code, ref string message)
        {
            return _autoMS.UpdateScheduleTransformation(jobID, scheduleDate, isDel, ref status_Code, ref message);
        }


        //public void GenerateDRTemplate(string Template_ID)
        //{
        //    _autoMS.GenerateDRTemplate(Template_ID);
        //}

        public void SaveFileUploadTemplate(AutomatonFileUploadTemplateEntity entity, ref string Template_ID, ref string StatusCode, ref string Message)
        {
            _autoMS.SaveFileUploadTemplate(entity, ref Template_ID, ref StatusCode, ref Message);
        }

        public void CheckETLThresholdLimit(string client_ID, string project_ID, long? template_ID, ref string StatusCode, ref string Message)
        {
            _autoMS.CheckETLThresholdLimit(client_ID, project_ID, template_ID, ref StatusCode, ref Message);
        }
        public int DateColumnCheck(string client_ID, string project_ID, string table_Name, string column_name, Nullable<long> config_ID, string columnType, ref string status_Code,
            ref string message)
        {
            return _autoMS.DateColumnCheck(client_ID, project_ID, table_Name, column_name, config_ID, columnType, ref status_Code, ref message);
        }
        public void GenerateReconcile(string client_ID, string project_ID, string template_ID, long? RoleId, string UpdatedBy, ref string StatusCode, ref string Message)
        {
            _autoMS.GenerateReconcile(client_ID, project_ID, template_ID, RoleId, UpdatedBy, ref StatusCode, ref Message);
        }
        public List<AutomatonSourceEntity> GetMetaDataTableDetailAutomatic(string client_ID, string project_ID, long? template_ID, long? Role_ID, string Table_name, string Flag_type, string connectionid, ref string status_Code, ref string message)
        {
            var GetSource = _autoMS.GetMetaDataTableDetailAutomatic(client_ID, project_ID, template_ID, Role_ID, Table_name, Flag_type, connectionid, ref status_Code, ref message);

            if (GetSource != null)
            {
                Mapper.CreateMap<ETL_GET_SOURCE_TARGET_MAPPING_DTLS_SP_Result, AutomatonSourceEntity>();
                var _GetSourceModel = Mapper.Map<List<ETL_GET_SOURCE_TARGET_MAPPING_DTLS_SP_Result>, List<AutomatonSourceEntity>>(GetSource);
                return _GetSourceModel;
            }
            return null;
        }
    }

    class DesignWrapper
    {
        private readonly static HXRAutomaton _autoMS;

        static DesignWrapper()
        {
            _autoMS = new HXRAutomaton();
        }

        internal static void CreateDesignDocument(string client_id, string project_id, long ToolID, long template_id, string template_name, string designTemplate,
            long? RoleId, ref string status_Code, ref string message)
        {
            using (DocX document = DocX.Load(designTemplate))
            {
                /*
                 * The template 'Automaton_ETL_Design_template.docx' does exist, 
                 * so lets use it to create an design document for a selected template                 
                 */
                CreateDocumentFromTemplate(document, client_id, project_id, ToolID, template_id, RoleId, ref status_Code, ref message);

                // Save all changes made to this template *.docx (We don't want to replace Automaton_ETL_Design_template.docx).
                document.SaveAs(new System.IO.FileInfo(designTemplate).DirectoryName + "//GeneratedDocument//" + template_name + ".docx");
            }
        }

        private static DocX CreateDocumentFromTemplate(DocX template, string client_id, string project_id, long ToolID, long template_id, long? RoleId, ref string status_Code, ref string message)
        {

            DataTable dt = _autoMS.GetDesignMasterDetails(client_id, project_id, ToolID, template_id, RoleId, ref status_Code, ref message);

            #region Set CustomProperty values

            if (dt.Rows.Count > 0)
            {

                // Set the value of the custom properties 'template_name', 'document_date' and 'db_details'.
                template.AddCustomProperty(new CustomProperty("template_name", Convert.ToString(dt.Rows[0]["Template_Name"])));
                template.AddCustomProperty(new CustomProperty("document_date", DateTime.Today.Date.ToString("d")));
                template.AddCustomProperty(new CustomProperty("db_details", Convert.ToString(dt.Rows[0]["db_details"])));

                // Set the value of the custom properties 'src_table_names', 'src_tbl_business_names' and 'lkp_table_names'.
                template.AddCustomProperty(new CustomProperty("src_table_names", Convert.ToString(dt.Rows[0]["src_table_names"])));
                template.AddCustomProperty(new CustomProperty("src_tbl_business_names", Convert.ToString(dt.Rows[0]["src_table_business_names"])));
                template.AddCustomProperty(new CustomProperty("lkp_table_names", Convert.ToString(dt.Rows[0]["lookup_table_names"])));

                // Set the value of the custom properties 'tgt_table_names', 'tgt_table_business_names' and 'lkp_table_names'.
                template.AddCustomProperty(new CustomProperty("tgt_table_names", Convert.ToString(dt.Rows[0]["tgt_table_names"])));
                template.AddCustomProperty(new CustomProperty("tgt_tbl_business_names", Convert.ToString(dt.Rows[0]["tgt_table_business_names"])));


            }

            #endregion

            /* 
             * Automaton_ETL_Design_template.docx contains a blank Table, 
             * we should replace this with a new Table that
             * contains all of our source table info.
             */

            // Get data from database
            DataSet dsData = _autoMS.GetDesignTableAttributeDetails(client_id, project_id, ToolID, template_id, RoleId, ref status_Code, ref message);

            for (int i = 2; i < template.Tables.Count; i++)
            {
                if (dsData.Tables[i - 2].Rows.Count > 0)
                {
                    Table t = template.Tables[i];
                    Table newTable = CreateAndInsertTableAfter(t, ref template, dsData.Tables[i - 2]);
                    t.Remove();
                }
            }

            #region commented
            /*
            //Fill Source table data
            if (dsData.Tables.Count > 0)
            {
                Table t3 = template.Tables[2];
                Table source_table = CreateAndInsertTableAfter(t3, ref template, dsData.Tables[0]);
                t3.Remove();
            }
            //Fill Target table data
            if (dsData.Tables.Count > 1)
            {
                Table t4 = template.Tables[3];
                Table target_table = CreateAndInsertTableAfter(t4, ref template, dsData.Tables[1]);
                t4.Remove();
            }
            //Fill Lookup table data
            if (dsData.Tables.Count > 2)
            {
                Table t5 = template.Tables[4];
                Table target_table = CreateAndInsertTableAfter(t5, ref template, dsData.Tables[2]);
                t5.Remove();
            }
            //Fill Business rule details
            if (dsData.Tables.Count > 3)
            {
                Table t6 = template.Tables[5];
                Table target_table = CreateAndInsertTableAfter(t6, ref template, dsData.Tables[3]);
                t6.Remove();
            }
             //Fill Mapping details
            if (dsData.Tables.Count > 4)
            {
                Table t7 = template.Tables[6];
                Table target_table = CreateAndInsertTableAfter(t7, ref template, dsData.Tables[4]);
                t7.Remove();
            }            
            //Fill Transformation details
            if (dsData.Tables.Count > 5)
            {
                Table t8 = template.Tables[7];
                Table target_table = CreateAndInsertTableAfter(t8, ref template, dsData.Tables[5]);
                t8.Remove();
            }
            */
            #endregion

            return template;
        }

        private static Table CreateAndInsertTableAfter(Table t, ref DocX document, DataTable dtData)
        {

            DataTable data = dtData;

            /* 
             * The trick to replacing one Table with another,
             * is to insert the new Table after the old one, 
             * and then remove the old one.
             */
            Table newTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            newTable.Design = TableDesign.TableGrid;

            #region Table title
            Formatting table_title = new Formatting();
            table_title.Bold = true;

            for (int col = 0; col < newTable.ColumnCount; col++)
            {
                newTable.Rows[0].Cells[col].Paragraphs[0].InsertText(data.Columns[col].ColumnName, false, table_title);
                newTable.Rows[0].Cells[col].Paragraphs[0].Alignment = Alignment.center;

                //switch(data.Columns[col].ColumnName)
                //{
                //    case "Table Name":
                //        source_table.Rows[0].Cells[col].Width = 20;
                //        break;
                //    case "Ordinal Position":
                //        source_table.Rows[0].Cells[col].Width = 100;
                //        break;
                //    case "Attribute Name":
                //        source_table.Rows[0].Cells[col].Width = 20;
                //        break;
                //    case "Business Name":
                //        source_table.Rows[0].Cells[col].Width = 20;
                //        break;
                //    case "Data Type":
                //        source_table.Rows[0].Cells[col].Width = 20;
                //        break;
                //    case "Size":
                //        source_table.Rows[0].Cells[col].Width = 20;
                //        break;
                //}

            }

            // Let the tables coloumns expand to fit its contents.
            newTable.AutoFit = AutoFit.Window;
            #endregion

            // Loop through the rows in the Table and insert data from the data source.
            for (int row = 1; row < newTable.RowCount; row++)
            {
                for (int cell = 0; cell < newTable.Rows[row].Cells.Count; cell++)
                {
                    Paragraph cell_paragraph = newTable.Rows[row].Cells[cell].Paragraphs[0];
                    cell_paragraph.InsertText(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }

            // Center the Table
            newTable.Alignment = Alignment.center;

            // Return the source table now that it has been created.
            return newTable;
        }

      

    }
}
