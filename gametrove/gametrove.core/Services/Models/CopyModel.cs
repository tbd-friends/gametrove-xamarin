using System;
using System.Collections.Generic;

namespace Gametrove.Core.Services.Models
{
    public class CopyModel
    {
        public Guid Id { get; set; }
        public decimal? Cost { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public DateTime? Purchased { get; set; }
    }
}