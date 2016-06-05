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

		public Post DeletePost(int id)
		{
			Post dbEntry = _context.Posts.Find(id);
			if (dbEntry != null)
			{
				_context.Posts.Remove(dbEntry);
				_context.SaveChanges();
			}
			_context.SaveChanges();

			return dbEntry;
		}

		public async Task<Post> GetPostAsync(int id)
		{
			return await _context.Posts.FindAsync(id);
		}

		public void CreatePost(Post post)
		{
			post.Created = DateTime.UtcNow;

			_context.Posts.Add(post);
			_context.SaveChanges();
		}

		public async Task EditPost(int id, Post updatedPost)
		{
			var post = await _context.Posts.SingleOrDefaultAsync(p => p.PostId == id);

			if (post == null)
			{
				throw new KeyNotFoundException($"A post with the id of {id} does not exist in the data store.");
			}

			post.Modified = DateTime.UtcNow;

			post.PostId = updatedPost.PostId;
			post.Title = updatedPost.Title;
			post.Tags = updatedPost.Tags;

			await _context.SaveChangesAsync();
		}

		public Blog GetBlogByAuthorNameSlug(string slug)
		{
			var blog = _context.Blogs.FirstOrDefault(b => b.Teacher.NameSlug == slug);
			return blog;
		}

		public async Task<Blog> InitPersonalPage(Teacher teacher)
		{
			var dbEntry = await _context.Teachers.FindAsync(teacher);
			var blog = new Blog();
			dbEntry.Blog = blog;
			await _context.SaveChangesAsync();

			return blog;
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
