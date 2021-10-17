using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentOrExchange.WebApp.Areas.Identity.Data;

namespace RentOrExchange.WebApp.Data
{
    public class DBContext : IdentityDbContext<MyAppUser>
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<UserPost> UserPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPost>().HasKey(a => a.UserPostId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
