using Instagraph.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var reqPosts = context.Posts
                                .Where(p => p.Comments.Count == 0)
                                .Select(p => new
                                {
                                    p.Id,
                                    Picture = p.Picture.Path,
                                    User = p.User.Username
                                })
                                .OrderBy(p => p.Id);
            var jsonText = JsonConvert.SerializeObject(reqPosts, Formatting.Indented);

            return jsonText;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var reqUsers = context.Users
                                .Include(u => u.Followers)
                                .Include(u => u.Posts)
                                .Where(u => u.Followers.Any(f => f.Follower.Comments.Any(c => c.Post.UserId == u.Id)))
                                .OrderBy(u => u.Id)
                                .Select(u => new
                                {
                                    Username = u.Username,
                                    Followers = u.Followers.Count
                                });
            var jsonText = JsonConvert.SerializeObject(reqUsers, Formatting.Indented);

            return jsonText;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var reqUsers = context.Users
                                .Select(u => new
                                {
                                    Username = u.Username,
                                    MostComments = u.Posts.Count == 0 ? 
                                                0 :
                                                u.Posts.OrderByDescending(p => p.Comments.Count).FirstOrDefault().Comments.Count
                                })
                                .OrderByDescending(ru => ru.MostComments)
                                .ThenBy(ru => ru.Username);

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));
            foreach (var user in reqUsers)
            {
                var userElement = new XElement("user");
                userElement.Add(new XElement("Username", user.Username));
                userElement.Add(new XElement("MostComments", user.MostComments));
                xDoc.Root.Add(userElement);
            }

            return xDoc.ToString();
        }
    }
}
