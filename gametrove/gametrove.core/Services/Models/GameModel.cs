using System;
using System.Collections.Generic;

namespace Gametrove.Core.Services.Models
{
    public class GameSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Platform { get; set; }
    }
    
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Code { get; set; }
        public string Platform { get; set; }
        public DateTime Registered { get; set; }
        public bool IsFavorite { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public decimal? CompleteInBoxPrice { get; set; }
        public decimal? LoosePrice { get; set; }
    }
}