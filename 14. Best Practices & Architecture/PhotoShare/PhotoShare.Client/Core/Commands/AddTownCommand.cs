using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTownCommand : ICommand
    {
        private readonly ITownService townSrv;
        private readonly IUserSessionService userSsSrv;

        public AddTownCommand(ITownService townSrv, IUserSessionService userSsSrv)
        {
            this.townSrv = townSrv;
            this.userSsSrv = userSsSrv;
        }
        
        public string Execute(string[] data)
        {
            var townName = string.Empty;
            var country = string.Empty;
            try
            {
                townName = data[0];
                country = data[1];
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            if (!userSsSrv.IsLoggedOn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (townSrv.ByNameAndCountry(townName, country) != null)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            var newTown = townSrv.AddTown(townName, country);

            return $"Town {newTown.Name} was added successfully!";
        }
    }
}
