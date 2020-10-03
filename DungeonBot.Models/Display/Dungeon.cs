using System.Collections.Generic;

namespace DungeonBot.Models.Display
{
    public class Dungeon
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Encounter> Encounters { get; set; }

        public string Status { get; set; }
    }
}
