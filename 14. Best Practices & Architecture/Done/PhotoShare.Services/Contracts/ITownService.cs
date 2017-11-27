using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface ITownService
    {
        Town ByNameAndCountry(string name, string country)

        Town AddTown(string name, string country);
    }
}
