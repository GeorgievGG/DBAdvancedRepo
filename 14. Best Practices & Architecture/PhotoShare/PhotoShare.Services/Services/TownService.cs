using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }
        public Town ByNameAndCountry(string name, string country)
        {
            return context.Towns.SingleOrDefault(t => t.Name == name && t.Country == country);
        }

        public Town AddTown(string name, string country)
        {
            Town town = new Town
            {
                Name = name,
                Country = country
            };

            context.Towns.Add(town);
            context.SaveChanges();

            return town;
        }
    }
}
