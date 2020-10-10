using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace statmathPostgreSample.Database
{
    public class Model : DbContext
    {
        public DbSet<Blog> blog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=postgreDB;Database=testdb;Username=test;Password=test");
    }



    public class Blog
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

    }
}
