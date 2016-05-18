using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class PortfolioPost
    {
        //[Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву запису портфоліо, будь ласка!")]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Введіть короткий текст запису портфоліо, будь ласка!")]
        public string ShortContent { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Введіть основний текст запису портфоліо, будь ласка!")]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public int PortfolioCategoryId { get; set; }
        public virtual PortfolioCategory PortfolioCategory { get; set; }

        public virtual List<PortfolioImage> PortfolioImages { get; set; }
    }
}
