using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumService
    {
        Album ByID(int id);

        Album ByTitle(string albumTitle);

        Album CreateAlbum(string albumTitle, Color BgColor);
    }
}
