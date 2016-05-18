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
    public class PostController : Controller
    {
        private IBloggingRepository repository;
        public int PageSize = 4;
        public PostController(IBloggingRepository bloggingRepository)
        {
            repository = bloggingRepository;
        }
        
        // GET: Posts
        public ActionResult List(int? blogId, int page = 1)
        {
            PostsListViewModel model = new PostsListViewModel
            {
                Posts = repository.Posts
                .Where(p => blogId == null || p.BlogId == blogId)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = blogId == null ?
                    repository.Posts.Count() :
                    repository.Posts.Where(e => e.BlogId == blogId).Count()
                },
                BlogId = blogId
            };

            return View(model);
        }

        // GET: Post
        public ActionResult PostById(int postId)
        {
            Post model = repository.Posts.Where(p => p.Id == postId).FirstOrDefault();
            int blogId = model.BlogId;
            ViewBag.blogId = blogId;
            return View(model);
        }
    }
}