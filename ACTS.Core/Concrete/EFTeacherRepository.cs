﻿using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;
using ACTS.Core.Identity;
using System.Collections;

namespace ACTS.Core.Concrete
{
	public class EFTeacherRepository : ITeacherRepository
	{
		private EFDbContext context = new EFDbContext();

		public IQueryable<Teacher> Teachers
		{
			get { return context.Teachers; }
		}

		public IQueryable<Teacher> NoPairTeachers
		{
			get { return context.Teachers.Where(t => t.User == null); }
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

			context.SaveChanges();
		}

		public Teacher DeleteTeacher(int teacherId)
		{
			Teacher dbEntry = context.Teachers.Find(teacherId);
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

			context.SaveChanges();
		}
		public void AddPairToUser(int teacherId, int userId)
		{
			var user = context.Users.Find(userId);
			var teacher = GetTeacherById(teacherId);

			if (user == null || teacher == null) return;

			if (user.HasTeacher ^ teacher.HasUser)
				throw new InvalidOperationException("Teacher or User already have pair.");

			user.Teacher = teacher;
			teacher.User = user;

			context.SaveChanges();
		}

		public void RemovePairToUser(int teacherId)
		{
			var teacher = GetTeacherById(teacherId);

			if (teacher == null) return;

			teacher.User.Teacher = default(Teacher);
			teacher.User = default(ApplicationUser);

			context.SaveChanges();
		}

		public IQueryable<Teacher> GetNoPairTeachersWithSelected(int teacherId)
		{
			return context.Teachers.Where(t => t.User == null || t.TeacherId == teacherId);
		}
	}
}
