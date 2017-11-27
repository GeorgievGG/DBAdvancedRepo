using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services.Services
{
    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumRole CreateAlbumRole(Album album, User user)
        {
            var albumRole = new AlbumRole()
            {
                Album = album,
                User = user
            };

            context.AlbumRoles.Add(albumRole);
            context.SaveChanges();

            return albumRole;
        }
    }
}
