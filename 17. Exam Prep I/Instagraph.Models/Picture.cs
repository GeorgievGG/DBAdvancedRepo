using System;
using System.Collections.Generic;

namespace Instagraph.Models
{
    public class Picture
    {
        private string path;
        private decimal size;

        public int Id { get; set; }
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == string.Empty)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.path = value;
            }
        }
        public decimal Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.size = value;
            }
        }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
