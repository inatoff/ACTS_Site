using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public abstract class Worker
	{
		//obligatory Fields

		[Required(ErrorMessage = "Please enter a name")]
		[StringLength(70, ErrorMessage = "Full name: Length should not exceed 70 characters")]
		[Display(Name = "Full name")]
		public string FullName { get; set; } // Полное имя

		//public string Name { get; set; } // Имя

		//public string SecondName { get; set; } // Отчество

		//public string Surname { get; set; } // Фамилия

		[Required(ErrorMessage = "Please enter a position")]
		[StringLength(500, ErrorMessage = "FullName: Length should not exceed 500 characters")]
		public string Position { get; set; } // Должность

		[Display(Name = "Image")]
		public byte[] Photo { get; set; }

		public string PhotoMimeType { get; set; }
	}
}
