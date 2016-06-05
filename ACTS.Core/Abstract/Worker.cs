using ACTS.Localization;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public abstract class Worker : IHaveFileId
	{
		//obligatory Fields

		[CustomRequired]
		[CustomMaxLength(70)]
		[Display(Name = nameof(DisplayRes.FullNameName), ResourceType = typeof(DisplayRes))]
		public string FullName { get; set; } // Полное имя

		//public string Name { get; set; } // Имя

		//public string SecondName { get; set; } // Отчество

		//public string Surname { get; set; } // Фамилия

		[CustomRequired]
		[CustomMaxLength(500)]
		[Display(Name = nameof(DisplayRes.PositionName), ResourceType = typeof(DisplayRes))]
		[DataType(DataType.MultilineText)]
		public string Position { get; set; } // Должность

		[Display(Name = nameof(DisplayRes.PhotoName), ResourceType = typeof(DisplayRes))]
		public Guid? PhotoId { get; set; }

		Guid? IHaveFileId.FileId { get { return PhotoId; } set { PhotoId = value; } }

		public Guid? FileId { get { return ((IHaveFileId)this).FileId; } set { ((IHaveFileId)this).FileId = value; } }
	}
}
