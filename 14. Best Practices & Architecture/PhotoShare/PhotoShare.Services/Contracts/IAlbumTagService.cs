using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumTagService
    {
        AlbumTag CreateAlbumTag(Album album, Tag tag);
    }
}
