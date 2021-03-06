﻿using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Peoples.Models
{
	public class PersonalPageViewModel
	{
		public string FullName { get; }
		public string Degree { get; }
		public string Position { get; }
		public string Email { get; }
		public string Intellect { get; }
		public string Vk { get; }
		public string Facebook { get; }
		public string Twitter { get; }

		public string Greetings { get; }
		public Rank Rank { get; }
		public Blog Blog { get; }

		public string Slug { get; }
		public IEnumerable<Discipline> Disciplines { get; set; }

		public IEnumerable<ScienceInterest> ScienceInterests { get; set; }

		public IEnumerable<Project> Projects { get; set; }

		public IEnumerable<Publication> Publications { get; set; }

		[Display(Name = nameof(DisplayRes.PhotoName), ResourceType = typeof(DisplayRes))]
		public Guid? PhotoId { get; set; }

		public PersonalPageViewModel(Teacher teacher)
		{
			FullName = teacher.FullName;
			Degree = teacher.Degree;
			Greetings = teacher.Greetings;
			Email = teacher.Email;
			Slug = teacher.NameSlug;
			Intellect = teacher.Intellect;
			Vk = teacher.Vk;
			Facebook = teacher.Facebook;
			Twitter = teacher.Twitter;
			Blog = teacher.Blog;
			Disciplines = teacher.Disciplines;
			ScienceInterests = teacher.ScienceInterests;
			Projects = teacher.Projects;
			Publications = teacher.Publications;
			PhotoId = teacher.PhotoId;
			Position = teacher.Position;
			Rank = teacher.Rank;
		}
		public PersonalPageViewModel() { }
	}
}
