using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class InfoAccountViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public IList<string> Roles { get; set; }
		public string TeacherName { get; set; }
	}
}
