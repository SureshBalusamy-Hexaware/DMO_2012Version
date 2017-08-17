using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRGetClientEntity
    {
        public long ID { get; set; }
        public string Client_ID { get; set; }
        public string Client_Name { get; set; }
        public string project_ID { get; set; }
        public Nullable<int> Role_ID { get; set; }
        public long User_ID { get; set; }
        public byte IsAdmin { get; set; }
    }
}
