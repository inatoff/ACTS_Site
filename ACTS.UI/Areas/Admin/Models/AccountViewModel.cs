using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ACTS.Core.Identity;

namespace ACTS.UI.Areas.Admin.Models
{
    public class AccountViewModel
    {
        [Required]
        [Display(Name = "User name")] 
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } 

        [Required]
        [Display(Name ="Email adress")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string SelectedRole { get; set; }

        [Display(Name = "Account roles")]
        public List<string> Roles { get; set; }
        
    }
}
