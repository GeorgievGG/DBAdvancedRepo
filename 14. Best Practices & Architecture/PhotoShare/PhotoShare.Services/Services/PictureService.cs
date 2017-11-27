using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Models;
using PhotoShare.Data;
using System.Linq;

namespace PhotoShare.Services.Services
{
    public class PictureService : IPictureService
    {
        private readonly PhotoShareContext context;

        public PictureService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Picture ById(int id)
        {
            return context.Pictures.SingleOrDefault(p => p.Id == id);
        }

        public Picture ByTitle(string title)
        {
            return context.Pictures.SingleOrDefault(p => p.Title == title);
        }

        public Picture CreatePicture(Album album, string title, string path)
        {
            var picture = new Picture()
            {
                Album = album,
                Title = title,
                Path = path
            };

            context.Pictures.Add(picture);

            context.SaveChanges();

            return picture;
        }
    }
}
