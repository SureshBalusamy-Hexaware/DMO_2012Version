using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DM_UI.Models
{
    public class RuleModel
    {
        

        [DisplayName("Rule")]
        [Required(ErrorMessage = "Enter Rule name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z ]+$", ErrorMessage = "Alphabets only allowed.")]
        public string Rule_Name { get; set; }

        [DisplayName("Default value")]
        [Required(ErrorMessage = "Enter Default value")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only alphanumerics are accepted.")]
        public string Default_value { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Condition")]
        [Required(ErrorMessage = "Enter Condition")]
        public string Conditional_Clause { get; set; }

    }
}