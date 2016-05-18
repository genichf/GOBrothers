using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    //[Table("Posts", Schema = "dbo")]
    public class Post
    {
        //[Key]
        [HiddenInput(DisplayValue = false)]
        public int Id{ get; set; }
        [Required(ErrorMessage = "Введіть назву статті, будь ласка!")]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Введіть короткий текст статті, будь ласка!")]
        public string ShortContent { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Введіть основний текст статті, будь ласка!")]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public int BlogId { get; set; }
        //[ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        public virtual List<PostImage> PostImages { get; set; }
    }
}
