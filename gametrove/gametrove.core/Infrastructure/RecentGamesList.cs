using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using SQLite;

namespace Gametrove.Core.Infrastructure
{
    public class RecentGamesList
    {
        private static SQLiteAsyncConnection _connection;
        private static bool _initialized = false;

        public async Task<IEnumerable<GameModel>> Recent()
        {
            await InitializeAsync();

            return (from x in await _connection.QueryAsync<TrackedGameModel>("SELECT * FROM [TrackedGameModel] ORDER BY LastVisited DESC LIMIT 10")
                    select x.ToGameModel()).ToList();
        }

        public RecentGamesList()
        {
            _connection = _connection ?? new SQLiteAsyncConnection(
                              Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                  "gametrove.db3"),
                              SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (_connection.TableMappings.All(m => m.MappedType.Name != nameof(TrackedGameModel)))
                {
                    try
                    {
                        await _connection.CreateTablesAsync(
                                CreateFlags.AutoIncPK, typeof(TrackedGameModel))
                            .ConfigureAwait(false);
                    }
                    catch (Exception exc)
                    {
                        _initialized = false;
                    }

                    _initialized = true;
                }
            }
        }

        public async Task Track(GameModel game)
        {
            await InitializeAsync();

            var exists =
                (await _connection.QueryAsync<TrackedGameModel>(
                    "SELECT * FROM [TrackedGameModel] WHERE GameId = ?",
                    game.Id)).SingleOrDefault();

            if (exists != null)
            {
                exists.LastVisited = DateTime.UtcNow;

                await _connection.UpdateAsync(exists);
            }
            else
            {
                await _connection.InsertAsync(TrackedGameModel.From(game));
            }
        }
    }

    public class TrackedGameModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
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

        public GameModel ToGameModel()
        {
            return new GameModel
            {
                Id = GameId,
                Name = Name,
                Subtitle = Subtitle,
                IsFavorite = IsFavorite,
                Code = Code,
                CompleteInBoxPrice = CompleteInBoxPrice,
                LoosePrice = LoosePrice,
                Platform = Platform
            };
        }
    }
}