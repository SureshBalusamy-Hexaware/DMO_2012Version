using DM_DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_DataModel.UnitOfWork
{
    public class HXRConfigurationMS : IDisposable
    {
        #region Private member variables...

        private DM_MetaDataEntities _context = null;
        //        private GenericRepository<HXR_CONFIGURATION_MS> _ConfigurationRepository;

        #endregion
        public HXRConfigurationMS()
        {
            _context = new DM_MetaDataEntities();
        }
        #region Public Repository Creation properties...


        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        //public GenericRepository<HXR_CONFIGURATION_MS> HXR_Configuration
        //{
        //    get
        //    {
        //        if (this._ConfigurationRepository == null)
        //            this._ConfigurationRepository = new GenericRepository<HXR_CONFIGURATION_MS>(_context);
        //        return _ConfigurationRepository;
        //    }
        //}

        #endregion


        #region Public member methods...
        /// <summary>
        /// Save Configuration
        /// </summary>
        /// <param name="client_ID"></param>
        /// <param name="project_ID"></param>
        /// <param name="source_Target"></param>
        /// <param name="dB_TYPE"></param>
        /// <param name="sERVER_NAME"></param>
        /// <param name="sERVER_IP_ADDRESS"></param>
        /// <param name="sERVER_PORT"></param>
        /// <param name="dATABASE_NAME"></param>
        /// <param name="dATABASE_PASSWORD"></param>
        /// <param name="sCHEMA_NAME"></param>
        /// <param name="sCHEMA_PASSWORD"></param>
        /// <param name="aCTIVE_FLAG"></param>
        /// <param name="oS_Username"></param>
        /// <param name="oS_PASSWORD"></param>
        /// <param name="last_Modified_Date"></param>
        /// <param name="last_Modified_By"></param>
        /// <param name="status_Code"></param>
        /// <param name="message"></param>
        public void SaveConfiguration(string client_ID, string project_ID, Nullable<long> tool_ID, Nullable<int> RoleId, string source_Target, string dB_TYPE, string sERVER_NAME,
            string sERVER_IP_ADDRESS, string sERVER_PORT, string dATABASE_NAME, string dATABASE_PASSWORD, string sCHEMA_NAME, string sCHEMA_PASSWORD,
            Nullable<long> aCTIVE_FLAG, string oS_Username, string oS_PASSWORD, Nullable<System.DateTime> last_Modified_Date, string last_Modified_By,
            ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));


            try
            {
                //_context.HXR_INSERT_CONFIG_DETAILS_SP(client_ID, project_ID,tool_ID, source_Target, dB_TYPE, sERVER_NAME, sERVER_IP_ADDRESS, sERVER_PORT, dATABASE_NAME, dATABASE_PASSWORD, sCHEMA_NAME,
                //    sCHEMA_PASSWORD, aCTIVE_FLAG, oS_Username, oS_PASSWORD, last_Modified_Date, last_Modified_By, OutPut_status_Code, OutPut_message);
                _context.COMMON_INSERT_CONFIG_DETAILS_SP(client_ID, project_ID, tool_ID, source_Target, dB_TYPE, sERVER_NAME, sERVER_IP_ADDRESS, sERVER_PORT, dATABASE_NAME,
                       dATABASE_PASSWORD, sCHEMA_NAME, sCHEMA_PASSWORD, aCTIVE_FLAG, oS_Username, oS_PASSWORD, last_Modified_Date, last_Modified_By, RoleId, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }


        public List<COMMON_GET_CONFIG_DETAILS_SP_Result> GetConfigurationByType(string client_ID, string project_ID, string source_Target, Nullable<long> tool_ID,
            Nullable<int> RoleId, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));


            try
            {

                //var result = _context.HXR_GET_CONFIG_DETAILS_SP(client_ID, project_ID, source_Target, tool_ID, OutPut_status_Code, OutPut_message).ToList<HXR_GET_CONFIG_DETAILS_SP_Result3>();
                var result = _context.COMMON_GET_CONFIG_DETAILS_SP(client_ID, project_ID, source_Target, null, tool_ID, RoleId, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_CONFIG_DETAILS_SP_Result>();



                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        public List<CMN_GET_CLIENT_SP_Result> GetClientDetails(string UserName, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.CMN_GET_CLIENT_SP(UserName, OutPut_status_Code, OutPut_message).ToList<CMN_GET_CLIENT_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();

                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        public void ImportMetaData(string client_ID, string project_ID, string table_name_list, string config_ID, string last_Modified_By, ref int GenerateMap, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_GenerateMap = new ObjectParameter("GenerateMap", typeof(int));

            try
            {
                _context.Database.CommandTimeout = 300;
                _context.COMMON_INSERT_METADATA_INFO_SP(client_ID, project_ID, config_ID, table_name_list,
                    last_Modified_By, OutPut_GenerateMap, OutPut_status_Code, OutPut_message);
                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                GenerateMap = int.Parse(OutPut_GenerateMap.Value.ToString());
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        public void ImportGenerateMap(string client_ID, string project_ID, string Tool_ID, ref long tableCount, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_tableCount = new ObjectParameter("table_count", typeof(long));

            try
            {
                _context.Database.CommandTimeout = 300;
                _context.ETL_GENERATE_TEMPLATES_EY_SP(client_ID, project_ID, Convert.ToInt64(Tool_ID), OutPut_tableCount, OutPut_status_Code, OutPut_message);
                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                tableCount = Convert.ToInt64(OutPut_tableCount.Value);

            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        #endregion

        #region Implementing IDiosposable...
        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion


        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
