using System;

namespace Gametrove.Core.Services.Models
{
    public class GameImage
    {
        public string Url { get; set; }
        public Guid Id { get; set; }
        public bool IsCoverArt { get; set; }
    }
}