using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;
using GOBrothers.Models;
using System.IO;
using GOBrothers.Areas.Admin.Utilites;
using System.Configuration;

namespace GOBrothers.Areas.Admin.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminController : Controller
    {
        private IBloggingRepository repository;
        public int PageSize = 4;
        public AdminController(IBloggingRepository bloggingRepository)
        {
            this.repository = bloggingRepository;
        }
       
        // GET: Admin/Admin
        public ActionResult AdminList(int page = 1)
        {
            Models.AdminPostsListViewModel model = new Models.AdminPostsListViewModel
            {
                Posts = repository.Posts
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Posts.Count()
                },
            };

            return View(model);
        }

        public ViewResult Edit(int postId)
        {
            Post post = repository.Posts
              .FirstOrDefault(p => p.Id == postId);
            //SelectList blogs = new SelectList(repository.Blogs, "Id", "Name");
            List<Blog> listBlogs = repository.Blogs.ToList();
            //где departmentsQuery -  список, с которого фреймворк сам построит DropDownList
            //"NewsMID" - строка с названием столбца из модели, данные строк которой заполнят значения value 
            //"Title" - строка с названием столбца из модели, данные строк которой заполнят значения text (Название, которое будет показываться в списке) 
            //selectedDepartment - значение selected. То что будет выбрано изначально.

            ViewBag.Blogs = new SelectList(listBlogs, "Id", "Name", post.BlogId);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                repository.SavePost(post);
                TempData["message"] = string.Format("Статтю {0} ЗБЕРЕЖЕНО!", post.Title);
                return RedirectToAction("AdminList", new { area = "admin" });
            }
            else
            {
                // there is something wrong with the data values
                return View(post);
            }
        }

        public ViewResult Create()
        {
            List<Blog> listBlogs = repository.Blogs.ToList();
            ViewBag.Blogs = new SelectList(listBlogs, "Id", "Name", repository.Blogs.Select(b => b.Id).Take(1));
            return View("Edit", new Post());
        }

        [HttpPost]
        public ActionResult Delete(int postId)
        {
            Post deletedPost = repository.DeletePost(postId);
            if (deletedPost != null)
            {
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/PostImages"), postId.ToString());
                Directory.Delete(pathToDirId, true);
                TempData["message"] = string.Format("Статтю {0} ВИДАЛЕНО!", deletedPost.Title);
            }
            return RedirectToAction("AdminList", new { area = "admin" });
        }

        public ActionResult LogoUpload()
        {
            return View();
        }

        [HttpPost]
        public string LogoUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["postId"];
                // Path to current Id directory for images
                string pathToDirIdLogo = Path.Combine(Server.MapPath("~/Content/PostImages/"), idDirectory, "Logo");
                // Create current Id directory for images
                Directory.CreateDirectory(pathToDirIdLogo);
                // Create new random file name
                string newFileName = "logo";
                // Full path to big image file
                string pathForBigFile = Path.Combine(pathToDirIdLogo, newFileName + Path.GetExtension(uploadFile.FileName));
                // Full path to small image file (by adding "small_" to file name)
                string pathForSmallFile = Path.Combine(pathToDirIdLogo, newFileName + "_small" + Path.GetExtension(uploadFile.FileName));
                // Save main image file that was resized on client side
                uploadFile.SaveAs(pathForBigFile);
                // Save small image file that will be resize on server side by using of static class ImageUtilites
                ImageUtilites.SaveImageWithServerResize(uploadFile, pathForSmallFile, 200, 150);
            }
            return "File was uploaded successfully!";
        }

        /*public ActionResult ImageUpload()
        {
            return View();
        }*/

        [HttpPost]
        public string ImageUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["postId"];
                // Path to current Id directory for images
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/PostImages"), idDirectory);
                // Create current Id directory for images
                Directory.CreateDirectory(pathToDirId);
                // Create new random file name
                string newRandomFileName = Guid.NewGuid().ToString();
                // Full path to big image file
                string pathForBigFile = Path.Combine(pathToDirId, newRandomFileName + Path.GetExtension(uploadFile.FileName));
                // Full path to small image file (by adding "small_" to file name)
                string pathForSmallFile = Path.Combine(pathToDirId, newRandomFileName + "_small" + Path.GetExtension(uploadFile.FileName));
                // Save main image file that was resized on client side
                uploadFile.SaveAs(pathForBigFile);
                // Save small image file that will be resize on server side by using of static class ImageUtilites
                ImageUtilites.SaveImageWithServerResize(uploadFile, pathForSmallFile, 250, 200);

                // Saving image data to table of database
                int postId;
                int.TryParse(Request.QueryString["postId"], out postId);
                repository.SavePostImage(new PostImage { OriginalName = Path.GetFileNameWithoutExtension(uploadFile.FileName), PostId = postId, RndName = newRandomFileName + Path.GetExtension(uploadFile.FileName) });
            }
            return "Files was uploaded successfully!";
        }
    }
}