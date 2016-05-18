using System.Web.Mvc;

namespace GOBrothers.Domain.Entities
{
    public class AlbumImage
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string RndName { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Albums { get; set; }
    }
}
