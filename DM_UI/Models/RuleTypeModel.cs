using System.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Resources;


namespace DM_UI.Models
{
    public class RuleTypeModel
    {
        public long RuleType_ID { get; set; }

        //[Required(ErrorMessage = "Enter the type")]
        [Required(ErrorMessageResourceType = typeof(DM_en_US), ErrorMessageResourceName = "ErrorMsgRuleType")]
        [LocalizedDisplayName("lblRuleType")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z ]+$", ErrorMessage = "Alphabets only allowed.")]
        //[MaxLength(14)]
        public string RuleType_Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description required")]
        //[MaxLength(37)]
        public string RuleType_Desc { get; set; }

        public long Active_Flag { get; set; }
    }
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId)
            : base(GetMessageFromResource(resourceId))
        { }
        private static string GetMessageFromResource(string resourceId)
        {
            // TODO: Return the string from the resource file            
            return HttpContext.GetGlobalResourceObject("DM_en_US", resourceId).ToString();
        }
    }
}