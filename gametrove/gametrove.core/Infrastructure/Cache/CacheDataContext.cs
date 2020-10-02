using System;
using System.IO;
using Gametrove.Core.Infrastructure.Cache.Models;
using Microsoft.EntityFrameworkCore;

namespace Gametrove.Core.Infrastructure.Cache
{
    public class CacheDataContext : DbContext
    {
        public DbSet<TrackedGameModel> Games { get; set; }
        public DbSet<TrackedGameModelImage> Images { get; set; }

        public CacheDataContext()
        {
            SQLitePCL.Batteries_V2.Init();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "gametrove.db3");

            optionsBuilder.UseSqlite($"filename={dbPath}");
        }
    }
}