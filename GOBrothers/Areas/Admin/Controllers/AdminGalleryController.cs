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
    public class AdminGalleryController : Controller
    {
        private IAlbumRepository repository;
        public int PageSize = 10;
        public AdminGalleryController(IAlbumRepository albumRepository)
        {
            this.repository = albumRepository;
        }

        // GET: Albums
        public ActionResult AdminAlbumsList(int page = 1)
        {
            Models.AdminAlbumsListViewModel model = new Models.AdminAlbumsListViewModel
            {
                Albums = repository.Albums
                .OrderBy(a => a.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Albums.Count()
                }
            };

            return View(model);
        }

        public ViewResult AlbumEdit(int albumId)
        {
            Album album = repository.Albums.FirstOrDefault(a => a.Id == albumId);
            return View(album);
        }

        [HttpPost]
        public ActionResult AlbumEdit(Album album)
        {
            if (ModelState.IsValid)
            {
                album.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                repository.SaveAlbum(album);
                TempData["message"] = string.Format("Альбом {0} ЗБЕРЕЖЕНО!", album.Name);
                return RedirectToAction("AdminAlbumsList", new { area = "admin" });
            }
            else
            {
                // there is something wrong with the data values
                return View(album);
            }
        }

        public ViewResult AlbumCreate()
        {
            return View("AlbumEdit", new Album());
        }

        [HttpPost]
        public ActionResult AlbumDelete(int albumId)
        {
            Album deletedAlbum = repository.DeleteAlbum(albumId);
            if (deletedAlbum != null)
            {
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/GalleryImages"), albumId.ToString());
                Directory.Delete(pathToDirId, true);
                TempData["message"] = string.Format("Альбом {0} ВИДАЛЕНО!", deletedAlbum.Name);
            }
            return RedirectToAction("AdminAlbumsList", new { area = "admin" });
        }
        //----------------------------
        // GET: Admin/Admin
        public ActionResult AdminImagesList(int page = 1)
        {
            Models.AdminAlbumsImagesListViewModel model = new Models.AdminAlbumsImagesListViewModel
            {
                AlbumImages = repository.AlbumImages
                .OrderBy(i => i.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.AlbumImages.Count()
                },
            };

            return View(model);
        }

        public ViewResult AlbumImageEdit(int albumImageId)
        {
            AlbumImage albumImage = repository.AlbumImages
              .FirstOrDefault(i => i.Id == albumImageId);

            List<Album> listAlbums = repository.Albums.ToList();

            ViewBag.Albums = new SelectList(listAlbums, "Id", "Name", albumImage.AlbumId);
            return View(albumImage);
        }

        [HttpPost]
        public ActionResult AlbumImageEdit(AlbumImage albumImage)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAlbumImage(albumImage);
                TempData["message"] = string.Format("Зображення в альбом {0} ЗБЕРЕЖЕНО!", albumImage.OriginalName);
                return RedirectToAction("AdminImagesList", new { area = "admin" });
            }
            else
            {
                // there is something wrong with the data values
                return View(albumImage);
            }
        }

        public ViewResult AlbumImageCreate()
        {
            List<Album> listAlbums = repository.Albums.ToList();
            ViewBag.Albums = new SelectList(listAlbums, "Id", "Name", repository.Albums.Select(a => a.Id).Take(1));
            return View("AlbumImageEdit", new AlbumImage());
        }

        [HttpPost]
        public ActionResult AlbumImageDelete(int albumImageId)
        {
            AlbumImage deletedAlbumImage = repository.DeleteAlbumImage(albumImageId);
            if (deletedAlbumImage != null)
            {
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/GalleryImages"), deletedAlbumImage.AlbumId.ToString());
                Directory.Delete(pathToDirId, true);
                TempData["message"] = string.Format("Зображення з альбому {0} ВИДАЛЕНО!", deletedAlbumImage.OriginalName);
            }
            return RedirectToAction("AdminImagesList", new { area = "admin" });
        }
        //----------------------------
        public ActionResult AlbumLogoUpload()
        {
            return View();
        }

        [HttpPost]
        public string AlbumLogoUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["albumId"];
                // Path to current Id directory for images
                string pathToDirIdLogo = Path.Combine(Server.MapPath("~/Content/GalleryImages/"), idDirectory, "Logo");
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

        public ActionResult AlbumImageUpload()
        {
            return View();
        }

        [HttpPost]
        public string AlbumImageUpload(HttpPostedFileBase uploadFile)
        {
            uploadFile = Request.Files[0];
            if (uploadFile.ContentLength > 0)
            {
                // Original name of image file
                string fileName = Path.GetFileName(uploadFile.FileName);
                // Directory name for store images for current post by Id (directory name = "Id" of current post)
                string idDirectory = Request.QueryString["albumId"];
                // Path to current Id directory for images
                string pathToDirId = Path.Combine(Server.MapPath("~/Content/GalleryImages"), idDirectory);
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
                int albumId;
                int.TryParse(Request.QueryString["albumId"], out albumId);
                repository.SaveAlbumImage(new AlbumImage { OriginalName = Path.GetFileNameWithoutExtension(uploadFile.FileName), AlbumId = albumId, RndName = newRandomFileName + Path.GetExtension(uploadFile.FileName) });
            }
            return "Files was uploaded successfully!";
        }
    }
}