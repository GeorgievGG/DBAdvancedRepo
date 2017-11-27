using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class DeleteUserCommand
    {
        private readonly IUserService userSrv;

        public DeleteUserCommand(IUserService userSrv)
        {
            this.userSrv = userSrv;
        }

        public string Execute(string[] data)
        {
            string username = data[1];
            if (userSrv.ByUsername(username) == null)
            {
                throw new InvalidOperationException($"User {username} not found!");
            }
            if (userSrv.ByUsername(username).IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            return $"User {username} was deleted successfully!";
        }
    }
}
