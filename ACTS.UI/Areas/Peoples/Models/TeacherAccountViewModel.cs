using ACTS.Core.Entities;
using ACTS.Localization.Resources;
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
        public string FullName { get; set; }
        public string Degree { get; set; }

        [Display(Name = nameof(DisplayRes.EmailContacts), ResourceType = typeof(DisplayRes))]
        [EmailAddress]
        public string Email { get; set; }

        [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
        [Display(Name = nameof(DisplayRes.EmailPersonal), ResourceType = typeof(DisplayRes))]
        public string AccountEmail { get; set; }

        [HiddenInput]
        public string Slug { get; set; }

        [Required]
        [Display(Name = nameof(DisplayRes.PasswordName), ResourceType = typeof(DisplayRes))]
        public string Password { get; set; }

        [Display(Name = nameof(DisplayRes.NewPasswordName), ResourceType = typeof(DisplayRes))]
        public string NewPassword { get; set; }

        [Display(Name = nameof(DisplayRes.ConfirmPasswordName), ResourceType = typeof(DisplayRes))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Passwords are not the same")]
        public string ConfirmPassword { get; set; }

        
        [Display(Name = nameof(DisplayRes.Greetings), ResourceType = typeof(DisplayRes))]
        [DataType(DataType.MultilineText)]
        public string Greetings { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.IntellectName), ResourceType = typeof(DisplayRes))]
        public string Intellect { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.VkontakteName), ResourceType = typeof(DisplayRes))]
        public string Vk { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.FacebookName), ResourceType = typeof(DisplayRes))]
        public string Facebook { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.TwitterName), ResourceType = typeof(DisplayRes))]
        public string Twitter { get; set; }


        public ISet<Discipline> Disciplines { get; set; }

        public ISet<ScienceInterest> ScienceInterests { get; set; }

        public virtual ISet<Project> Projects { get; set; }

        public virtual ISet<Publication> Publications { get; set; }

        public bool HasBlog { get; set; }
    }
}
