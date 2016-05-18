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

namespace GOBrothers.Areas.Admin.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminPortfolioController : Controller
    {
        private IPortfolioRepository repository;
        public int PageSize = 4;
        public AdminPortfolioController(IPortfolioRepository portfolioRepository)
        {
            this.repository = portfolioRepository;
        }

        // GET: Admin/Admin
        public ActionResult AdminPortfolioList(int page = 1)
        {
            Models.AdminPortfolioPostsListViewModel model = new Models.AdminPortfolioPostsListViewModel
            {
                PortfolioPosts = repository.PortfolioPosts
                .OrderBy(pp => pp.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.PortfolioPosts.Count()
                },
            };

            return View(model);
        }

        public ViewResult PortfolioEdit(int portfolioPostId)
        {
            PortfolioPost portfolioPost = repository.PortfolioPosts
              .FirstOrDefault(pp => pp.Id == portfolioPostId);

            List<PortfolioCategory> listPortfolioCategories = repository.PortfolioCategories.ToList();
 
            ViewBag.PortfolioCategories = new SelectList(listPortfolioCategories, "Id", "Name", portfolioPost.PortfolioCategoryId);
            return View(portfolioPost);
        }

        [HttpPost]
        public ActionResult PortfolioEdit(PortfolioPost portfolioPost)
        {
            if (ModelState.IsValid)
            {
                portfolioPost.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                repository.SavePortfolioPost(portfolioPost);
                TempData["message"] = string.Format("Запис портфоліо {0} ЗБЕРЕЖЕНО!", portfolioPost.Title);
                return RedirectToAction("AdminPortfolioList", new { area = "admin" });
            }
            else
            {
                // there is something wrong with the data values
                return View(portfolioPost);
            }
        }

        public ViewResult PortfolioCreate()
        {
            List<PortfolioCategory> listPortfolioCategories = repository.PortfolioCategories.ToList();
            ViewBag.PortfolioCategories = new SelectList(listPortfolioCategories, "Id", "Name", repository.PortfolioCategories.Select(b => b.Id).Take(1));
            return View("PortfolioEdit", new PortfolioPost());
        }

        [HttpPost]
        public ActionResult PortfolioDelete(int portfolioPostId)
        {
            PortfolioPost deletedPortfolioPost = repository.DeletePortfolioPost(portfolioPostId);
            if (deletedPortfolioPost != null)
            {
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/PortfolioImages"), portfolioPostId.ToString());
                Directory.Delete(pathToDirId, true);
                TempData["message"] = string.Format("Запис портфоліо {0} ВИДАЛЕНО!", deletedPortfolioPost.Title);
            }
            return RedirectToAction("AdminPortfolioList", new { area = "admin" });
        }

        public ActionResult PortfolioLogoUpload()
        {
            return View();
        }

        [HttpPost]
        public string PortfolioLogoUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["portfolioPostId"];
                // Path to current Id directory for images
                string pathToDirIdLogo = Path.Combine(Server.MapPath("~/Content/PortfolioImages/"), idDirectory, "Logo");
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
        public string PortfolioImageUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["portfolioPostId"];
                // Path to current Id directory for images
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/PortfolioImages"), idDirectory);
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
                int portfolioPostId;
                int.TryParse(Request.QueryString["portfolioPostId"], out portfolioPostId);
                repository.SavePortfolioImage(new PortfolioImage { OriginalName = Path.GetFileNameWithoutExtension(uploadFile.FileName), PortfolioPostId = portfolioPostId, RndName = newRandomFileName + Path.GetExtension(uploadFile.FileName) });
            }
            return "Files was uploaded successfully!";
        }
    }
}