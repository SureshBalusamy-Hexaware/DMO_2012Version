using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class ImportMetaDataEntity
    {
        public string ClientID { set; get; }
        public string ProjectID { get; set; }
        public string IPAddress { get; set; }
        public string DBName { get; set; }
        public string SourceTarget { get; set; }
        public string LastModifiedBy { get; set; }
        public string config_ID { get; set; }
        public string TablenameList { get; set; }
        public int GenerateMap { get; set; }
        public string ToolID { get; set; }
        /// <summary>
        /// Status code returned by procedures
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Message returned by procedure
        /// </summary>
        public string Message { get; set; }
    }
}
