using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumRoleService arSrv;
        private readonly IAlbumService albumSrv;
        private readonly IUserService userSrv;
        private readonly IUserSessionService userSsSrv;

        public ShareAlbumCommand(IAlbumRoleService arSrv, IAlbumService albumSrv, IUserService userSrv, IUserSessionService userSsSrv)
        {
            this.arSrv = arSrv;
            this.userSrv = userSrv;
            this.albumSrv = albumSrv;
            this.userSsSrv = userSsSrv;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            var albumId = 0;
            var username = string.Empty;
            var role = string.Empty;
            try
            {
                albumId = int.Parse(data[0]);
                username = data[1];
                role = data[2];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var album = albumSrv.ByID(albumId);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (!album.AlbumRoles.Where(ar => ar.Role == Models.Role.Owner).Select(ar => ar.User).Contains(userSsSrv.User))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var user = userSrv.ByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (role != "Owner" && role != "Viewer")
            {
                throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
            }

            arSrv.CreateAlbumRole(album, user, role);

            return $"Username {username} added to album {album.Name} ({role})";
        }
    }
}
