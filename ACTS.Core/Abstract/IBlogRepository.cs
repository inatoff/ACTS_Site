using ACTS.Core.Entities;
using System.Linq;

namespace ACTS.Core.Abstract
{
	public interface IBlogRepository
	{
		IQueryable<Post> Posts { get; }
        IQueryable<Blog> Blogs { get; }
		void CreatePost(Post post);
        void EditPost(Post post);
        Blog GetBlogByAuthorNameSlug(string slug);
		Post GetPostById(int postId);
		Post DeletePost(int postID);
	}
}