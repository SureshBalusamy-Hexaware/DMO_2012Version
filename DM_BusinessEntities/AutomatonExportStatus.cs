using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class AutomatonExportStatus
    {
        public string Client_ID { get; set; }

        public string Project_ID { get; set; }

        public string Table_Name { get; set; }

        public long Config_ID { get; set; }

        public string Output_Format { get; set; }



        public string File_Name { get; set; }

        public string Run_User { get; set; }

        public System.DateTime Run_Date { get; set; }




        public string Download_XML { get; set; }

        public string Download_CSV { get; set; }

        public string Download_XLSX { get; set; }

        public string Download_XLS { get; set; }

        public string Download_Others { get; set; }

        public string Folder_Path { get; set; }

        public Nullable<System.DateTime> Last_Run { get; set; }
        public long ID { get; set; }

    }
}
