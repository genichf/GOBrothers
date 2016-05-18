using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Models
{
    public class AlbumsListViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}