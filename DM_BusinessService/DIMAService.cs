using DM_DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessService
{
    public class DIMAService : IDIMA
    {
        private readonly DIMA _DIMA;
        public DIMAService()
        {
            _DIMA = new DIMA();
        }
        public void TruncateDIMAMappings(string client_ID, string project_ID, ref string status_Code, ref string message)
        {
            _DIMA.TruncateDIMAMappings(client_ID, project_ID, ref status_Code, ref message);
        }


        public void UpdateDIMAMappings(ref string status_Code, ref string message)
        {
            _DIMA.UpdateDIMAMappings(ref status_Code, ref message);
        }
    }
}
