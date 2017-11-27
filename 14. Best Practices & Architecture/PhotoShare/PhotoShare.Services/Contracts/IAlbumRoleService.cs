using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumRoleService
    {
        AlbumRole CreateAlbumRole(Album album, User user);

        AlbumRole CreateAlbumRole(Album album, User user, string role);
    }
}
