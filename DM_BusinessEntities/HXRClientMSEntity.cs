using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRClientMSEntity
    {
        /// <summary>
        /// Db_Column: Client_ID; DataType: varchar;
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Db_Column: Client_Name; DataType: varchar;
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Db_Column: Active_Flag; DataType: bigint;
        /// </summary>
        public int ActiveFlag { get; set; }

        /// <summary>
        /// Db_Column: Last_Modified_Date; DataType: Datetime
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Db_Column: Last_Modified_By; DataType: varchar;
        /// </summary>
        public string LastModifiedBy { get; set; }
    }
}
