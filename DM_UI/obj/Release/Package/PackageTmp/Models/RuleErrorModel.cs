using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DM_UI.Models
{
    public class RuleErrorModel
    {        

        [DisplayName("Code")]
        [Required(ErrorMessage = "Enter the code")]
        [RegularExpression(@"^[0-9][0-9]+$", ErrorMessage = "Only numerics are accepted.")]
        public string Error_Code { get; set; }

        [DisplayName("Error Description")]
        [Required(ErrorMessage = "Enter the description")]
        public string Error_Description { get; set; }

        public long Active_Flag { get; set; }
    }
}