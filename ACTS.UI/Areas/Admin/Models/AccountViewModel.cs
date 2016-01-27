using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ACTS.Core.Identity;
using System.Web.Mvc;

namespace ACTS.UI.Areas.Admin.Models
{
	public class AccountViewModel
	{
		[Required]
		[MinLength(5)]
		[Display(Name = "User name*")]
		[Remote("doesUserNameExist", "Account", AreaReference.UseRoot, HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
		public string UserName { get; set; }

		[Required]
		[Display(Name = "Password*")]
		[StringLength(100, ErrorMessage = "The {0} have to be {2} characters.", MinimumLength = 8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Confirm password*")]
		[DataType(DataType.Password)]
		[System.ComponentModel.DataAnnotations.Compare(nameof(Password), ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } 

		[Display(Name ="Email adress")]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "Account roles")]
		public IList<RoleItem> Roles { get; set; }

		public IEnumerable<string> SelectedRoles { get { return Roles.Where(r => r.Selected).Select(r => r.Value); } }

		[Display(Name = "Teacher")]
		public int? PairTeacherId { get; set; }
	}
}
