using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DM_BusinessEntities;
using DM_BusinessService;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using DM_UI.App_Start;
using System.Net.Http.Formatting;


namespace DM_UI.Controllers
{

    public class DIMAPLUSAPIController : ApiController
    {
        private readonly IDASEM _dimaplus;
        #region Public Constructor
        public DIMAPLUSAPIController()
        {
            _dimaplus = new DASEMService();
        }
        #endregion

        public List<DASEMTemplateEntity> GetAllTemplates(string client_ID, string project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _dimaplus.GetAllTemplates(client_ID, project_ID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref  Message);
            return lst;

        }

        // JA:20170313 GetAllMaskingTemplates
        public List<DASEMTemplateEntity> GetAllMaskingTemplates(string client_ID, string project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _dimaplus.GetAllMaskingTemplates(client_ID, project_ID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref  Message);
            return lst;

        }

        //public List<DASEMTemplateEntity> GetAllMaskingTemplateDetails(string client_ID, string project_ID, string Template_ID,string Tool_ID)
        //{
        //    string StatusCode = string.Empty, Message = string.Empty;
        //    var lst = _dimaplus.GetAllMaskingTemplateDetails(client_ID, project_ID, Template_ID, Tool_ID, ref StatusCode, ref  Message);
        //    return lst;

        //}


        public List<string> GetSlicingColumns(string client_ID, string project_ID, string constraint_Type, long config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var lst = _dimaplus.GetSlicingColumns(client_ID, project_ID, constraint_Type, config_ID, ref StatusCode, ref  Message);

            return lst;

        }
        public List<string> GetSlicingColumnValues(string client_ID, string project_ID, string Column_name, int Config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var lst = _dimaplus.GetSlicingColumnValues(client_ID, project_ID, Column_name, Config_ID, ref StatusCode, ref Message);

            return lst;
        }
        public dynamic GetCriteria(string Type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _Columns = _dimaplus.GetCriteria(Type, ref StatusCode, ref Message);

            var data = new { ColNames = _Columns };
            return data;
        }
        //[HttpGet]
        //public dynamic GetCriteria(int page, int rows, string client_ID, string project_ID, string Object_Type, string Template_Name,
        //    long config_ID, string Column_name, string SlicingValue, string Expression)
        public dynamic GetCriteria(string client_ID, string project_ID, string Object_Type, string Template_Name,
        long config_ID, string Column_name, string SlicingValue, string Expression)
        {
            int _ToolID = (int)UIProperties.Tools.DIMAPLUS;
            int page = 1, rows = 10;
            string StatusCode = string.Empty, Message = string.Empty;
            DataTable _Criteria = _dimaplus.GetCriteria(page, rows, client_ID, project_ID, Object_Type, Template_Name, config_ID, Column_name, SlicingValue, _ToolID,
                Expression, ref StatusCode, ref Message);

            //return UIProperties.ConvertToJSONData(_Criteria, page, rows);
            return UIProperties.ConvertToJQGridRows(_Criteria, page, rows);
        }
        public dynamic GetAllCriteria(int page, int rows)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            List<DIMAPLUSCriteriaEntity> _AllCriteria = _dimaplus.GetAllCriteria(page, rows, _ClientID, _ProjectID, ref StatusCode, ref Message);
            if (_AllCriteria.Count <= 0) return null;
            var result = new
            {
                total = Math.Ceiling(Convert.ToDouble(_AllCriteria[0].TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = _AllCriteria[0].TotalRecords.ToString(),
                rows = (from _criteria in _AllCriteria
                        select new
                        {
                            cell = new string[] { 
                                _criteria.Objects,
                                _criteria.SlicingField,
                                _criteria.SlicingValue,
                                _criteria.Criteria,
                                _criteria.SourceDelete
                            }
                        }).ToArray()
            };

            return result;
        }

        private DataTable GetCriteriaMS()
        {
            DataTable CriteriaMS = new DataTable();
            CriteriaMS.Columns.Add("Client_ID", typeof(string));
            CriteriaMS.Columns.Add("Project_ID", typeof(string));
            CriteriaMS.Columns.Add("Criteria_ID", typeof(Int32));
            CriteriaMS.Columns.Add("Template_Name", typeof(string));
            CriteriaMS.Columns.Add("Tool_ID", typeof(Int32));
            CriteriaMS.Columns.Add("Object_Type", typeof(string));
            CriteriaMS.Columns.Add("Object_Name", typeof(string));
            CriteriaMS.Columns.Add("Slicing_field", typeof(string));
            CriteriaMS.Columns.Add("Slicing_Value", typeof(string));
            CriteriaMS.Columns.Add("Expression_Code", typeof(string));
            CriteriaMS.Columns.Add("Sx_Flag", typeof(int));
            CriteriaMS.Columns.Add("Tx_Flag", typeof(int));
            CriteriaMS.Columns.Add("Fx_Flag", typeof(int));
            CriteriaMS.Columns.Add("Is_Delete", typeof(string));
            CriteriaMS.Columns.Add("Modified_By", typeof(string));

            return CriteriaMS;
        }
        [HttpPost]
        public string SaveUpdateCriteria(DIMAPLUSCriteriaEntity[] Criteria)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            DataTable dtCriteriaMS = GetCriteriaMS();

            foreach (DIMAPLUSCriteriaEntity item in Criteria)
            {
                //item.ClientId = _ClientID;
                //item.ProjectId = _ProjectID;

                DataRow row = dtCriteriaMS.NewRow();
                row["Client_ID"] = item.ClientId;
                row["Project_ID"] = item.ProjectId;
                row["Criteria_ID"] = item.Criteria_ID;
                row["Template_Name"] = item.Template;
                row["Tool_ID"] = ((int)UIProperties.Tools.DIMAPLUS);
                row["Object_Type"] = item.ObjectType;
                row["Object_Name"] = item.Objects;
                row["Slicing_field"] = item.SlicingField;
                row["Slicing_Value"] = item.SlicingValue;
                row["Expression_Code"] = item.Condition;
                row["Sx_Flag"] = item.Criteria;
                row["Tx_Flag"] = DBNull.Value;
                row["Fx_Flag"] = DBNull.Value;
                row["Is_Delete"] = item.SourceDelete;
                row["Modified_By"] = UIProperties.Sessions.UserName;

                dtCriteriaMS.Rows.Add(row);

                //HXR_UPDATE_CRITERIA_SP   
                //_dimaplus.SaveUpdateCriteria(item, ref StatusCode, ref Message);

                //HXR_UPDATE_CRITERIA_DELETEFLAG_SP
                //_dimaplus.UpdateCriteriaSourceDelete(item, ref StatusCode, ref Message);

            }
            _dimaplus.SaveTemplate(dtCriteriaMS, ref StatusCode, ref Message);

            return Message;
        }

        //JA:20170313 


        private DataTable GetMaskingMS()
        {
            DataTable MaskingMS = new DataTable();
            MaskingMS.Columns.Add("Client_ID", typeof(string));
            MaskingMS.Columns.Add("Project_ID", typeof(string));
            MaskingMS.Columns.Add("Template_Name", typeof(string));
            MaskingMS.Columns.Add("Table_Name", typeof(string));
            MaskingMS.Columns.Add("Config_ID", typeof(Int32));
            MaskingMS.Columns.Add("Column_Name", typeof(string));
            MaskingMS.Columns.Add("Masking_Type", typeof(string));
            MaskingMS.Columns.Add("Un_Mask", typeof(int));
            MaskingMS.Columns.Add("Updated_By", typeof(string));
            MaskingMS.Columns.Add("Batch_Process", typeof(int));
            return MaskingMS;
        }

        [HttpPost]
        public string SaveUpdateMaskingTemplate(DIMAPLUSMaskingEntity[] Masking)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;
            long? RoleId = UIProperties.Sessions.Client.Role_ID;

            DataTable dtMaskingMS = GetMaskingMS();

            foreach (DIMAPLUSMaskingEntity item in Masking)
            {
                DataRow row = dtMaskingMS.NewRow();
                row["Client_ID"] = item.ClientId;
                row["Project_ID"] = item.ProjectId;
                row["Template_Name"] = item.TemplateName;
                row["Table_Name"] = item.Table_Name;
                row["Config_ID"] = item.Config_ID;
                row["Column_Name"] = item.Column_Name;
                row["Masking_Type"] = item.Masking_Type;
                row["Un_Mask"] = item.Un_Mask;
                row["Updated_By"] = item.Updated_By;
                row["Batch_Process"] = item.Batch_process;


                dtMaskingMS.Rows.Add(row);
            }
            _dimaplus.SaveMaskingTemplate(dtMaskingMS, RoleId, ref StatusCode, ref Message);

            return Message;
        }





        [HttpPost]
        public string PurgeData(DIMAPLUSCriteriaEntity[] Criteria)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            IEnumerable<DIMAPLUSCriteriaEntity> _Criteria = Criteria.Where(r => r.SourceDelete == "Yes");

            foreach (DIMAPLUSCriteriaEntity item in _Criteria)
            {
                //if (item.SourceDelete != "Yes") continue;
                item.SourceDelete = item.SourceDelete == "Yes" ? "1" : "0";
                item.ClientId = _ClientID;
                item.ProjectId = _ProjectID;
                item.Tool_ID = ((int)UIProperties.Tools.DIMAPLUS);
                item.RoleId = UIProperties.Sessions.Client.Role_ID;
                _dimaplus.PurgeData(item, ref StatusCode, ref Message);

                if (StatusCode != "0")
                    return Message;

            }
            return Message;
        }


        [HttpGet]
        public dynamic GetSourceDetails(string Client_ID, string project_ID, string Template_ID)
        {
            var lst = _dimaplus.GetSourceObjectDetails(Client_ID, project_ID, Convert.ToInt32(Template_ID));
            var rows = new
            {
                rows = (
                    from pro in lst
                    select new
                    {
                        i = pro.No_of_Tables,
                        cell = new string[] {
                             pro.No_of_Tables.ToString(),
                            pro.No_of_Views.ToString(),
                            pro.No_of_Procedures.ToString() 
                      }
                    }).ToArray()
            };

            return rows;
        }
        [HttpGet]
        public dynamic GetObjectDetails(string Client_ID, string project_ID, string ObjType, string TemplateName, string ConfigId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            DIMAPLUSCriteriaEntity _params = new DIMAPLUSCriteriaEntity();
            _params.ClientId = Client_ID;
            _params.ProjectId = project_ID;
            _params.ObjectType = ObjType;
            _params.Template = TemplateName;
            _params.ConfigId = ConfigId;
            DataTable _dt = _dimaplus.GetObjectDetails(_params, ref StatusCode, ref Message);
            var rows = new
            {
                rows = (
                    from pro in _dt.AsEnumerable()
                    select new
                    {
                        i = pro["ID"],
                        cell = new string[] {
                            pro["ID"].ToString(),
                             pro["ObjectName"].ToString(),
                            pro["Record_Count"].ToString()                            
                      }
                    }).ToArray()
            };
            return rows;
        }
        [HttpGet]
        public dynamic GetTargetDetails(string Client_ID, string project_ID)
        {
            var lst = _dimaplus.GetTargetServerDetails(Client_ID, project_ID, UIProperties.Sessions.Client.Role_ID);
            var rows = new
            {
                rows = (
                    from pro in lst
                    select new
                    {
                        i = pro.Server_IP,
                        cell = new string[] {
                             pro.Server_IP.ToString(),
                            pro.Database.ToString(),
                            pro.User.ToString() 
                      }
                    }).ToArray()
            };

            return rows;
        }
        [HttpGet]
        public dynamic GetAuditReport(string Client_ID, string project_ID, string Template_ID)
        {
            var lst = _dimaplus.GetAuditReport(Client_ID, project_ID, Convert.ToInt32(Template_ID));
            var rows = new
            {
                rows = (
                    from pro in lst
                    select new
                    {
                        i = pro.Object_Type,
                        cell = new string[] {
                           
                            pro.Run_ID.ToString(),
                             pro.Object_Type.ToString(),
                            pro.Source_Object_Count.ToString(),
                            pro.Target_Object_Count.ToString(),
                             pro.Template_ID.ToString()
                      }
                    }).ToArray()
            };

            return rows;
        }
        [HttpGet]
        public dynamic GetTransferResultReport(string Client_ID, string project_ID, long? Template_ID, long? Run_ID)
        {
            var lst = _dimaplus.GetTransferResultReport(Client_ID, project_ID, Template_ID, Run_ID);
            var rows = new
            {
                rows = (
                    from pro in lst
                    select new
                    {
                        i = pro.Type,
                        cell = new string[] {
                            pro.Run_ID.ToString(),
                            Convert.ToString( pro.Type),
                            Convert.ToString(pro.Object_Name),
                            Convert.ToString(pro.Slicing_Field) ,
                            Convert.ToString(pro.Slicing_Value),
                            Convert.ToString(pro.Source_Records) ,
                            Convert.ToString(pro.Target_Records) ,
                            Convert.ToString(pro.Criteria_Sucess) ,
                            Convert.ToString(pro.Is_Delete) 
                      }
                    }).ToArray()
            };


            return rows;
        }


        [HttpPost]
        public string CopySlicedData(FormDataCollection data)
        {
            string Client_ID = Convert.ToString(data.Get("Client_ID"));
            string project_ID = Convert.ToString(data.Get("project_ID"));
            string ToolID = Convert.ToString(data.Get("ToolID"));
            long? template_ID = Convert.ToInt32(data.Get("TemplateId"));
            string message = string.Empty;
            string status_code = string.Empty;
            
            bool mResult = _dimaplus.CopySlicedData(Client_ID, project_ID, ToolID, template_ID, UIProperties.Sessions.Client.Role_ID, 
                UIProperties.Sessions.UserName, ref message, ref status_code);
            if (mResult)
            {
                //return "Successfully Completed.";
                return message;
            }
            else
            {
                return message;
            }

        }
        [HttpGet]
        public dynamic GetDeleteList(int page, int rows, string TemplateId, string Run_ID)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            List<DIMAPLUSCriteriaEntity> _Purge = _dimaplus.GetDeleteList(page, rows, _ClientID, _ProjectID, Convert.ToInt32(TemplateId), 
                Convert.ToInt32(Run_ID), ref StatusCode, ref Message);
            if (_Purge.Count <= 0) return null;
            var result = new
            {
                total = Math.Ceiling(Convert.ToDouble(_Purge[0].TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = _Purge[0].TotalRecords.ToString(),
                rows = (from _criteria in _Purge
                        select new
                        {
                            cell = new string[] { 
                                _criteria.Criteria_ID.ToString(),
                                _criteria.Objects,
                                _criteria.SlicingField,
                                _criteria.SlicingValue,                                
                                _criteria.SourceDelete
                            }
                        }).ToArray()
            };

            return result;
        }

        [HttpGet]
        public List<DASEMSliceRunIDsEntity> GetRunIDList(string Client_ID, string project_ID, long? template_ID)
        {

            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;


            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _dimaplus.GetRunIDList(_ClientID, _ProjectID, template_ID, ref StatusCode, ref  Message);
            return lst;

        }

        [HttpGet]
        public dynamic CheckCopyCount(string TemplateId)
        {
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;
            string StatusCode = string.Empty, Message = string.Empty;
            long _TemplateId = 0;
            long.TryParse(TemplateId, out _TemplateId);
            _dimaplus.CheckCopyCount(_ClientID, _ProjectID, _TemplateId, ref Message, ref StatusCode);
            var data = new { StatusCode = StatusCode, Message = Message };
            return data;
        }


    }
}
