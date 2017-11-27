using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IUserSessionService userSsSrv;

        public LogoutCommand(IUserSessionService userSsSrv)
        {
            this.userSsSrv = userSsSrv;
        }

        public string Execute(string[] data)
        {
            var username = userSsSrv.User.Username;

            if (!userSsSrv.IsLoggedOn())
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }

            userSsSrv.Logout();

            return $"User {username} successfully logged out!";
        }
    }
}
