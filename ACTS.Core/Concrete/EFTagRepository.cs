using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Concrete
{
	public class EFTagRepository : ITagRepository
	{
		private EFDbContext _context = new EFDbContext();

		public IEnumerable<string> GetAll()
		{
			var tagsCollection = _context.Posts.Select(p => p.CombinedTags).ToList();
			return string.Join(",", tagsCollection).Split(',').Distinct();
		}

		public string Get(string tag)
		{
			var posts = _context.Posts.Where(post => post.CombinedTags.Contains(tag)).ToList();
			posts = posts.Where(post =>
				   post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
				   .ToList();

			if (!posts.Any())
			{
				throw new KeyNotFoundException($"The tag \"{tag}\" does not exist.");
			}

			return tag.ToLower();
		}

		public void Edit(string existingTag, string newTag)
		{
			var posts = _context.Posts.Where(post => post.CombinedTags.Contains(existingTag)).ToList();

			posts = posts.Where(post =>
					post.Tags.Contains(existingTag, StringComparer.CurrentCultureIgnoreCase))
					.ToList();

			if (!posts.Any())
			{
				throw new KeyNotFoundException($"The tag \"{existingTag}\" does not exist.");
			}

			foreach (var post in posts)
			{
				post.Tags.Remove(existingTag);
				post.Tags.Add(newTag);
			}

			_context.SaveChanges();
		}

		public void Delete(string tag)
		{
			var posts = _context.Posts.Where(post => post.CombinedTags.Contains(tag))
					.ToList();

			posts = posts.Where(post =>
				post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
				.ToList();

			if (!posts.Any())
			{
				throw new KeyNotFoundException($"The tag \"{tag}\" does not exist.");
			}

			foreach (var post in posts)
			{
				post.Tags.Remove(tag);
			}

			_context.SaveChanges();
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
		// ~EFTagRepository() {
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
