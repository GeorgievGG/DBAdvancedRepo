using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userSrv;
        private readonly IUserSessionService userSsSrv;

        public DeleteUserCommand(IUserService userSrv, IUserSessionService userSsSrv)
        {
            this.userSrv = userSrv;
            this.userSsSrv = userSsSrv;
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

            var user = userSrv.ByUsername(username);

            if (user != userSsSrv.User)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (user == null)
            {
                throw new InvalidOperationException($"User {username} not found!");
            }

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            userSrv.DeleteUser(username);

            return $"User {username} was deleted successfully!";
        }
    }
}
