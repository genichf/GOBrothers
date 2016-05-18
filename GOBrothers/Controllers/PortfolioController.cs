using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOBrothers.Domain.Abstract;
using GOBrothers.Models;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Controllers
{
    public class PortfolioController : Controller
    {
        private IPortfolioRepository repository;
        public int PageSize = 4;
        public PortfolioController(IPortfolioRepository portfolioRepository)
        {
            repository = portfolioRepository;
        }

        // GET: PortfolioPosts
        public ActionResult PortfolioList(int? portfolioCategoryId, int page = 1)
        {
            PortfolioPostsListViewModel model = new PortfolioPostsListViewModel
            {
                PortfolioPosts = repository.PortfolioPosts
                .Where(pp => portfolioCategoryId == null || pp.PortfolioCategoryId == portfolioCategoryId)
                .OrderBy(pp => pp.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = portfolioCategoryId == null ?
                    repository.PortfolioPosts.Count() :
                    repository.PortfolioPosts.Where(e => e.PortfolioCategoryId == portfolioCategoryId).Count()
                },
                PortfolioCategoryId = portfolioCategoryId
            };

            return View(model);
        }

        // GET: PortfolioPost
        public ActionResult PortfolioPostById(int portfolioPostId)
        {
            PortfolioPost model = repository.PortfolioPosts.Where(pp => pp.Id == portfolioPostId).FirstOrDefault();
            int portfolioCategoryId = model.PortfolioCategoryId;
            ViewBag.portfolioCategoryId = portfolioCategoryId;
            return View(model);
        }
    }
}