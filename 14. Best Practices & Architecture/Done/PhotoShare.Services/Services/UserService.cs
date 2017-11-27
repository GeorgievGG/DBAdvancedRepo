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

        public void DeleteUser(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            
            user.IsDeleted = true;
            context.SaveChanges();
        }

        public User RegisterUser(string username, string password, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}
