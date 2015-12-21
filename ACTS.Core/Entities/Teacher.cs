using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
	 public class Teacher : Worker
	{
		public int TeacherID { get; set; }

		public string Degree { get; set; } // Научная степень    

		[EmailAddress]
		public string EMail { get; set; }

		//social Links

		[Url]
		public string Intellect { get; set; }

		[Url]
		public string VkID { get; set; }

		[Url]
		public string FaceBook { get; set; }

		[Url]
		public string Twitter { get; set; }

		// Blog

		public virtual ICollection<Post> Blog { get; set; }
	}
}
