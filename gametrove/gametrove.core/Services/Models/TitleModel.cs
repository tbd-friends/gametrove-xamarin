using System;
using System.Collections;
using System.Collections.Generic;

namespace Gametrove.Core.Services.Models
{
    public class TitleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public IEnumerable<string> Genres { get; set; }
    }
}