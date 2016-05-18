using System.Linq;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Concrete
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private GOBrothersContext context = new GOBrothersContext();

        public IQueryable<PortfolioCategory> PortfolioCategories { get { return context.PortfolioCategories; } }
        public IQueryable<PortfolioPost> PortfolioPosts { get { return context.PortfolioPosts; } }
        public IQueryable<PortfolioImage> PortfolioImages { get { return context.PortfolioImages; } }

        public void SavePortfolioCategory(PortfolioCategory portfolioCategory)
        {
            if (portfolioCategory.Id == 0)
            {
                context.PortfolioCategories.Add(portfolioCategory);
            }
            else
            {
                PortfolioCategory dbEntry = context.PortfolioCategories.Find(portfolioCategory.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = portfolioCategory.Name;
                }
            }
            context.SaveChanges();
        }

        public void SavePortfolioPost(PortfolioPost portfolioPost)
        {
            if (portfolioPost.Id == 0)
            {
                context.PortfolioPosts.Add(portfolioPost);
            }
            else
            {
                PortfolioPost dbEntry = context.PortfolioPosts.Find(portfolioPost.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = portfolioPost.Title;
                    dbEntry.ShortContent = portfolioPost.ShortContent;
                    dbEntry.Content = portfolioPost.Content;
                    dbEntry.Created = portfolioPost.Created;
                    dbEntry.PortfolioCategoryId = portfolioPost.PortfolioCategoryId;
                }
            }
            context.SaveChanges();
        }

        public void SavePortfolioImage(PortfolioImage portfolioImage)
        {
            if (portfolioImage.Id == 0)
            {
                context.PortfolioImages.Add(portfolioImage);
            }
            else
            {
                PortfolioImage dbEntry = context.PortfolioImages.Find(portfolioImage.Id);
                if (dbEntry != null)
                {
                    dbEntry.OriginalName = portfolioImage.OriginalName;
                    dbEntry.RndName = portfolioImage.RndName;
                    dbEntry.PortfolioPostId = portfolioImage.PortfolioPostId;
                }
            }
            context.SaveChanges();
        }

        public PortfolioCategory DeletePortfolioCategory(int portfolioCategoryId)
        {
            PortfolioCategory dbEntry = context.PortfolioCategories.Find(portfolioCategoryId);
            if (dbEntry != null)
            {
                context.PortfolioCategories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public PortfolioPost DeletePortfolioPost(int portfolioPostId)
        {
            PortfolioPost dbEntry = context.PortfolioPosts.Find(portfolioPostId);
            if (dbEntry != null)
            {
                context.PortfolioPosts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public PortfolioImage DeletePortfolioImage(int portfolioImageId)
        {
            PortfolioImage dbEntry = context.PortfolioImages.Find(portfolioImageId);
            if (dbEntry != null)
            {
                context.PortfolioImages.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
