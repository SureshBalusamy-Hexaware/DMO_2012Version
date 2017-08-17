using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRTargetConfigurationMSEntity
    {
        /// <summary>
        /// DB_Column:Source_Target; DataType: nvarchar
        /// </summary>
        public string SourceTarget { get; set; }

        public long ToolID { get; set; }

        public long Config_ID { get; set; }
        /// <summary>
        /// DB_Column:DB_TYPE; DataType: nvarchar
        /// </summary>
        public string DBType { get; set; }

        /// <summary>
        /// DB_Column:SERVER_NAME; DataType: nvarchar
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// DB_Column:SERVER_IP_ADDRESS; DataType: nvarchar
        /// </summary>
        public string ServerIPAddress { get; set; }

        /// <summary>
        /// DB_Column:SERVER_PORT; DataType: nvarchar
        /// </summary>
        public string ServerPort { get; set; }

        /// <summary>
        /// DB_Column:DATABASE_NAME; DataType: nvarchar
        /// </summary>
        public string DataBaseName { get; set; }

        /// <summary>
        /// DB_Column:DATABASE_PASSWORD; DataType: nvarchar
        /// </summary>
        public string DataBasePassword { get; set; }

        /// <summary>
        /// DB_Column:SCHEMA_NAME; DataType: nvarchar
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// DB_Column:SCHEMA_PASSWORD; DataType: nvarchar
        /// </summary>
        public string SchemaPassword { get; set; }

        /// <summary>
        /// DB_Column:ACTIVE_FLAG; DataType: nvarchar
        /// </summary>
        public string ActiveFlag { get; set; }

        /// <summary>
        /// DB_Column:OS_Username; DataType: nvarchar
        /// </summary>
        public string OSUserName { get; set; }

        /// <summary>
        /// DB_Column:OS_PASSWORD; DataType: nvarchar
        /// </summary>
        public string OSPassword { get; set; }
    }
}
