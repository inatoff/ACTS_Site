using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
	public class Employee
	{
		//obligatory Fields
		public int EmployeeID { get; set; }

		public string Name { get; set; } // Имя

		public string SecondName { get; set; } // Отчество

		public string Surname { get; set; } // Фамилия

		public string Post { get; set; } // Должность

		public byte[] Photo { get; set; } 

		//optional Fields

		public string Degree { get; set; } // Научная степень    
		
		public string EMail { get; set; }

		//social Links

		public string Intellect { get; set; }

		public string VkID { get; set; }

		public string FaceBook { get; set; }

		public string Twitter { get; set; }

		//TODO:  Dictionary<Date,Publication>  Blog???

		
	}
}
