using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System.Web;
using System.Web.Providers.Entities;


namespace DM_UI.Controllers
{
    public class ConfigMSController : ApiController
    {

        private readonly IHXRConfigurationMS _configMS;

        #region Public Constructor
        public ConfigMSController()
        {
            _configMS = new HXRConfigurationMSService();
        }
        #endregion

        [HttpGet] // GET api/configms
        public HXRConfigurationMSEntity Get(string ClientID, string ProjectID, string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //long ToolID = 7;
            long ToolID = Convert.ToInt64(UIProperties.Tools.HexaRule);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;
            return _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
        }

        [HttpGet] //Test Connection
        public string Get([FromUri] List<string> DataSource, [FromUri] List<string> UserID, [FromUri] List<string> Password, [FromUri] List<string> SchemaName)
        {
            string Msg = string.Empty;
            string _Succeeded = "Test connection succeeded.";
            string _Failed = "Test connection failed.";
            bool _Source = false, _Target = false;
            try
            {
                if (_configMS.TestConnection(DataSource[0], UserID[0], Password[0], SchemaName[0]))
                {
                    _Source = true;
                    //Msg = "Source Test connection succeeded. ";
                }
                //else
                //{
                //    Msg = "Source Test connection failed.";
                //}
                if (Convert.ToInt16(UIProperties.Sessions.ToolID) != Convert.ToInt16(UIProperties.Tools.DataProfiler) &&
                    Convert.ToInt16(UIProperties.Sessions.ToolID) != Convert.ToInt16(UIProperties.Tools.HexaRule))
                {
                    if (_configMS.TestConnection(DataSource[1], UserID[1], Password[1], SchemaName[1]))
                    {
                        _Target = true;
                        //Msg += "Target Test connection succeeded.";
                    }
                }
                //else Msg += "Target Test connection failed.";
                else
                {
                    if (_Source)
                        Msg = "Source " + _Succeeded;
                    else
                        Msg = "Source " + _Failed;
                    return Msg;
                }

                if (_Source && _Target)
                    Msg = "Source and Target " + _Succeeded;
                else if (_Source && (!_Target))
                    Msg = "Source " + _Succeeded + " " + "Target " + _Failed;
                else if ((!_Source) && _Target)
                    Msg = "Source " + _Failed + " " + "Target " + _Succeeded;
            }
            catch (Exception _ex)
            {
                Msg = "Test connection failed.";
                return Msg;
            }
            return Msg;
        }

        // GET api/configms/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]   // POST api/configms
        public string Post([FromBody]HXRConfigurationMSEntity[] hxrconfigms)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //hxrconfigms.Project.Client.ClientID = UIProperties.Sessions.Client.Client_ID;
            //hxrconfigms.Project.ProjectID = UIProperties.Sessions.Client.project_ID;
            //hxrconfigms.ToolID = 7;


            hxrconfigms[0].LastModifiedDate = DateTime.Now;
            hxrconfigms[0].LastModifiedBy = UIProperties.Sessions.UserName;
            _configMS.SaveConfiguration(hxrconfigms[0], ref StatusCode, ref Message);

            if (hxrconfigms.Length > 1)
            {

                hxrconfigms[1].LastModifiedDate = DateTime.Now;
                _configMS.SaveConfiguration(hxrconfigms[1], ref StatusCode, ref Message);
                hxrconfigms[1].LastModifiedBy = UIProperties.Sessions.UserName;
            }


            //Refersh the session Object Start Code
            string mStatusCode = string.Empty, mMessage = string.Empty;
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            int ToolID = Convert.ToInt16(UIProperties.Sessions.ToolID);
            // int? RoleId = UIProperties.Sessions.Client.Role_ID;// Convert.ToInt16(UIProperties.Sessions.Client.Role_ID);
            int? RoleId = hxrconfigms[0].RoleId;

            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, "SOURCE", ToolID, RoleId, ref mStatusCode, ref mMessage);
            if (_configEntity == null) return null;
            UIProperties.Sessions.ConfigEntity = _configEntity;

            if (hxrconfigms.Length > 1)
            {
                var _TargetEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, "TARGET", ToolID, RoleId, ref mStatusCode, ref mMessage);
                if (_TargetEntity == null) return null;

                HXRTargetConfigurationMSEntity _tgtConfigEntity = new HXRTargetConfigurationMSEntity()
                {
                    Config_ID = _TargetEntity.Config_ID
                };
                UIProperties.Sessions.TargetConfigEntity = _tgtConfigEntity;
                //Refersh the session Object End Code
            }
            return Message;
        }

        [HttpPost]
        public string ImportPost([FromBody]ImportMetaDataEntity ImportMetaData)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            int GenerateMap = 0;
            ImportMetaData.LastModifiedBy = UIProperties.Sessions.UserName;
            _configMS.ImportMetaData(ImportMetaData, ref  StatusCode, ref  Message, ref GenerateMap);


            //HttpContext.Current.Session.Add("GenerateMap", GenerateMap);

            System.Web.HttpContext.Current.Session["GenerateMap"] = GenerateMap;

            return Message;
        }

        [HttpPost]
        public object GeneratePost([FromBody]ImportMetaDataEntity ImportMetaData)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long tableCount = 0;
            ImportMetaData.LastModifiedBy = UIProperties.Sessions.UserName;
            //_configMS.ImportGenerateMap(ImportMetaData, ref  tableCount, ref  StatusCode, ref  Message);

            _configMS.GenerateMapping(ImportMetaData.ClientID, ImportMetaData.ProjectID, ImportMetaData.config_ID, ImportMetaData.TablenameList,
                UIProperties.Sessions.UserName, ref StatusCode, ref  Message);

            var data = new
            {
                statusCode = StatusCode,
                message = Message,
                tableCount = tableCount
            };
            return data;
        }

        [HttpPut]  // PUT api/configms/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete] // DELETE api/configms/5
        public void Delete(int id)
        {
        }


        [HttpGet]
        public object GetConfigDetailsByRole(int RoleId)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            try
            {
                int ToolID = Convert.ToInt32(UIProperties.Sessions.ToolID);
                string ClientID = UIProperties.Sessions.Client.Client_ID;
                string ProjectID = UIProperties.Sessions.Client.project_ID;
                //ViewData["HXRTgtConfigurationMSEntity"] = GetConfigBySourceTarget("TARGET", ToolID, RoleId);
                //ViewData["HXRSrcConfigurationMSEntity"] = GetConfigBySourceTarget("SOURCE", ToolID, RoleId);
                HXRConfigurationMSEntity _tgtConfigEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, "TARGET", ToolID, RoleId, ref StatusCode, ref Message);
                HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, "SOURCE", ToolID, RoleId, ref StatusCode, ref Message);

                if (_tgtConfigEntity == null)
                {
                    _tgtConfigEntity = new HXRConfigurationMSEntity()
                    {
                        Project = new HXRProjectMSEntity()
                        {
                            Client = new HXRClientMSEntity()
                        }
                    };
                }
                if (_configEntity == null)
                {
                    _configEntity = new HXRConfigurationMSEntity()
                    {
                        Project = new HXRProjectMSEntity()
                        {
                            Client = new HXRClientMSEntity()
                        }
                    };
                }

                var data = new
                {
                    Target = _tgtConfigEntity,
                    Source = _configEntity
                };
                return data;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                var data = new { message = ex.Message };
                return data;
            }
        }
    }
}
