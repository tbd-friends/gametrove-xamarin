using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure.Cache.Models;
using Gametrove.Core.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Gametrove.Core.Infrastructure.Cache
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
                                  CopiesOwned = g.CopiesOwned,
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

        public async Task UpdateImage(GameImage image)
        {
            using (var context = new CacheDataContext())
            {
                var current = context.Images.Single(i => i.Id == image.Id);

                current.IsCoverArt = image.IsCoverArt;

                await context.SaveChangesAsync();
            }
        }
    }
}