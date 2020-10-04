using System.Collections.Generic;
using System.Linq;

namespace DungeonBot.Models.Display
{
    public class Dungeon
    {
        public string Name { get; }

        public string Description { get; }

        public IReadOnlyCollection<Encounter> Encounters { get; }

        public string Status { get; }

        public IReadOnlyCollection<DungeonResult> DungeonResults { get; }

        public Dungeon(string name, string description, IEnumerable<Encounter> encounters, string status, IEnumerable<DungeonResult> dungeonResults)
        {
            Name = name;
            Description = description;
            Encounters = encounters.ToList().AsReadOnly();
            Status = status;
            DungeonResults = dungeonResults?.ToList().AsReadOnly();
        }
    }
}
