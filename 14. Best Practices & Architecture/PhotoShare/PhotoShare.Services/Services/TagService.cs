using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Tag ByName(string tag)
        {
            return context.Tags.SingleOrDefault(t => t.Name == tag);
        }

        public Tag AddTag(string tagName)
        {
            var tag = new Tag
            {
                Name = tagName
            };

            context.Tags.Add(tag);
            context.SaveChanges();

            return tag;
        }
    }
}
