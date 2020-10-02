using System;
using SQLite;

namespace Gametrove.Core.Infrastructure.Cache.Models
{
    public class TrackedGameModelImage
    {
        [PrimaryKey, AutoIncrement] public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Guid ImageId { get; set; }
        public string Url { get; set; }
        public bool IsCoverArt { get; set; }

        public virtual TrackedGameModel Game { get; set; }
    }
}