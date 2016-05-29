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
		private EFDbContext _context = new EFDbContext();

		public IQueryable<Post> Posts
		{
			get { return _context.Posts; }
		}

		public IQueryable<Blog> Blogs
		{
			get { return _context.Blogs; }
		}

		public Post DeletePost(int postId)
		{
			Post dbEntry = _context.Posts.Find(postId);
			if (dbEntry != null)
			{
				_context.Posts.Remove(dbEntry);
				_context.SaveChanges();
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
				_context.Posts.Add(post);
			}
			else
			{
				Post dbEntry = _context.Posts.Find(post.PostId);
				if (dbEntry != null)
				{
					dbEntry.Title = post.Title;
					dbEntry.Modified = DateTime.UtcNow;
					dbEntry.Content = post.Content;
				}
			}

			_context.SaveChanges();
		}

		public async Task EditPost(int postId, Post updatedPost)
		{
			var post = await _context.Posts.SingleOrDefaultAsync(p => p.PostId == postId);

			if (post == null)
			{
				throw new KeyNotFoundException($"A post with the id of {postId} does not exist in the data store.");
			}
			post.PostId = updatedPost.PostId;
			post.Title = updatedPost.Title;
			post.Modified = updatedPost.Modified;
			post.Tags = updatedPost.Tags;

			await _context.SaveChangesAsync();
		}

		public Blog GetBlogByAuthorNameSlug(string slug)
		{
			var blog = _context.Blogs.FirstOrDefault(b => b.Teacher.NameSlug == slug);
			return blog;
		}

		public async Task InitPersonalPage(Teacher teacher)
		{
			var dbEntry = await _context.Teachers.FindAsync(teacher);
			dbEntry.Blog = new Blog();
			await _context.SaveChangesAsync();
		}

		//TODO
		public Task<IQueryable<Post>> GetPostsByTag(string[] tags)
		{
			throw new NotImplementedException();
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
		// ~EFBlogRepository() {
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
}
