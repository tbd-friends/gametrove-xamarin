using System;
using System.Collections.Generic;
using Gametrove.Core.Services.Models;
using SQLite;

namespace Gametrove.Core.Infrastructure.Cache.Models
{
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
        public int CopiesOwned { get; set; }
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
                LastVisited = DateTime.UtcNow,
                CopiesOwned = model.CopiesOwned
            };
        }
    }
}