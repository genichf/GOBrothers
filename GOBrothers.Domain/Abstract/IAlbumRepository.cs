using System.Linq;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Abstract
{
    public interface IAlbumRepository
    {
        IQueryable<Album> Albums { get; }
        IQueryable<AlbumImage> AlbumImages { get; }

        void SaveAlbum(Album album);
        void SaveAlbumImage(AlbumImage albumImage);

        Album DeleteAlbum(int albumId);
        AlbumImage DeleteAlbumImage(int albumImageId);
    }
}
