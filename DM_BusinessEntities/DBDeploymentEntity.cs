using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DBDeployment
    {
        public string SchemaName { get; set; }

        public string SchemaPassword { get; set; }

        public string ServerIP { get; set; }

        public string ServerPort { get; set; }

        public string DBUser { get; set; }

        public string DBPassword { get; set; }

        public string DBType { get; set; }
    }
}
