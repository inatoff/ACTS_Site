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

		[Required(AllowEmptyStrings = false)]
		[StringLength(70, ErrorMessage = "Full name: Length should not exceed 70 characters")]
		[Display(Name = "Full name*")]
		public string FullName { get; set; } // Полное имя

		//public string Name { get; set; } // Имя

		//public string SecondName { get; set; } // Отчество

		//public string Surname { get; set; } // Фамилия

		[Required(AllowEmptyStrings = false)]
		[StringLength(500, ErrorMessage = "Position: Length should not exceed 500 characters")]
		[Display(Name = "Position*")]
		[DataType(DataType.MultilineText)]
		public string Position { get; set; } // Должность

		[Display(Name = "Photo")]
		public byte[] Photo { get; set; }

		public string PhotoMimeType { get; set; }
	}
}
