using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class RoleMenuEntity
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public int Parent_Menu_ID { get; set; }
        public string Menu_Type { get; set; }
        public bool Active_Flag { get; set; }
    }
}
