﻿using Microsoft.EntityFrameworkCore;
using statmathPostgreSample.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace statmathPostgreSample.Database
{
    public class MachineModel : DbContext
    {
        #region Connection
        private bool IsInDocker
        {
            get
            {
                if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("InDocker")))
                    return true;
                else
                    return false;
            }
        }

        public string ConnectionString
        {
            get
            {
                if (IsInDocker)
                    return "Server=host.docker.internal;Database=testdb;Username=test;Password=test";
                else
                    return "Server=localhost;Database=testdb;Username=test;Password=test";
            }
        }

        #endregion

        #region Entities
        public DbSet<Machine> machines { get; set; }
        public DbSet<MachineJob> machinejobs { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
