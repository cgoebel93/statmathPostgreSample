using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace statmathPostgreSample.Database
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=testdb;Username=testuser;Password=testpwd");
    }

    public class Blog
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

    }
}
