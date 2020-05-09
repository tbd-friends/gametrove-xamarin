using System;

namespace Gametrove.Core.Services.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Platform { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}