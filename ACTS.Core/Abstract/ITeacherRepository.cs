using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ACTS.Core.Abstract
{
	public interface ITeacherRepository
	{
		IQueryable<Teacher> Teachers { get; }
		IQueryable<Teacher> NoPairTeachers { get; }
		IQueryable<Teacher> GetNoPairTeachersWithSelected(int teacherId);
		void CreateTeacher(Teacher teacher);
		void UpdateTeacher(Teacher teacher);
		void AddPairToUser(int teacherId, int userId);
		void RemovePairToUser(int teacherId);
		Teacher GetTeacherById(int teacherId);
        Teacher GetTeacherByUrlSlug(string urlSlug);
        Teacher DeleteTeacher(int teacherID);
	}
}
