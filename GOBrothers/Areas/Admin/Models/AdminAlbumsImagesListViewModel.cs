using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;
using GOBrothers.Models;

namespace GOBrothers.Areas.Admin.Models
{
    public class AdminAlbumsImagesListViewModel
    {
        public IEnumerable<AlbumImage> AlbumImages { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? AlbumId { get; set; }
    }
}