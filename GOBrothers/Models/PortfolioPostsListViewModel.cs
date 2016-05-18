using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Models
{
    public class PortfolioPostsListViewModel
    {
        public IEnumerable<PortfolioPost> PortfolioPosts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? PortfolioCategoryId { get; set; }
    }
}