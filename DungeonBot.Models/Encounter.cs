using System.Collections.Generic;

namespace DungeonBot.Models
{
    public class Encounter
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        //public string ThumbnailImage { get; set; }

        public IEnumerable<string> Counters { get; set; }
    }
}
