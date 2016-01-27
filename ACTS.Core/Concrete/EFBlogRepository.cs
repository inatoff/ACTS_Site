using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class EFBlogRepository : IBlogRepository
	{
		private EFDbContext context = new EFDbContext();

		public IQueryable<Post> Posts
		{
			get { return context.Posts; }
		}

        public IQueryable<Blog> Blogs
        {
            get { return context.Blogs; }
        } 

		public Post DeletePost(int postId)
		{
			Post dbEntry = context.Posts.Find(postId);
			if (dbEntry != null)
			{
				context.Posts.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}

		public Post GetPostById(int postId)
		{
			return Posts.FirstOrDefault(p => p.PostId == postId);
		}

        public void CreatePost(Post post)
        {
            if (post.PostId == 0)
            {
                post.Create = DateTime.UtcNow;
                context.Posts.Add(post);
            }
            else
            {
                Post dbEntry = context.Posts.Find(post.PostId);
                if (dbEntry != null)
                {
                    dbEntry.Title = post.Title;
                    dbEntry.Modified = DateTime.UtcNow;
                    dbEntry.Content = post.Content;
                }
            }

            context.SaveChanges();
        }

        public void EditPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Blog GetBlogByAuthorNameSlug(string slug)
        {
            return Blogs.FirstOrDefault(b => b.Teacher.NameSlug == slug);
        }
    } 
}
