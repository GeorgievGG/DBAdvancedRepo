using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userSrv;
        private readonly IFriendshipService frshpSrv;

        public AcceptFriendCommand(IUserService userSrv, IFriendshipService frshpSrv)
        {
            this.userSrv = userSrv;
            this.frshpSrv = frshpSrv;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var addedUN = string.Empty;
            var addingUN = string.Empty;
            try
            {
                addedUN = data[0];
                addingUN = data[1];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var added = userSrv.ByUsername(addedUN);

            if (added == null)
            {
                throw new ArgumentException($"{addedUN} not found!");
            }

            var adding = userSrv.ByUsername(addingUN);

            if (adding == null)
            {
                throw new ArgumentException($"{addingUN} not found!");
            }

            var frshp = frshpSrv.byUserIds(added.Id, adding.Id);

            if (frshp != null)
            {
                throw new ArgumentException($"{addingUN} is already a friend to {addedUN}");
            }

            var frshpReq = frshpSrv.byUserIds(adding.Id, added.Id);

            if (frshp != null)
            {
                throw new ArgumentException($"{addingUN} has not added {addedUN} as a friend");
            }

            frshpSrv.AcceptFriend(added, adding);

            return $"{addedUN} accepted {addingUN} as a friend";
        }
    }
}
