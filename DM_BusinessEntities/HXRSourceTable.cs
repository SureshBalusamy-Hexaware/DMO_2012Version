using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRSourceTable
    {
        public string TableName { get; set; }
        public Nullable<long> Rule_cateogry_ID { get; set; }
        public Nullable<long> Rule_ID { get; set; }
        public string PrimaryKeyCol { get; set; }
        public string PrimaryKeyValue { get; set; }
        public string UpdateCol { get; set; }
        public string UpdateVal { get; set; }
        public string update_all { get; set; }
        public Nullable<long> run_id { get; set; }
        public Nullable<long> ConfigID { get; set; }
        public string status_Code { get; set; }
        public string message { get; set; }
    }
}
