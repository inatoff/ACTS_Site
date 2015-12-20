using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface ITeacherRepository
	{
		IQueryable<Teacher> Teachers { get; }
		void SaveTeacher(Teacher teacher);
		Teacher DeleteTeacher(int teacherID);
	}
}
