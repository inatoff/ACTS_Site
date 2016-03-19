using ACTS.Localization;
using ACTS.Localization.Resources;
using ACTS.Localization.Resources.GlobalResources; 
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

		[CustomRequired]
		[CustomMinLength(5)]
		[Display(Name = nameof(DisplayRes.UserNameName), ShortName = nameof(DisplayRes.UserNameShortName), ResourceType = typeof(DisplayRes))]
		public string UserName { get; set; }

		[EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
		[Display(Name = nameof(DisplayRes.EmailAddressName), ResourceType = typeof(DisplayRes))]
		public string Email { get; set; }

		[Display(Name = nameof(DisplayRes.RolesName), ResourceType = typeof(DisplayRes))]
		public IList<RoleItem> Roles { get; set; }

		[Display(Name = nameof(DisplayRes.PairTeacherIdName), ResourceType = typeof(DisplayRes))]
		public int? PairTeacherId { get; set; }

		public IEnumerable<string> SelectedRoles { get { return Roles.Where(r => r.Selected).Select(r => r.Value); } }
		public IEnumerable<string> UnselectedRoles { get { return Roles.Where(r => !r.Selected).Select(r => r.Value); } }
	}
}
