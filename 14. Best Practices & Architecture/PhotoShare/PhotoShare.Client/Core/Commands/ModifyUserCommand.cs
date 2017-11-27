using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using PhotoShare.Services.Services;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userSrv;
        private readonly ITownService townSrv;
        private readonly IUserSessionService userSsSrv;

        public ModifyUserCommand(IUserService userSrv, ITownService townSrv, IUserSessionService userSsSrv)
        {
            this.userSrv = userSrv;
            this.townSrv = townSrv;
            this.userSsSrv = userSsSrv;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var username = string.Empty;
            var property = string.Empty;
            var newValue = string.Empty;
            try
            {
                username = data[0];
                property = data[1];
                newValue = data[2];
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
                throw new ArgumentException($"User {username} not found!");
            }

            var UserType = typeof(UserService);

            var methodName = $"ModifyUser{property}";

            var reqMethod = UserType.GetMethods().SingleOrDefault(p => p.Name == methodName);

            if (reqMethod == null)
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            try
            {
                reqMethod.Invoke(userSrv, new object[] { user, newValue });
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"Value {newValue} not valid.e{Environment.NewLine}{e.Message}");
            }

            return $"User {username} {property} is {newValue}.";
        }
    }
}
