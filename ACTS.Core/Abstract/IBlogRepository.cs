using ACTS.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface IBlogRepository
	{
		IQueryable<Post> Posts { get; }
        IQueryable<Blog> Blogs { get; }
		void CreatePost(Post post);
        Task EditPost(int postId, Post post);
        Blog GetBlogByAuthorNameSlug(string slug);
		Task<Post> GetPostByIdAsync(int postId);
		Post DeletePost(int postID);
        Task InitPersonalPage(Teacher teacher);
        Task<IQueryable<Post>> GetPostsByTag(string[] tags);
	}
}