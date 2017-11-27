using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userSrv;
        private readonly IFriendshipService frshpSrv;
        private readonly IUserSessionService userSsSrv;

        public AddFriendCommand(IUserService userSrv, IFriendshipService frshpSrv, IUserSessionService userSsSrv)
        {
            this.userSrv = userSrv;
            this.frshpSrv = frshpSrv;
            this.userSsSrv = userSsSrv;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var addingUN = string.Empty;
            var addedUN = string.Empty;
            try
            {
                addingUN = data[0];
                addedUN = data[1];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var adding = userSrv.ByUsername(addingUN);

            if (adding != userSsSrv.User)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (adding == null)
            {
                throw new ArgumentException($"{addingUN} not found!");
            }

            var added = userSrv.ByUsername(addedUN);

            if (added == null)
            {
                throw new ArgumentException($"{addedUN} not found!");
            }

            var frshp = frshpSrv.byUserIds(adding.Id, added.Id);

            if (frshp != null)
            {
                throw new ArgumentException($"{addedUN} is already a friend to {addingUN}");
            }

            frshpSrv.AddFriend(adding, added);

            return $"Friend {addedUN} added to {addingUN}";
        }
    }
}
