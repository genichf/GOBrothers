using System.Linq;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Concrete
{
    public class BloggingRepository : IBloggingRepository
    {
        private GOBrothersContext context = new GOBrothersContext();

        public IQueryable<Blog> Blogs { get { return context.Blogs; } }
        public IQueryable<Post> Posts { get { return context.Posts; } }
        public IQueryable<PostImage> PostImages { get { return context.PostImages; } }

        public void SaveBlog(Blog blog)
        {
            if (blog.Id == 0)
            {
                context.Blogs.Add(blog);
            }
            else
            {
                Blog dbEntry = context.Blogs.Find(blog.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = blog.Name;
                }
            }
            context.SaveChanges();
        }

        public void SavePost(Post post)
        {
            if (post.Id == 0)
            {
                context.Posts.Add(post);
            }
            else
            {
                Post dbEntry = context.Posts.Find(post.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = post.Title;
                    dbEntry.ShortContent = post.ShortContent;
                    dbEntry.Content = post.Content;
                    dbEntry.Created = post.Created;
                    dbEntry.BlogId = post.BlogId;
                }
            }
            context.SaveChanges();
        }

        public void SavePostImage(PostImage postImage)
        {
            if (postImage.Id == 0)
            {
                context.PostImages.Add(postImage);
            }
            else
            {
                PostImage dbEntry = context.PostImages.Find(postImage.Id);
                if (dbEntry != null)
                {
                    dbEntry.OriginalName = postImage.OriginalName;
                    dbEntry.RndName = postImage.RndName;
                    dbEntry.PostId = postImage.PostId;
                }
            }
            context.SaveChanges();
        }

        public Blog DeleteBlog(int blogId)
        {
            Blog dbEntry = context.Blogs.Find(blogId);
            if (dbEntry != null)
            {
                context.Blogs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
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

        public PostImage DeletePostImage(int postImageId)
        {
            PostImage dbEntry = context.PostImages.Find(postImageId);
            if (dbEntry != null)
            {
                context.PostImages.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
