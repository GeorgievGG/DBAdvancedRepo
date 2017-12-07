using Instagraph.Data;
using Instagraph.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var objects = JArray.Parse(jsonString);

            var sb = new StringBuilder();
            var pics = new List<Picture>();
            foreach (var obj in objects)
            {
                var path = (string)obj.SelectToken("Path");
                var size = (string)obj.SelectToken("Size");

                if (pics.Any(p => p.Path == path))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                try
                {
                    var picture = new Picture()
                    {
                        Path = path,
                        Size = decimal.Parse(size, CultureInfo.GetCultureInfo("en-US"))
                    };
                    pics.Add(picture);
                    sb.AppendLine($"Successfully imported Picture {path}.");
                }
                catch (Exception)
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }
            context.Pictures.AddRange(pics);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var objects = JArray.Parse(jsonString);

            var sb = new StringBuilder();
            foreach (var obj in objects)
            {
                var username = (string)obj.SelectToken("Username");
                var password = (string)obj.SelectToken("Password");
                var picPath = (string)obj.SelectToken("ProfilePicture");
                var picture = context.Pictures.SingleOrDefault(u => u.Path == picPath);
                try
                {
                    var user = new User
                    {
                        Username = username,
                        Password = password,
                        ProfilePictureId = picture.Id
                    };

                    context.Add(user);
                    sb.AppendLine($"Successfully imported User {username}.");
                }
                catch (Exception)
                {
                    sb.AppendLine("Error: Invalid data.");
                };
            }

            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var objects = JArray.Parse(jsonString);

            var sb = new StringBuilder();
            var userFollowers = new List<UserFollower>();

            foreach (var obj in objects)
            {
                var userName = (string)obj.SelectToken("User");
                var followerName = (string)obj.SelectToken("Follower");

                try
                {
                    var user = context.Users.SingleOrDefault(u => u.Username == userName);
                    var follower = context.Users.SingleOrDefault(u => u.Username == followerName);

                    if (userFollowers.Any(x => x.UserId == user.Id && x.FollowerId == follower.Id))
                    {
                        sb.AppendLine("Error: Invalid data.");
                        continue;
                    }
                    var userFollower = new UserFollower()
                    {
                        UserId = user.Id,
                        FollowerId = follower.Id
                    };
                    userFollowers.Add(userFollower);
                    sb.AppendLine($"Successfully imported Follower {followerName} to User {userName}.");
                }
                catch (Exception)
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xmlDoc = XDocument.Parse(xmlString);
            var objects = xmlDoc.Root.Elements();

            var sb = new StringBuilder();
            var posts = new List<Post>();
            foreach (var obj in objects)
            {
                try
                {
                    var caption = obj.Element("caption").Value;
                    var userName = obj.Element("user").Value;
                    var picturePath = obj.Element("picture").Value;

                    var user = context.Users.SingleOrDefault(u => u.Username == userName);
                    var picture = context.Pictures.SingleOrDefault(p => p.Path == picturePath);

                    var post = new Post()
                    {
                        Caption = caption,
                        UserId = user.Id,
                        PictureId = picture.Id
                    };
                    posts.Add(post);
                    sb.AppendLine($"Successfully imported Post {caption}.");
                }
                catch (Exception)
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }
            context.Posts.AddRange(posts);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xmlDoc = XDocument.Parse(xmlString);
            var objects = xmlDoc.Root.Elements();

            var sb = new StringBuilder();
            var comments = new List<Comment>();
            foreach (var obj in objects)
            {
                try
                {
                    var content = obj.Element("content").Value;
                    var userName = obj.Element("user").Value;
                    var postId = int.Parse(obj.Element("post").Attribute("id").Value);

                    var user = context.Users.SingleOrDefault(u => u.Username == userName);
                    var post = context.Posts.SingleOrDefault(p => p.Id == postId);

                    var comment = new Comment()
                    {
                        Content = content,
                        UserId = user.Id,
                        PostId = post.Id
                    };
                    comments.Add(comment);
                    sb.AppendLine($"Successfully imported Comment {content}.");
                }
                catch (Exception)
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }
            context.Comments.AddRange(comments);
            context.SaveChanges();

            return sb.ToString();
        }
    }
}
