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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString);
        }
    }
}
