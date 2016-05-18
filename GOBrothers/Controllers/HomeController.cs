using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GOBrothers.Domain.Concrete;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;
using GOBrothers.Models;

namespace GOBrothers.Controllers
{
    public class HomeController : Controller
    {
        private IBloggingRepository repository;
        public HomeController(IBloggingRepository bloggingRepository)
        {
            this.repository = bloggingRepository;
        }
        public ActionResult Index()
        {
            PostsListViewModel model = new PostsListViewModel { Posts = repository.Posts.OrderByDescending(p => p.Id).Take(3).ToList() };
            return View(model);
        }
    }
}