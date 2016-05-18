using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOBrothers.Models;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Areas.Admin.Controllers
{
    public class AdminMenuController : Controller
    {
        private IBloggingRepository bloggingRepository;
        private IPortfolioRepository portfolioRepository;
        private IAlbumRepository albumRepository;
        public AdminMenuController(IBloggingRepository bloggingRepository, IPortfolioRepository portfolioRepository, IAlbumRepository albumRepository)
        {
            this.bloggingRepository = bloggingRepository;
            this.portfolioRepository = portfolioRepository;
            this.albumRepository = albumRepository;
        }
        // GET: Menu
        public ActionResult AdminItems()
        {
            List<Blog> blogs = bloggingRepository.Blogs.ToList();

            List<PortfolioCategory> portfolioCategories = portfolioRepository.PortfolioCategories.ToList();

            List<Album> albums = albumRepository.Albums.ToList();
            List<AlbumImage> albumImages = albumRepository.AlbumImages.ToList();

            ViewBag.blogs = blogs;

            ViewBag.portfolioCategories = portfolioCategories;

            ViewBag.albums = albums;

            ViewBag.albumImages = albumImages;

            return PartialView();
        }
    }
}