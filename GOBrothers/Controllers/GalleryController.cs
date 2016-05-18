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
    public class GalleryController : Controller
    {
        private IAlbumRepository repository;
        public int PageSize = 10;
        public int PageSizeImages = 12;
        public GalleryController(IAlbumRepository albumRepository)
        {
            repository = albumRepository;
        }
        // GET: Albums
        public ActionResult AlbumsList(int page = 1)
        {
            AlbumsListViewModel model = new AlbumsListViewModel
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

        // GET: Album
        public ActionResult AlbumById(int albumId, int page = 1)
        {
            AlbumImagesListViewModel model = new AlbumImagesListViewModel
            {
                AlbumImages = repository.AlbumImages
                .Where(i => i.AlbumId == albumId)
                .OrderBy(i => i.Id)
                .Skip((page - 1) * PageSizeImages)
                .Take(PageSizeImages).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSizeImages,
                    TotalItems =
                    repository.AlbumImages.Where(e => e.AlbumId == albumId).Count()
                },
                AlbumId = albumId
            };
            if (albumId != null)
            {
                ViewBag.AlbumName = repository.Albums.Where(a => a.Id == albumId).Select(a => a.Name).FirstOrDefault().ToString();
            }
            return View(model);
        }

        // GET: AlbumImages
        public ActionResult ImagesList(int? albumId, int page = 1)
        {
            AlbumImagesListViewModel model = new AlbumImagesListViewModel
            {
                AlbumImages = repository.AlbumImages
                .Where(i => albumId == null || i.AlbumId == albumId)
                .OrderBy(i => i.Id)
                .Skip((page - 1) * PageSizeImages)
                .Take(PageSizeImages).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSizeImages,
                    TotalItems = albumId == null ?
                    repository.AlbumImages.Count() :
                    repository.AlbumImages.Where(e => e.AlbumId == albumId).Count()
                },
                AlbumId = albumId
            };
            if (albumId != null)
            {
                ViewBag.AlbumName = repository.Albums.Where(a => a.Id == albumId).Select(a => a.Name).FirstOrDefault().ToString();
            }
            return View(model);
        }

        // GET: Image
        public ActionResult ImageById(int albumImageId)
        {
            AlbumImage model = repository.AlbumImages.Where(i => i.Id == albumImageId).FirstOrDefault();
            int albumId = model.AlbumId;
            ViewBag.albumId = albumId;
            return View(model);
        }
    }
}