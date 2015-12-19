using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
	public abstract class Worker
	{
		//obligatory Fields

		public string FullName { get; set; } // Полное имя

		//public string Name { get; set; } // Имя

		//public string SecondName { get; set; } // Отчество

		//public string Surname { get; set; } // Фамилия

		public string Position { get; set; } // Должность

		public byte[] Photo { get; set; }

		public string PhotoMimeType { get; set; }
	}
}
