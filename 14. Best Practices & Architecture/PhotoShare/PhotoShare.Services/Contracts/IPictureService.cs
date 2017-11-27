using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IPictureService
    {
        Picture ById(int id);

        Picture ByTitle(string title);

        Picture CreatePicture(Album album, string title, string path);
    }
}
