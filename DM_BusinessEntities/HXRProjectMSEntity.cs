using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRProjectMSEntity
    {
        /// <summary>
        /// Db_Column: Project_ID; DataType: varchar;
        /// </summary>
        public string ProjectID { get; set; }

        /// <summary>
        /// Db_Column: Project_Description; DataType: nvarchar;
        /// </summary>
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Db_Column: Client_ID; DataType: varchar;
        /// </summary>
        public HXRClientMSEntity Client { get; set; }

        /// <summary>
        /// Db_Column: Active_Flag; DataType: varchar;
        /// </summary>
        public string ActiveFlag { get; set; }

        /// <summary>
        /// Db_Column: Modified_Date; DataType: datetime;
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Db_Column: Modified_By; DataType: varchar;
        /// </summary>
        public string ModifiedBy { get; set; }  
    }
}
