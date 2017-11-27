using PhotoShare.Models;
using System.Collections.Generic;

namespace PhotoShare.Services.Contracts
{
    public interface IUserService
    {
        User ByID(int id);

        User ByUsername(string username);

        User RegisterUser(string username, string password, string email);

        void DeleteUser(string username);

        List<User> ListFriends(User user);

        void ModifyUserPassword(User user, string password);

        void ModifyUserBornTown(User user, string bornTown);

        void ModifyUserCurrentTown(User user, string homeTown);
    }
}
