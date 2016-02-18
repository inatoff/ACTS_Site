using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples.Models
{
    public class TeacherAccountViewModel
    { 
        public string Degree { get; set; } // Научная степень     

        [Display(Name = "Visible for all")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Visible only for site administration")]
        [EmailAddress]
        public string AccountEmail { get; set; }

        [Required]
        [Display(Name = "Your current password")]
        public string Password { get; set; }

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Passwords are not the same")]
        public string ConfirmPassword { get; set; }
         
        [Url]
        [Display(Name = "Intellect url")]
        public string Intellect { get; set; }

        [Url]
        [Display(Name = "Vkontakte url")]
        public string Vk { get; set; }

        [Url]
        [Display(Name = "Facebook url")]
        public string Facebook { get; set; }

        [Url]
        [Display(Name = "Twitter url")]
        public string Twitter { get; set; }  
        
        public IList<string> Disciplines { get; set; }

        public IList<string> ScienceInterests { get; set; }

        public virtual IList<string> Projects { get; set; }

        public virtual IList<string> Publications { get; set; }

        public bool HasBlog { get; set; }
    }
}
