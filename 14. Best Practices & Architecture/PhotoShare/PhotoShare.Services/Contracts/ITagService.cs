using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface ITagService
    {
        Tag AddTag(string tag);
        Tag ByName(string tag);
    }
}
