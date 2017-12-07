using System;
using System.Collections.Generic;

namespace Instagraph.Models
{
    public class User
    {
        private string username;
        private string password;

        public int Id { get; set; }
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == string.Empty)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.username = value;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == string.Empty)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.password = value;
            }
        }
        public int ProfilePictureId { get; set; }

        public Picture ProfilePicture { get; set; }
        public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
        public ICollection<UserFollower> UsersFollowing { get; set; } = new List<UserFollower>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}