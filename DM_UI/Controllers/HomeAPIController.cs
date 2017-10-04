using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DM_UI.Controllers
{
    public class HomeAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {

            return "value";
        }

        // POST api/<controller>
        public string Post([FromBody]DBDeployment dbDeployment)
        {
            try
            {
                DASEMService dasemService = new DASEMService();

                string ConnectionString =
                    "data source=" + dbDeployment.ServerIP + ";initial catalog=" + dbDeployment.SchemaName
                    + ";user id=" + dbDeployment.DBUser + ";password=" + dbDeployment.DBPassword + ";";

                string message = string.Empty;
                string status_code = string.Empty;

                string SqlFileLocation = ConfigurationManager.AppSettings["Path_DBDeployFolder"];
                string EncryptedFile = ConfigurationManager.AppSettings["EncryptedFile"];
                string DecryptedFile = ConfigurationManager.AppSettings["DecryptedFile"];
                SqlFileLocation = AppDomain.CurrentDomain.BaseDirectory + @"\" + SqlFileLocation;


                if (dasemService.DBDeployment(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID,
                    UIProperties.Sessions.ToolID, ConnectionString,
                    SqlFileLocation + "\\" + EncryptedFile, SqlFileLocation + "\\" + DecryptedFile, ref message, ref status_code))
                    return "Success";

                return "Error: " + message;
            }
            catch (Exception _ex)
            {
                return _ex.Message;

            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}