namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Services.Contracts;
    using System;
    using System.Linq;

    public class CreateAlbumCommand
    {
        private readonly IAlbumTagService atSrv;
        private readonly IAlbumRoleService arSrv;
        private readonly IAlbumService albumSrv;
        private readonly IColorService colorSrv;

        public CreateAlbumCommand(IAlbumTagService atSrv, IAlbumRoleService arSrv, IAlbumService albumSrv)
        {
            this.atSrv = atSrv;
            this.arSrv = arSrv;
            this.albumSrv = albumSrv;
        }
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var username = data[1];
            var albumTitle = data[2];
            var bgColor = data[3];
            var tags = data.Skip(4);



            var album = albumSrv.CreateAlbum(albumTitle, Models.Color.Black);

            return "";
        }
    }
}
