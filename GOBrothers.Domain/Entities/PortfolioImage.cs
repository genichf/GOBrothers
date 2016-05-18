using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class PortfolioImage
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string RndName { get; set; }

        public int PortfolioPostId { get; set; }
        public virtual PortfolioPost PortfolioPosts { get; set; }
    }
}
