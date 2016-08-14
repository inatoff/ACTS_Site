using ACTS.Core.Abstract;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
	public class Employee : Worker
	{
		[HiddenInput(DisplayValue = false)]
		public int EmployeeId { get; set; }


		public override string ToString()
		{
			return $"{FullName} ({GlobalRes.Employee})";
		}
	}
}
