using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class UploadPictureCommand : ICommand
    {
        private readonly IAlbumService albumSrv;
        private readonly IPictureService picSrv;
        private readonly IUserSessionService userSsSrv;

        public UploadPictureCommand(IAlbumService albumSrv, IPictureService picSrv, IUserSessionService userSsSrv)
        {
            this.picSrv = picSrv;
            this.albumSrv = albumSrv;
            this.userSsSrv = userSsSrv;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            var albumTitle = string.Empty;
            var pictureTitle = string.Empty;
            var picturePath = string.Empty;
            try
            {
                albumTitle = data[0];
                pictureTitle = data[1];
                picturePath = data[2];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var album = albumSrv.ByTitle(albumTitle);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumTitle} not found!");
            }

            if (!album.AlbumRoles.Where(ar => ar.Role == Models.Role.Owner).Select(ar => ar.User).Contains(userSsSrv.User))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            picSrv.CreatePicture(album, pictureTitle, picturePath);

            return $"Picture {pictureTitle} added to {albumTitle}!";
        }
    }
}
