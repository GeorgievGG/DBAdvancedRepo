using System;
using System.Collections.Generic;

namespace Instagraph.Models
{
    public class Post
    {
        private string caption;

        public int Id { get; set; }
        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == string.Empty)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.caption = value;
            }
        }
        public int UserId { get; set; }
        public int PictureId { get; set; }

        public User User { get; set; }
        public Picture Picture { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}