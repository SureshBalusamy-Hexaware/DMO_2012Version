using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRRuleValidationResultsEntity
    {
        public Nullable<long> pass_count { get; set; }
        public Nullable<long> number_errors { get; set; }
        public Nullable<long> Fail_Count { get; set; }
        public Nullable<long> total_records { get; set; }
        public Nullable<long> Run_Id { get; set; }
    }
}
