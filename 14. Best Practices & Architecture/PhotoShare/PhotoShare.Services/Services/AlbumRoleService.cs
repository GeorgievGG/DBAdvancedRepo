using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;

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
            return this.CreateAlbumRole(album, user, null);
        }

        public AlbumRole CreateAlbumRole(Album album, User user, string role)
        {
            var selectedRole = Role.Owner;

            if (role != null)
            {
                selectedRole = (Role)Enum.Parse(typeof(Role), role);
            }

            var albumRole = new AlbumRole()
            {
                Album = album,
                User = user,
                Role = selectedRole
            };

            context.AlbumRoles.Add(albumRole);
            context.SaveChanges();

            return albumRole;
        }
    }
}
