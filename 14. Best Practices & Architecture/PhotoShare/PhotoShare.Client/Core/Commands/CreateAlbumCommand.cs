using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Utilities;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumTagService atSrv;
        private readonly IAlbumRoleService arSrv;
        private readonly IAlbumService albumSrv;
        private readonly IColorService colorSrv;
        private readonly IUserService userSrv;
        private readonly ITagService tagSrv;
        private readonly IUserSessionService userSsSrv;


        public CreateAlbumCommand(IAlbumTagService atSrv, IAlbumRoleService arSrv, IAlbumService albumSrv, IColorService colorSrv, IUserService userSrv, ITagService tagSrv, IUserSessionService userSsSrv)
        {
            this.atSrv = atSrv;
            this.arSrv = arSrv;
            this.userSrv = userSrv;
            this.albumSrv = albumSrv;
            this.colorSrv = colorSrv;
            this.tagSrv = tagSrv;
            this.userSsSrv = userSsSrv;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var username = string.Empty;
            var albumTitle = string.Empty;
            var bgColor = string.Empty;
            string[] tags = null;
            try
            {
                username = data[0];
                albumTitle = data[1];
                bgColor = data[2];
                tags = data.Skip(3).ToArray();
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }
            var user = userSrv.ByUsername(username);

            if (user != userSsSrv.User)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }
            if (albumSrv.ByTitle(albumTitle) != null)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            Color color;

            try
            {
                color = colorSrv.ByColorString(bgColor);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            var tgs = new List<Tag>();
            foreach (var tag in tags)
            {
                var tg = tagSrv.ByName(tag.ValidateOrTransform());
                if (tg == null)
                {
                    throw new ArgumentException("Invalid tags!");
                }
                else
                {
                    tgs.Add(tg);
                }
            }

            var album = albumSrv.CreateAlbum(albumTitle, color);

            var albumRole = arSrv.CreateAlbumRole(album, user);

            foreach (var tag in tgs)
            {
                var albumTag = atSrv.CreateAlbumTag(album, tag);
            }

            return $"Album {album.Name} successfully created!";
        }
    }
}
