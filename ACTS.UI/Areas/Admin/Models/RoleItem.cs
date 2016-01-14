using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class RoleItem
	{
		// для случая когда item может не имеет уникальный Value добавляеться Id
		// так как все роли имеют уникальное имя необходимисть в Id отпадает
		//public int Id { get; set; }
		public string Value { get; set; }
		public bool Selected { get; set; }
	}
}
