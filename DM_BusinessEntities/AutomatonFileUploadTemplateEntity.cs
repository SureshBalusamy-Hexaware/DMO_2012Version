using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class AutomatonFileUploadTemplateEntity
    {
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Template_Name { get; set; }
        public string Source_Name { get; set; }
        public string File_Location { get; set; }
        public string File_Name { get; set; }
        public string Prefix_FileName { get; set; }
        public string Batch_Portion { get; set; }
        public string Batch_Name_Values { get; set; }
        public string File_Type { get; set; }
        public string File_Delimiter { get; set; }
        public string Data_Starting_Line { get; set; }
        public string Target_Table { get; set; }
        public string Header_Row { get; set; }
        public string Created_By { get; set; }
        public string  Type{ get; set; }
    }
}
