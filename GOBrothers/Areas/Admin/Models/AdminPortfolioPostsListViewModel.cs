using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;
using GOBrothers.Models;

namespace GOBrothers.Areas.Admin.Models
{
    public class AdminPortfolioPostsListViewModel
    {
        public IEnumerable<PortfolioPost> PortfolioPosts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}