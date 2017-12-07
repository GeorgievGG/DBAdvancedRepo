using System;

namespace Instagraph.Models
{
    public class Comment
    {
        private string content;

        public int Id { get; set; }
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == string.Empty)
                {
                    throw new ArgumentException("Error: Invalid data.");
                }
                this.content = value;
            }
        }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}