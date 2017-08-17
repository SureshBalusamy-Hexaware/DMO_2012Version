using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DM_UI.Models
{
    public class RuleCategoryModel
    {

        public long RuleCategory_ID { get; set; }

        //[DisplayName("Name")]
        //[Required(ErrorMessage = "Enter the name")]
        [LocalizedDisplayName("lblRuleCategory")]
        [Required(ErrorMessageResourceType = typeof(DM_en_US), ErrorMessageResourceName = "ErrorMsgRuleCategory")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z ]+$", ErrorMessage = "Alphabets only allowed.")]
        public string RuleCategory_Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description required")]
        public string RuleCategory_Desc { get; set; }

        public long Active_Flag { get; set; }
    }
}