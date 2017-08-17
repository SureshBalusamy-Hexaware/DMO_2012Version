using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;

namespace DM_UI.Controllers
{
    public class DataReconAPIController : ApiController
    {
        private readonly IDataRecon _recon;

        public DataReconAPIController()
        {
            _recon = new DataReconService();
        }

        [HttpGet] // api/DataReconAPI/GetSourceTableNames
        public dynamic GetTableColumns(string client_ID, string project_ID, string srcTable, string tgtTable, string srcConfig_ID, string tgtConfig_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var tableColumns = _recon.GetTableColumns(client_ID, project_ID, srcTable, tgtTable, srcConfig_ID, tgtConfig_ID, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from column in tableColumns
                    select new
                    {
                        cell = new string[] {
                         column.Source_Column,
                         column.Target_Column,                         
                      }
                    }).ToArray()
            };
            return rows;
        }

        [HttpGet] // api/DataReconAPI/GetTransRuleData
        public dynamic GetTransRuleData(string client_ID, string project_ID, string srcTable, string tgtTable, [FromUri]string[] Source_Column, [FromUri]string[] Target_Column)//,  [FromBody]string[] Source_Column, [FromBody]string[] Target_Column)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var tableColumns = _recon.GetTransRuleData(client_ID, project_ID, srcTable, tgtTable, string.Join(",", Source_Column), string.Join(",", Target_Column), ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from column in tableColumns
                    select new
                    {
                        cell = new string[] {
                         column.source_field_name,
                         column.source_trans_rule,
                         column.target_field_name,
                         column.target_trans_rule,
                      }
                    }).ToArray()
            };
            return rows;

        }

        [HttpGet] // GET api/DataReconAPI
        public dynamic GetMetaDataColumnByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string Config_Id, string Table_Name, int IsKeyColumn)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var tableColumns = _recon.GetMetaDataColumnsByTableName(client_ID, project_ID, database_IP, source_Target, database_Name, Config_Id, Table_Name, ref StatusCode, ref Message);


            var seq_no = 0;


            if (IsKeyColumn == 1)
            {

                var rows = new
                {
                    total = 0,
                    page = 0,
                    records = 0,
                    rows = (
                        from rule in tableColumns
                        orderby rule.Is_key_column descending
                        select new
                        {
                            cell = new string[] {
                         rule.Is_key_column,
                         (++seq_no).ToString(),
                         rule.Column_Name,
                         rule.Expression ,
                         rule.Data_Type                           
                      }
                        }).Take(5).ToArray()
                };

                return rows;
            }
            else
            {
                var rows = new
                {
                    total = 0,
                    page = 0,
                    records = 0,
                    rows = (
                        from rule in tableColumns
                        select new
                        {
                            cell = new string[] {                         
                         rule.Seq_No,
                         rule.Column_Name,
                         rule.Expression,
                         rule.Data_Type 
                         
                        
                      }
                        }).ToArray()
                };
                return rows;
            }
        }

        [HttpGet]
        public dynamic GetTemplateNames(string client_ID, string project_ID)
        {
            return _recon.GetTemplateNameList(client_ID, project_ID, Convert.ToInt64("0" + UIProperties.Sessions.ToolID), UIProperties.Sessions.Client.Role_ID);
        }

        [HttpGet]
        public List<TemplateDataEntity> GetTemplateDetails(string client_ID, string project_ID, string template_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var template_details = _recon.GetTemplateDetails(client_ID, project_ID, template_name, ref StatusCode, ref Message);

            return template_details;
        }

        [HttpGet] // GET    api/DataReconAPI     
        public dynamic GetKeyColumns(string client_ID, string project_ID, string table_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //return _ruleMS.GetKeyColumns(client_ID, project_ID, table_name, ref StatusCode, ref Message);

            return _recon.GetKeyColumns(client_ID, project_ID, table_name, Convert.ToInt32(UIProperties.Tools.DataRecon), ref StatusCode, ref Message);
            //var rows = new
            //{
            //    total = 0,
            //    page = 0,
            //    records = 0,
            //    rows = (
            //        from rule in _KeyColumn
            //        select new
            //        {
            //            i = rule.Table_Name,
            //            cell = new string[] {                            
            //                rule.Key_Column1,
            //                rule.Key_Column2,
            //                rule.Key_Column3,
            //                rule.Key_Column4,
            //                rule.Key_Column5
            //          }
            //        }).ToArray()
            //};
            //return rows;

        }

        [HttpPost]
        public dynamic SaveData(ComparisionData reconcileData)
        {
            var lst = new List<TemplateDataEntity>();
            var MessageResult = _recon.SaveData(reconcileData, UIProperties.Sessions.UserName, Convert.ToInt64("0" + UIProperties.Sessions.ToolID), UIProperties.Sessions.ConfigEntity.Config_ID, UIProperties.Sessions.TargetConfigEntity.Config_ID);

            //var mResult = new
            //{
            //    Message = MessageResult,
            //    Data = (from m in lst
            //            select new TemplateDataEntity
            //            {
            //                Source_Table_Name = m.Source_Table_Name,
            //                Source_Column_Name = m.Source_Column_Name,
            //                Target_Table_Name = m.Target_Table_Name,
            //                Target_Column_Name = m.Target_Column_Name,
            //                Error_Count = m.Error_Count,
            //                Status = m.Status,
            //                Run_ID = m.Run_ID
            //            }).ToArray()
            //};


            return MessageResult;

        }

        [HttpGet]
        public dynamic CompareData(string client_id, string project_id, string template_name)
        {
            var lst = new List<TemplateDataEntity>();
            var MessageResult = _recon.CompareData(client_id, project_id, template_name, UIProperties.Sessions.UserName, Convert.ToInt64("0" + UIProperties.Sessions.ToolID),
                UIProperties.Sessions.Client.Role_ID, ref lst);

            var mResult = new
            {
                Message = MessageResult,
                Data = (from m in lst
                        select new TemplateDataEntity
                        {
                            Source_Table_Name = m.Source_Table_Name,
                            Source_Column_Name = m.Source_Column_Name,
                            Target_Table_Name = m.Target_Table_Name,
                            Target_Column_Name = m.Target_Column_Name,
                            Error_Count = m.Error_Count,
                            Status = m.Status,
                            Run_ID = m.Run_ID
                        }).ToArray()
            };


            return mResult;
        }

        [HttpGet] // GET api/DataReconAPI
        public dynamic ValidateExpression(string client_ID, string project_ID, string Config_Id, string Table_Name, string Expression)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _recon.ValidateExpression(client_ID, project_ID, Convert.ToInt64(Config_Id), Table_Name, Expression, ref StatusCode, ref Message);

            var rows = new
            {
                Message = Message,
                IsSuccess = Message == "Success" ? 1 : 0
            };
            return rows;

        }

        [HttpGet]
        public dynamic GetTableDataDetail(string client_ID, string project_ID, string Table_name, string connectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _recon.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<DataReconSourceTargetEntity>;
            var totalTransactions = 1;

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from clm in lst
                    select new
                    {
                        cell = new string[] {                         
                         Convert.ToString(totalTransactions++),
                         clm.Column_Name,
                         clm.Data_Type,
                         clm.Target_Column_Name,
                         clm.Target_Data_Type
                      }
                    }).ToArray()
            };

            return rows;
        }

        [HttpGet]
        public dynamic GetTableColumnDetailForAutoComplete(string client_ID, string project_ID, string Table_name, string connectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _recon.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<DataReconSourceTargetEntity>;

            var mrows = (from auto in lst
                         select new
                         {
                             label = auto.Column_Name,
                             value = auto.Data_Type

                         }).ToArray();
            return mrows;

        }

        [HttpGet]
        public dynamic GetDetailedStatus(long RunID, string ColumnName)
        {
            var result = _recon.GetDetailErrorStatus(RunID, ColumnName);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from data in result
                    select new
                    {
                        cell = new string[] {                         
                         data.Source_Column_Value,                         
                         data.Target_Column_value,
                         data.TABLE_KEY_COLUMN1,
                         data.TABLE_KEY_COLUMN2,
                         data.TABLE_KEY_COLUMN3,
                         data.TABLE_KEY_COLUMN4,
                         data.TABLE_KEY_COLUMN5,                         
                        
                      }
                    }).ToArray()
            };
            return rows;
        }
    }
}
