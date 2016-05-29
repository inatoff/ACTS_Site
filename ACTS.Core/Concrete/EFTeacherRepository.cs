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
using ACTS.Core.Concrete;
using System.Linq.Expressions;

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

		[Obsolete]
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
					dbEntry.Greetings = teacher.Greetings;
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

				_context.Disciplines.RemoveRange(teacher.Disciplines);
				_context.ScienceInterests.RemoveRange(teacher.ScienceInterests);
				_context.Projects.RemoveRange(teacher.Projects);
				_context.Publications.RemoveRange(teacher.Publications);

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

		public Teacher GetTeacherByIdWithLoadedProp(int teacherId, params Expression<Func<Teacher, object>> [] includes)
		{
			var teachers = Teachers;
			foreach (var include in includes)
				teachers = teachers.Include(include);

			return teachers.FirstOrDefault(t => t.TeacherId == teacherId);
		}

		public void CreateTeacher(Teacher teacher)
		{
			_context.Teachers.Add(teacher);
			_context.SaveChanges();
		}

		public void UpdateTeacher(Teacher teacher)
		{

			UpdateSet(teacher.Disciplines, teacher);
			UpdateSet(teacher.ScienceInterests, teacher);
			UpdateSet(teacher.Projects, teacher);
			UpdateSet(teacher.Publications, teacher);

			_context.Entry(teacher).State = EntityState.Modified;

			_context.SaveChanges();
		}

		private void UpdateSet<TOrdItem>(ISet<TOrdItem> target, Teacher teacher) 
			where TOrdItem: OrderedItem<string, int>
		{
			var teacherOrdSet = _context.Set<TOrdItem>()
										.AsNoTracking()
										.Where(oi => oi.TeacherId == teacher.TeacherId)
										.ToList();
			//add and update items
			foreach (var ordItem in target)
			{
				var updatedItem = teacherOrdSet.FirstOrDefault(oi => oi.Order == ordItem.Order);
				ordItem.TeacherId = teacher.TeacherId;
				if (updatedItem == null)
					//add item
					_context.Entry(ordItem).State = EntityState.Added;
				else
				{
					//update item
					ordItem.OrderedItemId = updatedItem.OrderedItemId;
					_context.Entry(ordItem).State = EntityState.Modified;
				}
			}


			// delete items
			foreach (var ordItem in teacherOrdSet)
				if (!target.Any(oi => oi.Order == ordItem.Order))
					_context.Entry(ordItem).State = EntityState.Deleted;
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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFTeacherRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}

	internal class OrderedItemComparer<TOrdItem>: IEqualityComparer<TOrdItem> 
		where TOrdItem : OrderedItem<string,int> 
	{
		public bool Equals(TOrdItem x, TOrdItem y)
		{
			return x.Order == y.Order && x.Value == y.Value;
		}

		public int GetHashCode(TOrdItem obj)
		{
			unchecked // Overflow is fine, just wrap
			{
				var hash = 936392;
				hash = (hash * 846239) ^ obj.Order.GetHashCode();
				hash = (hash * 846239) ^ obj.Value.GetHashCode();
				return hash;
			}
		}
	}
}
