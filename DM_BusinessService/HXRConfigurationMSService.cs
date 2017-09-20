using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_DataModel;
using DM_DataModel.UnitOfWork;
using AutoMapper;
using DM_BusinessEntities;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace DM_BusinessService
{
    public class HXRConfigurationMSService : IHXRConfigurationMS
    {
        //private readonly UnitOfWork _unitofWork;
        private readonly HXRConfigurationMS _ConfigMS;
        /// <summary>
        /// Constructor
        /// </summary>
        public HXRConfigurationMSService()
        {
            //_unitofWork = new UnitOfWork();
            _ConfigMS = new HXRConfigurationMS();
        }



        public bool TestConnection(string DataSource, string UserID, string Password, string SchemaName)
        {
            TestDbConnection _testDb = new TestDbConnection();
            _testDb.DataSource = DataSource;// TextBox3.Text.Trim();
            _testDb.UserID = UserID;// TextBox5.Text.Trim();
            _testDb.Password = Password;// TextBox6.Text.Trim();
            _testDb.SchemaName = SchemaName;// TextBox7.Text.Trim();

            _testDb.TestConnection();
            if (_testDb.IsConnectionValid)
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Test connection succeeded.');", true);
                return true;
            else
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Test connection failed.');", true);
                return false;
        }

        public void SaveConfiguration(DM_BusinessEntities.HXRConfigurationMSEntity ConfigMs, ref string StatusCode, ref string Message)
        {
            _ConfigMS.SaveConfiguration(ConfigMs.Project.Client.ClientID, ConfigMs.Project.ProjectID, ConfigMs.ToolID, ConfigMs.RoleId, ConfigMs.SourceTarget, ConfigMs.DBType, ConfigMs.ServerName,
                ConfigMs.ServerIPAddress, ConfigMs.ServerPort, ConfigMs.DataBaseName, ConfigMs.DataBasePassword, ConfigMs.SchemaName, ConfigMs.SchemaPassword,
            Convert.ToInt64(ConfigMs.ActiveFlag), ConfigMs.OSUserName, ConfigMs.OSPassword, DateTime.Now, ConfigMs.LastModifiedBy, ref StatusCode, ref Message
                );
        }
        public void ImportMetaData(DM_BusinessEntities.ImportMetaDataEntity ImportMetaData, ref string StatusCode, ref string Message, ref int GenerateMap)
        {
            _ConfigMS.ImportMetaData(ImportMetaData.ClientID, ImportMetaData.ProjectID, ImportMetaData.TablenameList, ImportMetaData.config_ID,
                 ImportMetaData.LastModifiedBy, ref GenerateMap, ref StatusCode, ref Message);
        }

        public void ImportGenerateMap(DM_BusinessEntities.ImportMetaDataEntity ImportMetaData, ref long tableCount, ref string StatusCode, ref string Message)
        {
            _ConfigMS.ImportGenerateMap(ImportMetaData.ClientID, ImportMetaData.ProjectID, ImportMetaData.ToolID, ref tableCount, ref StatusCode, ref Message);
        }
        HXRConfigurationMSEntity IHXRConfigurationMS.GetConfigurationByType(string ClientID, string ProjectID, string SourceTarget, Nullable<long> tool_ID, Nullable<int> RoleId, ref string StatusCode, ref string Message)
        {
            HXRConfigurationMSEntity configMSEntity = new HXRConfigurationMSEntity();
            configMSEntity.Project = new HXRProjectMSEntity();
            configMSEntity.Project.Client = new HXRClientMSEntity();

            var ConfigDetails = _ConfigMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, tool_ID, RoleId, ref  StatusCode, ref  Message);
            if (ConfigDetails != null && ConfigDetails.Count > 0)
            {
                //Mapper.CreateMap<HXR_GET_CONFIG_DETAILS_SP_Result, HXRConfigurationMSEntity>();                
                //HXRConfigurationMSEntity ConfigModel = Mapper.Map<HXR_GET_CONFIG_DETAILS_SP_Result, HXRConfigurationMSEntity>(ConfigDetails[0]);
                configMSEntity.Config_ID = ConfigDetails[0].Config_ID;
                configMSEntity.Project.Client.ClientID = ConfigDetails[0].Client_ID;
                configMSEntity.Project.ProjectID = ConfigDetails[0].Project_ID;
                configMSEntity.SourceTarget = ConfigDetails[0].Source_Target;
                configMSEntity.ServerName = ConfigDetails[0].Server_Name;
                configMSEntity.ServerIPAddress = ConfigDetails[0].Server_IP_Address;
                configMSEntity.ServerPort = ConfigDetails[0].Server_Port;
                configMSEntity.DBType = ConfigDetails[0].DB_Type;
                configMSEntity.DataBaseName = ConfigDetails[0].Database_Name;
                configMSEntity.DataBasePassword = ConfigDetails[0].Database_Password;
                configMSEntity.SchemaName = ConfigDetails[0].Schema_Name;
                configMSEntity.SchemaPassword = ConfigDetails[0].Schema_Password;
                configMSEntity.ActiveFlag = ConfigDetails[0].Active_Flag.ToString();
                configMSEntity.OSUserName = ConfigDetails[0].OS_Username;
                configMSEntity.OSPassword = ConfigDetails[0].OS_Password;
                return configMSEntity;

            }
            return null;
        }

        public HXRGetClientEntity GetClientDetails(string UserName, ref string StatusCode, ref string Message)
        {

            var _ClientInfo = _ConfigMS.GetClientDetails(UserName, ref StatusCode, ref Message);

            if (_ClientInfo != null && _ClientInfo.Count > 0)
            {
                Mapper.CreateMap<CMN_GET_CLIENT_SP_Result, HXRGetClientEntity>();
                var _ClientInfoModel = Mapper.Map<List<CMN_GET_CLIENT_SP_Result>, List<HXRGetClientEntity>>(_ClientInfo);
                return _ClientInfoModel[0];
            }
            return null;
        }
    }
    /// <summary>
    /// Testing Database connectivity
    /// </summary>
    public class TestDbConnection
    {
        //Data Source=172.25.121.105;User ID=sa;Password=***********
        //Data Source=172.25.121.105;Initial Catalog=DIMAPLUS;User ID=sa
        //public TestDbConnection()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        /// <summary>
        /// IP address of the Database server
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// SQLServer authendication UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// SQLServer authendication Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Initial Catalog
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// Check connection string is valid
        /// </summary>
        public bool IsConnectionValid { get; set; }

        /// <summary>
        /// connectionstring
        /// </summary>
        private string ConnectionString { get; set; }


        private string Query
        {
            get
            {
                return "select name from master.dbo.sysdatabases where name=\'" + SchemaName + "\'";
                //return "SELECT TOP 1000 [EMP_ID]      ,[EMPNAME]      ,[DEPT]      ,[SALARY]  FROM [DEMO_SRC].[dbo].[EMPLOYEE_SRC]";
            }
        }

        /// <summary>
        /// Open the connection using sqlconnection class
        /// </summary>
        public void TestConnection()
        {

            ConnectionString = "Data Source = " + DataSource + ";User ID = " + UserID + ";Password = " + Password;
            try
            {
                using (SqlConnection _con = new SqlConnection(ConnectionString))
                {
                    _con.Open();

                    using (SqlCommand cmd = new SqlCommand(Query, _con))
                    {
                        SqlDataReader _reader = cmd.ExecuteReader();
                        IsConnectionValid = _reader.HasRows;
                        _reader.Close();
                    }
                }
            }
            catch (SqlException _sqlex)
            {
                IsConnectionValid = false;
            }
            catch (Exception _e)
            {
                IsConnectionValid = false;
            }
        }

    }
}
