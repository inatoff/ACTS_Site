using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;
using ACTS.Core.Identity;
using System.Collections;
using System.Data.Entity;

namespace ACTS.Core.Concrete
{
	public class EFTeacherRepository : ITeacherRepository
	{
		private EFDbContext _context = new EFDbContext();
		public IQueryable<Teacher> Teachers
		{
			get { return _context.Teachers; }
		}
		public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
		{
			return await _context.Teachers.ToListAsync();
		}
		public IQueryable<Teacher> NoPairTeachers
		{
			get { return _context.Teachers.Where(t => t.User == null); }
		}

		public void SaveTeacher(Teacher teacher)
		{
			if (teacher.TeacherId == 0)
			{
				_context.Teachers.Add(teacher);
			} else
			{
				Teacher dbEntry = _context.Teachers.Find(teacher.TeacherId);
				if (dbEntry != null)
				{
					dbEntry.FullName = teacher.FullName;
					dbEntry.Position = teacher.Position;
					dbEntry.Degree = teacher.Degree;
					dbEntry.Rank = teacher.Rank;
					dbEntry.Photo = teacher.Photo;
					dbEntry.PhotoMimeType = teacher.PhotoMimeType;
					dbEntry.Email = teacher.Email;
					// social Links
					dbEntry.Intellect = teacher.Intellect;
					dbEntry.Vk = teacher.Vk;
					dbEntry.Facebook = teacher.Facebook;
					dbEntry.Twitter = teacher.Twitter;
				}
			}

			_context.SaveChanges();
		}

		public Teacher DeleteTeacher(int teacherId)
		{
			Teacher teacher = GetTeacherById(teacherId);
			if (teacher != null)
			{
				if (teacher.HasUser)
					RemovePairToUser(teacherId);

				if (teacher.HasBlog)
				{
					_context.Posts.RemoveRange(teacher.Blog.Posts);
					_context.Blogs.Remove(teacher.Blog);
				}

				_context.Teachers.Remove(teacher);
				_context.SaveChanges();
			}
			return teacher;
		}


		public async Task<Teacher> GetTeacherByUrlSlugAsync(string nameSlug)
		{
			return await Teachers.FirstOrDefaultAsync(p => p.NameSlug == nameSlug);
		}

		public Teacher GetTeacherById(int teacherId)
		{
			return _context.Teachers.Find(teacherId);
		}

		public void CreateTeacher(Teacher teacher)
		{
			_context.Teachers.Add(teacher);
			_context.SaveChanges();
		}

		public void UpdateTeacher(Teacher teacher)
		{
			Teacher dbEntry = _context.Teachers.Find(teacher.TeacherId);
			if (dbEntry != null)
			{
				dbEntry.FullName = teacher.FullName;
				dbEntry.Position = teacher.Position;
				dbEntry.Degree = teacher.Degree;
				dbEntry.Rank = teacher.Rank;
				dbEntry.Photo = teacher.Photo;
				dbEntry.PhotoMimeType = teacher.PhotoMimeType;
				dbEntry.Email = teacher.Email;
				dbEntry.NameSlug = teacher.NameSlug;
				// social Links
				dbEntry.Intellect = teacher.Intellect;
				dbEntry.Vk = teacher.Vk;
				dbEntry.Facebook = teacher.Facebook;
				dbEntry.Twitter = teacher.Twitter;

				_context.SaveChanges();
			}
		}

		public void UpdateTeacherByProfile(int id, Teacher teacher)
		{
			Teacher dbEntry = _context.Teachers.Find(id);
			if (dbEntry != null)
			{                  
				dbEntry.Degree = teacher.Degree;
				dbEntry.Email = teacher.Email;
				// social Links
				dbEntry.Intellect = teacher.Intellect;
				dbEntry.Vk = teacher.Vk;
				dbEntry.Facebook = teacher.Facebook;
				dbEntry.Twitter = teacher.Twitter;

				dbEntry.Disciplines = teacher.Disciplines;
				dbEntry.Projects = teacher.Projects;
				dbEntry.Publications = teacher.Publications;
				dbEntry.ScienceInterests = teacher.ScienceInterests;
			}

			_context.SaveChanges();
		}


		public void AddPairToUser(int teacherId, int userId)
		{
			var user = _context.Users.Find(userId);
			var teacher = GetTeacherById(teacherId);

			if (user == null || teacher == null) return;

			if (user.HasTeacher ^ teacher.HasUser)
				throw new InvalidOperationException("Teacher or User already have pair.");

			user.Teacher = teacher;
			teacher.User = user;

			_context.SaveChanges();
		}

		public void RemovePairToUser(int teacherId)
		{
			var teacher = GetTeacherById(teacherId);

			if (teacher == null) return;

			teacher.User.Teacher = default(Teacher);
			teacher.User = default(ApplicationUser);

			_context.SaveChanges();
		}

		public IQueryable<Teacher> GetNoPairTeachersWithSelected(int teacherId)
		{
			return _context.Teachers.Where(t => t.User == null || t.TeacherId == teacherId);
		}


		public async Task InitPersonalPage(Teacher teacher)
		{
			var dbEntry = GetTeacherById(teacher.TeacherId);
			dbEntry.Blog = new Blog();
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetCurrentUserTeacherIdAsync(int userId)
		{
			var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == currentUser.Teacher.TeacherId);
			return teacher.TeacherId;
		}
	}
}
