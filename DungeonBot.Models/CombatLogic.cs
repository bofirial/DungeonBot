using System.Collections.Generic;

namespace DungeonBot.Models
{
    public class CombatLogic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Serialization")]
        public List<SourceCodeFile> SourceCodeFiles { get; set; } = new List<SourceCodeFile>();
    }
}
