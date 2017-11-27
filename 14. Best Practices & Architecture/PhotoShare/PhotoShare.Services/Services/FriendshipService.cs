using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly PhotoShareContext context;

        public FriendshipService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void AcceptFriend(User added, User adding)
        {
            var friendship = new Friendship()
            {
                User = added,
                Friend = adding
            };

            context.Friendships.Add(friendship);
            context.SaveChanges();
        }

        public void AddFriend(User adding, User added)
        {
            var friendship = new Friendship()
            {
                User = adding,
                Friend = added
            };

            context.Friendships.Add(friendship);
            context.SaveChanges();
        }

        public Friendship byUserIds(int userId, int friendId)
        {
            var fr = context.Friendships
                        .SingleOrDefault(f => f.UserId == userId && f.FriendId == friendId);
            return fr;
        }
    }
}
