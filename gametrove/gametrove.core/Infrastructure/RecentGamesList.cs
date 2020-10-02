using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using SQLite;
using ZXing;

namespace Gametrove.Core.Infrastructure
{
    public class RecentGamesList
    {
        public async Task<IEnumerable<GameModel>> Recent()
        {
            using (var context = new CacheDataContext())
            {
                var myContext = context;
                
                return await (from g in context.Games
                    orderby g.LastVisited descending
                    select new GameModel
                    {
                        Id = g.GameId,
                        Name = g.Name,
                        Subtitle = g.Subtitle,
                        IsFavorite = g.IsFavorite,
                        Code = g.Code,
                        CompleteInBoxPrice = g.CompleteInBoxPrice,
                        LoosePrice = g.LoosePrice,
                        Platform = g.Platform,
                        Images = (from i in myContext.Images
                            where i.GameId == g.GameId
                            select new GameImage
                            {
                                Id = i.Id,
                                Url = i.Url,
                                IsCoverArt = i.IsCoverArt
                            }).ToList()
                    }).ToListAsync();
            }
        }

        public async Task Track(GameModel game)
        {
            using (var context = new CacheDataContext())
            {
                var exists =
                    context.Games.SingleOrDefault(g => g.GameId == game.Id);

                if (exists != null)
                {
                    context.Remove(exists);

                    var gameImages = context.Images.Where(g => g.GameId == game.Id);

                    foreach (var image in gameImages)
                    {
                        context.Remove(image);
                    }
                }
                
                context.Add(TrackedGameModel.From(game));

                foreach (var image in game.Images)
                {
                    context.Add(new TrackedGameModelImage
                    {
                        Id = image.Id,
                        GameId = game.Id,
                        Url = image.Url,
                        IsCoverArt = image.IsCoverArt
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }

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

    public class TrackedGameModelImage
    {
        [PrimaryKey, AutoIncrement] public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Guid ImageId { get; set; }
        public string Url { get; set; }
        public bool IsCoverArt { get; set; }

        public virtual TrackedGameModel Game { get; set; }
    }

    public class TrackedGameModel
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Code { get; set; }
        public string Platform { get; set; }
        public DateTime Registered { get; set; }
        public bool IsFavorite { get; set; }
        public decimal? CompleteInBoxPrice { get; set; }
        public decimal? LoosePrice { get; set; }
        public DateTime LastVisited { get; set; }
        public virtual IEnumerable<TrackedGameModelImage> Images { get; set; }

        public static TrackedGameModel From(GameModel model)
        {
            return new TrackedGameModel
            {
                GameId = model.Id,
                Name = model.Name,
                Subtitle = model.Subtitle,
                IsFavorite = model.IsFavorite,
                Code = model.Code,
                CompleteInBoxPrice = model.CompleteInBoxPrice,
                LoosePrice = model.LoosePrice,
                Platform = model.Platform,
                LastVisited = DateTime.UtcNow
            };
        }
    }
}