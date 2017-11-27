using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumRoleService
    {
        AlbumTag CreateAlbumRole(Album album, User user);
    }
}
