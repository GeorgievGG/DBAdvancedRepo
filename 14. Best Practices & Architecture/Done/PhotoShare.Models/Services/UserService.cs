using PhotoShare.Services.Contracts;
using System;
using PhotoShare.Models;
using PhotoShare.Data;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public User ByID(int id)
        {
            return context.Users
                .SingleOrDefault(u => u.Id == id);
        }

        public User ByUsername(string username)
        {
            return context.Users
               .SingleOrDefault(u => u.Username == username);
        }
    }
}
