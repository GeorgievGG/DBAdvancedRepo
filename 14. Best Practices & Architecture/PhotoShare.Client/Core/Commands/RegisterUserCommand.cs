namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Services.Contracts;
    using System;

    public class RegisterUserCommand
    {
        private IUserService userSrv;

        public RegisterUserCommand(IUserService userSrv)
        {
            this.userSrv = userSrv;
        }
        
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (password == repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            if (userSrv.ByUsername(username) != null)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            userSrv.RegisterUser(username, password, email);

            return $"User {username} was registered successfully!";
        }
    }
}
