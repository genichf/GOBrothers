using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class PostImage
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string RndName { get; set; }

        public int PostId { get; set; }
        public virtual Post Posts { get; set; }
    }
}
