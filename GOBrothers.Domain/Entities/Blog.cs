using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    //[Table("Blogs", Schema = "dbo")]
    public class Blog
    {
        //[Key]
        //[Column("BlogId")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву рубрики, будь ласка!")]
        public string Name { get; set; }
        
        public virtual List<Post> Posts { get; set; }
    }
}
