using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userSrv;
        private readonly IUserSessionService userSsSrv;

        public RegisterUserCommand(IUserService userSrv, IUserSessionService userSsSrv)
        {
            this.userSrv = userSrv;
            this.userSsSrv = userSsSrv;
        }
        
        public string Execute(string[] data)
        {
            var username = string.Empty;
            var password = string.Empty;
            var repeatPassword = string.Empty;
            var email = string.Empty;
            try
            {
                username = data[0];
                password = data[1];
                repeatPassword = data[2];
                email = data[3];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            var user = userSrv.ByUsername(username);

            if (userSsSrv.IsLoggedOn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (user != null)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            userSrv.RegisterUser(username, password, email);

            return $"User {username} was registered successfully!";
        }
    }
}
