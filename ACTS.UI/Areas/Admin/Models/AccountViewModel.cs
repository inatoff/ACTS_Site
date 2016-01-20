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
