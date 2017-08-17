using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class RoleEntity
    {
        public long Role_ID { get; set; }
        public long Client_ID { get; set; }
        public long Project_ID { get; set; }
        public string Role_Name { get; set; }
        public string Role_Description { get; set; }
        public bool Active_Flag { get; set; }
        public bool Sys_Admin { get; set; }
    }
}
