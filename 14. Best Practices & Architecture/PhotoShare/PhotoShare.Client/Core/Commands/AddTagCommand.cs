using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Utilities;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand : ICommand
    {
        private readonly ITagService tagSrv;
        private readonly IUserSessionService userSsSrv;

        public AddTagCommand(ITagService tagSrv, IUserSessionService userSsSrv)
        {
            this.tagSrv = tagSrv;
            this.userSsSrv = userSsSrv;
        }

        public string Execute(string[] data)
        {
            var tag = string.Empty;
            try
            {
                tag = data[0].ValidateOrTransform();
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid command!");
            }

            if (!userSsSrv.IsLoggedOn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (tagSrv.ByName(tag) != null)
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            tagSrv.AddTag(tag);

            return $"Tag {tag} was added successfully!";
        }
    }
}
