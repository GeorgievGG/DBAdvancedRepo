using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Utilities;
using PhotoShare.Services.Contracts;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumTagService atSrv;
        private readonly IAlbumService albumSrv;
        private readonly ITagService tagSrv;
        private readonly IUserSessionService userSsSrv;

        public AddTagToCommand(IAlbumTagService atSrv, IAlbumService albumSrv, ITagService tagSrv, IUserSessionService userSsSrv)
        {
            this.atSrv = atSrv;
            this.albumSrv = albumSrv;
            this.tagSrv = tagSrv;
            this.userSsSrv = userSsSrv;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            var albumTitle = string.Empty;
            var tagName = string.Empty;
            try
            {
                albumTitle = data[0];
                tagName = data[1].ValidateOrTransform();
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var album = albumSrv.ByTitle(albumTitle);

            if (!album.AlbumRoles.Where(ar => ar.Role == Models.Role.Owner).Select(ar => ar.User).Contains(userSsSrv.User))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var tag = tagSrv.ByName(tagName);

            if (album == null || tag == null)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            return $"Tag {tag} added to {albumTitle}!";
        }
    }
}
