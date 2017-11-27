using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services.Services
{
    public class AlbumTagService : IAlbumTagService
    {
        private readonly PhotoShareContext context;

        public AlbumTagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumTag CreateAlbumTag(Album album, Tag tag)
        {
            var albumTag = new AlbumTag()
            {
                Album = album,
                Tag = tag
            };

            context.AlbumTags.Add(albumTag);
            context.SaveChanges();

            return albumTag;
        }
    }
}
