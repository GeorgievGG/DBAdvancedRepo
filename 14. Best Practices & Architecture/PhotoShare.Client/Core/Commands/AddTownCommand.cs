using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTownCommand
    {
        private readonly ITownService townSrv;

        public AddTownCommand(ITownService townSrv)
        {
            this.townSrv = townSrv;
        }
        
        public string Execute(string[] data)
        {
            string townName = data[1];
            string country = data[0];

            if (townSrv.ByNameAndCountry(townName, country) != null)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            var newTown = townSrv.AddTown(townName, country);

            return $"Town {newTown.Name} was added successfully!";
        }
    }
}
