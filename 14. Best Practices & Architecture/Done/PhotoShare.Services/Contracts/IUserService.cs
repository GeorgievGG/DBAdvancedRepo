using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IUserService
    {
        User ByID(int id);

        User ByUsername(string username);

        User RegisterUser(string username, string password, string email);

        void DeleteUser(string username);
    }
}
