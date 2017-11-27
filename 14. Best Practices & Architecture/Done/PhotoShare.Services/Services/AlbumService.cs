using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Album ByID(int id)
        {
            return context.Albums.SingleOrDefault(a => a.Id == id);
        }

        public Album ByTitle(string albumTitle)
        {
            return context.Albums.SingleOrDefault(a => a.Name == albumTitle);
        }

        public Album CreateAlbum(string albumTitle, Color BgColor)
        {
            var album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = BgColor,
                IsPublic = false
            };

            context.Albums.Add(album);
            context.SaveChanges();

            return album;
        }
    }
}
