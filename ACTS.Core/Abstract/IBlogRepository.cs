using ACTS.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface IBlogRepository: IDisposable
	{
		IQueryable<Post> Posts { get; }
		IQueryable<Blog> Blogs { get; }
		void CreatePost(Post post);
		Task EditPost(int id, Post post);
		Blog GetBlogByAuthorNameSlug(string slug);
		Task<Post> GetPostAsync(int id);
		Post DeletePost(int id);
		Task InitPersonalPage(Teacher teacher);
		Task<IQueryable<Post>> GetPostsByTag(string[] tags);
	}
}