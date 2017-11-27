using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserSessionService userSsSrv;

        public LoginCommand(IUserSessionService userSsSrv)
        {
            this.userSsSrv = userSsSrv;
        }

        public string Execute(string[] data)
        {
            var username = string.Empty;
            var password = string.Empty;
            try
            {
                username = data[0];
                password = data[1];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            //WTF?! - This is the requirement in the Problem description.
            if (userSsSrv.IsLoggedOn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            //This is another requirement.
            if (userSsSrv.IsLoggedOn())
            {
                throw new ArgumentException("You should logout first!");
            }

            var user = userSsSrv.Login(username, password);
            if (user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            return $"User {username} successfully logged in!";
        }
    }
}
