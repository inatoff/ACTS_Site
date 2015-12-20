using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
	public class Employee : Worker
	{
		public int EmployeeID { get; set; }
	}
}
