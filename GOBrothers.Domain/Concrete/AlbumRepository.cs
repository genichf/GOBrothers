using System;
using System.Linq;
using GOBrothers.Domain.Abstract;
using GOBrothers.Domain.Entities;

namespace GOBrothers.Domain.Concrete
{
    public class AlbumRepository : IAlbumRepository
    {
        private GOBrothersContext context = new GOBrothersContext();

        public IQueryable<Album> Albums { get { return context.Albums; } }
        public IQueryable<AlbumImage> AlbumImages { get { return context.AlbumImages; } }

        public void SaveAlbum(Album album)
        {
            if (album.Id == 0)
            {
                context.Albums.Add(album);
            }
            else
            {
                Album dbEntry = context.Albums.Find(album.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = album.Name;
                    dbEntry.Created = album.Created;
                }
            }
            context.SaveChanges();
        }

        public void SaveAlbumImage(AlbumImage albumImage)
        {
            if (albumImage.Id == 0)
            {
                context.AlbumImages.Add(albumImage);
            }
            else
            {
                AlbumImage dbEntry = context.AlbumImages.Find(albumImage.Id);
                if (dbEntry != null)
                {
                    dbEntry.OriginalName = albumImage.OriginalName;
                    dbEntry.RndName = albumImage.RndName;
                    dbEntry.AlbumId = albumImage.AlbumId;
                }
            }
            context.SaveChanges();
        }

        public Album DeleteAlbum(int albumId)
        {
            Album dbEntry = context.Albums.Find(albumId);
            if (dbEntry != null)
            {
                context.Albums.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public AlbumImage DeleteAlbumImage(int albumImageId)
        {
            AlbumImage dbEntry = context.AlbumImages.Find(albumImageId);
            if (dbEntry != null)
            {
                context.AlbumImages.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
