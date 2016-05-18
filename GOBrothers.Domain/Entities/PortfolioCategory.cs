using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class PortfolioCategory
    {
        //[Key]
        //[Column("PortfolioCategoryId")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву категорії портфоліо, будь ласка!")]
        public string Name { get; set; }

        public virtual List<PortfolioPost> PortfolioPosts { get; set; }
    }
}
