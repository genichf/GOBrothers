using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Models
{
    public class AlbumImagesListViewModel
    {
        public IEnumerable<AlbumImage> AlbumImages { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? AlbumId { get; set; }
        public DateTime? AlbumCreated { get; set; }
    }
}