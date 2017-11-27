using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IColorService
    {
        Color ById(int id);
        Color ByColorString(string color);
    }
}
