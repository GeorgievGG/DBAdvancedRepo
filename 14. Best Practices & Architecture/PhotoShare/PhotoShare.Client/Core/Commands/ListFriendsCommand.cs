using PhotoShare.Client.Core.Contracts;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class ListFriendsCommand : ICommand
    {
        private readonly IUserService userSrv;

        public ListFriendsCommand(IUserService userSrv)
        {
            this.userSrv = userSrv;
        }

        public string Execute(string[] data)
        {
            var username = string.Empty;
            try
            {
                username = data[0];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            var sb = new StringBuilder();

            var user = userSrv.ByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            List<User> friends = userSrv.ListFriends(user);

            if (friends.Count == 0)
            {
                sb.Append("No friends for this user. :(");
            }
            else
            {
                sb.AppendLine("Friends:");
                foreach (var friend in friends)
                {
                    sb.AppendLine($"-{friend.Username}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
