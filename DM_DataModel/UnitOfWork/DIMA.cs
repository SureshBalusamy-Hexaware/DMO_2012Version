using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_DataModel.UnitOfWork
{
    public class DIMA : IDisposable
    {
        #region Private member variables...
        DM_MetaDataEntities _context = null;
        #endregion
        public DIMA()
        {
            _context = new DM_MetaDataEntities();
        }
        #region Public member methods....

        public void TruncateDIMAMappings(string client_ID, string project_ID, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                _context.DIMA_DELETE_MAPPING_MS_SP(client_ID, project_ID, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value == null ? "" : OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value == null ? "" : OutPut_message.Value.ToString();
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

        public void UpdateDIMAMappings(ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                _context.DIMA_UPDATE_TABLE_TYPE_SP(OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value == null ? "" : OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value == null ? "" : OutPut_message.Value.ToString();
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
