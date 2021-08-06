using Core.Models.Domain;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly DatabaseSettings _settings;

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions, DatabaseSettings settings) : base(dbContextOptions)
        {
            _settings = settings;
        }

        public DbSet<AwardsAtlantaPostFailures> AwardsAtlantaPostFailures { get; set; }
        public DbSet<AwardsAtlantaOrders> AwardsAtlantaOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.UseSqlServer(_settings.ConnectionString, s => s.CommandTimeout(60));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
