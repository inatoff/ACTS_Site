using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Models
{
	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Адрес электронной почты")]
		public string EmailOrLogin { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool RememberMe { get; set; }
	}
}
