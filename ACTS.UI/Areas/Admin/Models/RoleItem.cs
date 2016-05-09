using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public class RoleItem
	{
		public RoleItem(string value, bool selected)
		{
			Value = value;
			Selected = selected;
		}

		public RoleItem(string value) 
		{
			Value = value;
		}

		public RoleItem()
		{
		}

		// для случая когда Item может не имеет уникальный Value добавляеться Id
		// так как все роли имеют уникальное имя необходимисть в Id отпадает
		public string Value { get; set; }
		public bool Selected { get; set; }
	}
}
