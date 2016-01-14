using ACTS.Core.Entities;
using System.Linq;

namespace ACTS.Core.Abstract
{
	public interface IPostRepository
	{
		IQueryable<Post> Posts { get; }
		void SavePost(Post post);
		Post GetPostById(int postId);
		Post DeletePost(int postId);
	}
}