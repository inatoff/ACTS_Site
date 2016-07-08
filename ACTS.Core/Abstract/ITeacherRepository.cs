using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;

namespace ACTS.Core.Abstract
{
	public interface ITeacherRepository: IDisposable
	{
		IQueryable<Teacher> Teachers { get; }
		IQueryable<Teacher> NoPairTeachers { get; }

		IQueryable<Teacher> GetNoPairTeachersWithSelected(int id);
		void CreateTeacher(Teacher teacher);
		void UpdateTeacher(Teacher teacher);
		void UpdateTeacherByProfile(int id, Teacher teacher);
		void AddPairToUser(int teacherId, int userId);
		void RemovePairToUser(int id);
		Teacher GetTeacher(int id);
		Teacher GetTeacherByIdWithLoadedProp(int id, params Expression<Func<Teacher, object>> [] includes);
		Teacher DeleteTeacher(int id);
		Task<Teacher> GetTeacherByUrlSlugAsync(string urlSlug);
		Task<int> GetCurrentUserTeacherIdAsync(int userId);
		Task<IEnumerable<Teacher>> GetAllTeachersAsync();
		Task InitPersonalPage(Teacher teacher);
	}
}
