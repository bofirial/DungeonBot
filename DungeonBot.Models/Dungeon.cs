using System.Collections.Generic;

namespace DungeonBot.Models
{
    public class Dungeon
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public IEnumerable<Encounter> Encounters { get; set; }

        //public string ThumbnailImage { get; set; }

        public int AvailableDungeonBots { get; set; }

        //public IEnumerable<DungeonBot> DungeonBots { get; set; }

        //public IEnumerable<Attempt> AttemptHistory { get; set; }

        public string Status { get; set; }
    }
}
