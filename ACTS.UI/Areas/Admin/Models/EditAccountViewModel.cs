using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class EditAccountViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[EmailAddress]
		[Display(Name = "Email adress")]
		public string Email { get; set; }

		[Display(Name = "Account roles")]
		public IList<RoleItem> Roles { get; set; }

		[Display(Name = "Teacher")]
		public int? PairTeacherId { get; set; }

		public IEnumerable<string> SelectedRoles { get { return Roles.Where(r => r.Selected).Select(r => r.Value); } }
		public IEnumerable<string> UnselectedRoles { get { return Roles.Where(r => !r.Selected).Select(r => r.Value); } }
	}
}
