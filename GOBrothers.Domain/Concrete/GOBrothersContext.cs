using System.Data.Entity;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Concrete
{
    public class GOBrothersContext : DbContext
    {
        public GOBrothersContext() : base("DB_9FAC9F_GOBrothers")
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }

        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<PortfolioPost> PortfolioPosts { get; set; }
        public DbSet<PortfolioImage> PortfolioImages { get; set; }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumImage> AlbumImages { get; set; }
    }
}
