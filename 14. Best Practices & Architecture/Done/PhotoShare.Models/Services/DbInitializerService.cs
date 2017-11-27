using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services.Services
{
    public class DbInitializerService : IDbInitializer
    {
        private readonly PhotoShareContext context;

        public DbInitializerService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            this.context.Database.EnsureCreated();
        }
    }
}
