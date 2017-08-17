using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DIMAPLUSCopySliceEntity
    {

        public long No_of_Tables { get; set; }
        public long No_of_Views { get; set; }
        public long No_of_Procedures { get; set; }
        public string Server_IP { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
    }
}
