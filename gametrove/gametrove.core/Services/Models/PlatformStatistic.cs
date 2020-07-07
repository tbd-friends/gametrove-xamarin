using System;

namespace Gametrove.Core.Services.Models
{
    public class PlatformStatistic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
    }
}