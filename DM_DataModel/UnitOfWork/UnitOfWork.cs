using DM_DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DM_DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {

        #region Private member variables...

        //private WebApiDbEntities _context = null;
        private DM_MetaDataEntities _context = null;

        // private GenericRepository<HXR_CONFIGURATION_MS> _ConfigurationRepository;

        //private GenericRepository<Product> _productRepository;
        //private GenericRepository<Token> _tokenRepository;

        #endregion

        public UnitOfWork()
        {
            //_context = new WebApiDbEntities();
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
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
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
        //public void SaveConfiguration(string client_ID, string project_ID, Nullable<long> tool_ID, string source_Target, string dB_TYPE, string sERVER_NAME, string sERVER_IP_ADDRESS, string sERVER_PORT, string dATABASE_NAME, string dATABASE_PASSWORD, string sCHEMA_NAME, string sCHEMA_PASSWORD, Nullable<long> aCTIVE_FLAG, string oS_Username, string oS_PASSWORD, Nullable<System.DateTime> last_Modified_Date, string last_Modified_By,
        //    ref  string status_Code, ref string message)
        //{
        //    var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
        //    var OutPut_message = new ObjectParameter("message", typeof(string));


        //    try
        //    {
        //        _context.HXR_INSERT_CONFIG_DETAILS_SP(client_ID, project_ID, tool_ID, source_Target, dB_TYPE, sERVER_NAME, sERVER_IP_ADDRESS, sERVER_PORT, dATABASE_NAME, dATABASE_PASSWORD, sCHEMA_NAME,
        //            sCHEMA_PASSWORD, aCTIVE_FLAG, oS_Username, oS_PASSWORD, last_Modified_Date, last_Modified_By, OutPut_status_Code, OutPut_message);
        //        status_Code = OutPut_status_Code.Value.ToString();
        //        message = OutPut_message.Value.ToString();
        //    }
        //    catch (DbEntityValidationException e)
        //    {

        //        var outputLines = new List<string>();
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            outputLines.Add(string.Format(
        //                "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //            }
        //        }
        //        System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

        //        throw e;
        //    }

        //}


        //public List<HXR_GET_CONFIG_DETAILS_SP_Result> GetConfigurationByType(string client_ID, string project_ID, string source_Target, Nullable<long> tool_ID, ref  string status_Code, ref string message)
        //{
        //    var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
        //    var OutPut_message = new ObjectParameter("message", typeof(string));


        //    try
        //    {


        //      //  var result = _context.HXR_GET_CONFIG_DETAILS_SP(client_ID, project_ID, source_Target, tool_ID, OutPut_status_Code, OutPut_message).ToList<HXR_GET_CONFIG_DETAILS_SP_Result>();
        //      //  var result = new HXR_GET_CONFIG_DETAILS_SP_Result();


        //        status_Code = OutPut_status_Code.Value.ToString();
        //        message = OutPut_message.Value.ToString();
        //        return result;
        //    }
        //    catch (DbEntityValidationException e)
        //    {

        //        var outputLines = new List<string>();
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            outputLines.Add(string.Format(
        //                "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //            }
        //        }
        //        System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

        //        throw e;
        //    }

        //}


        public void WriteErrorLog(Exception ex, string d = "")
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string _filePath = Path.GetFullPath(Path.Combine(filePath, @"ErrorLog.txt"));
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine("Date :" + DateTime.Now.ToString() +
                    " Message :" + d + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine);
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
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
