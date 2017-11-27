using PhotoShare.Client.Utilities;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand
    {
        private readonly ITagService tagSrv;

        public AddTagCommand(ITagService tagSrv)
        {
            this.tagSrv = tagSrv;
        }

        public string Execute(string[] data)
        {
            string tag = data[1].ValidateOrTransform();

            if (tagSrv.ByName(tag) != null)
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            tagSrv.AddTag(tag);

            return $"Tag {tag} was added successfully!";
        }
    }
}
