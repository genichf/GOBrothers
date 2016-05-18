using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class Album
    {
        //[Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву альбома, будь ласка!")]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public virtual List<AlbumImage> AlbumImages { get; set; }
    }
}
