using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Services.Services
{
    public class UserSessionService : IUserSessionService
    {
        public readonly IUserService userSrv;

        public UserSessionService(IUserService userSrv)
        {
            this.userSrv = userSrv;
        }

        public User User { get; private set; }

        public User Login(string username, string password)
        {
            var user = userSrv.ByUsername(username);

            if (user.Password != password)
            {
                user = null;
            }

            this.User = user;

            return user;
        }

        public void Logout()
        {
            this.User = null;
        }

        public bool IsLoggedOn()
        {
            return this.User != null;
        }
    }
}
