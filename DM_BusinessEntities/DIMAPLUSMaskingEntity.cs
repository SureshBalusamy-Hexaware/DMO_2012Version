using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DIMAPLUSMaskingEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string TemplateName { get; set; }
        public string Table_Name { get; set; }
        public int Config_ID { get; set; }
        public string Column_Name { get; set; }
        public string Masking_Type { get; set; }
        public int Un_Mask { get; set; }
        public string Updated_By { get; set; }
        public int Batch_process { get; set; }
    }
}
