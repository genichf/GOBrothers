using System.Linq;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Abstract
{
    public interface IPortfolioRepository
    {
        IQueryable<PortfolioCategory> PortfolioCategories { get; }
        IQueryable<PortfolioPost> PortfolioPosts { get; }
        IQueryable<PortfolioImage> PortfolioImages { get; }

        void SavePortfolioCategory(PortfolioCategory portfolioCategory);
        void SavePortfolioPost(PortfolioPost portfolioPost);
        void SavePortfolioImage(PortfolioImage portfolioImage);

        PortfolioCategory DeletePortfolioCategory(int portfolioCategoryId);
        PortfolioPost DeletePortfolioPost(int portfolioPostId);
        PortfolioImage DeletePortfolioImage(int portfolioImageId);
    }
}
