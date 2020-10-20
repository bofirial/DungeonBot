using System.Reflection;

namespace DungeonBotGame.Models.Combat
{
    public class ActionModuleContext
    {
        public object ActionModuleObject { get; set; }

        public MethodInfo ActionModuleEntryPointMethodInfo { get; set; }
    }
}
