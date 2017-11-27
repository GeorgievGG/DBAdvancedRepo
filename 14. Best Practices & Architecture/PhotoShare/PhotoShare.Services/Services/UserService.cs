using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
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

        public List<User> ListFriends(User user)
        {
            var friends = user
                            .FriendsAdded
                            .Select(fa => fa.Friend)
                            .OrderBy(x => x.FirstName)
                            .ThenBy(x => x.LastName)
                            .ToList();

            return friends;
        }

        public void ModifyUserPassword(User user, string password)
        {
            user.Password = password;
            var containsDigit = false;
            var containsSmallCase = false;

            foreach (var ch in password)
            {
                if (ch >= 97 && ch <= 122)
                {
                    containsSmallCase = true;
                }
                if (ch >= 48 && ch <= 57)
                {
                    containsDigit = true;
                }
            }
            if (!containsDigit || !containsSmallCase)
            {
                throw new ArgumentException("Invalid Password");
            }

            context.SaveChanges();
        }

        public void ModifyUserBornTown(User user, string bornTown)
        {
            var town = context.Towns.SingleOrDefault(t => t.Name == bornTown);

            if (town == null)
            {
                throw new ArgumentException($"Town {bornTown} not found!");
            }

            user.BornTown = town;

            context.SaveChanges();
        }

        public void ModifyUserCurrentTown(User user, string homeTown)
        {
            var town = context.Towns.SingleOrDefault(t => t.Name == homeTown);

            if (town == null)
            {
                throw new ArgumentException($"Town {homeTown} not found!");
            }

            user.CurrentTown = town;

            context.SaveChanges();
        }
    }
}
