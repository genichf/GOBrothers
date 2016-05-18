using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOBrothers.Domain.Entities;
using GOBrothers.Models;

namespace GOBrothers.Areas.Admin.Models
{
    public class AdminAlbumsListViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}