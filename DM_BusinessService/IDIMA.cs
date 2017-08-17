using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessService
{
    public interface IDIMA
    {
        void TruncateDIMAMappings(string client_ID, string project_ID, ref  string status_Code, ref string message);
        void UpdateDIMAMappings(ref  string status_Code, ref string message);
    }
}
