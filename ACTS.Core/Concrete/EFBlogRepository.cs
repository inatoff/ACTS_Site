using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACTS.Core.Entities;
using System.Data.Entity;

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
            else
            {
                throw new KeyNotFoundException($"The post with the id of {postId} does not exist!");
            }
            return dbEntry;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await Posts.FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public void CreatePost(Post post)
        {
            if (post.PostId == 0)
            {
                post.Created = DateTime.UtcNow;
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

        public async Task EditPost(int postId, Post updatedPost)
        {
            var post = await context.Posts.SingleOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                throw new KeyNotFoundException($"A post with the id of {postId} does not exist in the data store.");
            }
            post.PostId = updatedPost.PostId;
            post.Title = updatedPost.Title;
            post.Modified = updatedPost.Modified;
            post.Tags = updatedPost.Tags;

            await context.SaveChangesAsync();
        }

        public Blog GetBlogByAuthorNameSlug(string slug)
        {
			var blog = context.Blogs.FirstOrDefault(b => b.Teacher.NameSlug == slug);
			return blog;
		}

        public async Task InitPersonalPage(Teacher teacher)
        {
            var dbEntry = await context.Teachers.FindAsync(teacher);
            dbEntry.Blog = new Blog();
            await context.SaveChangesAsync();
        }

        //TODO
        public Task<IQueryable<Post>> GetPostsByTag(string[] tags)
        {
            throw new NotImplementedException();
        }
    }
}
