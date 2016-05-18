using System.Linq;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Abstract
{
    public interface IBloggingRepository
    {
        IQueryable<Blog> Blogs { get; }
        IQueryable<Post> Posts { get; }
        IQueryable<PostImage> PostImages { get; }

        void SaveBlog(Blog blog);
        void SavePost(Post post);
        void SavePostImage(PostImage postImage);

        Blog DeleteBlog(int blogId);
        Post DeletePost(int postId);
        PostImage DeletePostImage(int postImageId);
    }
}
