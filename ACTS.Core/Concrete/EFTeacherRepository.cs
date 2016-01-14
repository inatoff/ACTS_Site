using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class EFTeacherRepository : ITeacherRepository
	{
		private EFDbContext context = new EFDbContext();

		public IQueryable<Teacher> Teachers
		{
			get { return context.Teachers; }
		}

		public void SaveTeacher(Teacher teacher)
		{
			if (teacher.TeacherId == 0)
			{
				context.Teachers.Add(teacher);
			} else
			{
				Teacher dbEntry = context.Teachers.Find(teacher.TeacherId);
				if (dbEntry != null)
				{
					dbEntry.FullName = teacher.FullName;
					dbEntry.Position = teacher.Position;
					dbEntry.Degree = teacher.Degree;
					dbEntry.Photo = teacher.Photo;
					dbEntry.PhotoMimeType = teacher.PhotoMimeType;
					dbEntry.EMail = teacher.EMail;
					// social Links
					dbEntry.Intellect = teacher.Intellect;
					dbEntry.Vk = teacher.Vk;
					dbEntry.FaceBook = teacher.FaceBook;
					dbEntry.Twitter = teacher.Twitter;
				}
			}

			context.SaveChanges();
		}

		public Teacher DeleteTeacher(int teacherID)
		{
			Teacher dbEntry = context.Teachers.Find(teacherID);
			if (dbEntry != null)
			{
				context.Teachers.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}


        public Teacher GetTeacherByUrlSlug(string nameSlug)
        {
            return Teachers.FirstOrDefault(p => p.NameSlug == nameSlug);
        }

        public Teacher GetTeacherById(int teacherId)
		{
			return Teachers.FirstOrDefault(p => p.TeacherId == teacherId);
		}

		public void CreateTeacher(Teacher teacher)
		{
			context.Teachers.Add(teacher);
			context.SaveChanges();
		}

		public void UpdateTeacher(Teacher teacher)
		{
			Teacher dbEntry = context.Teachers.Find(teacher.TeacherId);
			if (dbEntry != null)
			{
				dbEntry.FullName = teacher.FullName;
				dbEntry.Position = teacher.Position;
				dbEntry.Degree = teacher.Degree;
				dbEntry.Photo = teacher.Photo;
				dbEntry.PhotoMimeType = teacher.PhotoMimeType;
				dbEntry.EMail = teacher.EMail;
				// social Links
				dbEntry.Intellect = teacher.Intellect;
				dbEntry.Vk = teacher.Vk;
				dbEntry.FaceBook = teacher.FaceBook;
				dbEntry.Twitter = teacher.Twitter;
			}

			context.SaveChanges();
		}

    }
}
