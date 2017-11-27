using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IFriendshipService
    {
        void AddFriend(User adding, User added);

        void AcceptFriend(User added, User adding);

        Friendship byUserIds(int userId, int friendId);
    }
}
