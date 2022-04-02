using System.Reflection;

namespace DungeonBotGame.Models.Combat
{
    public record ActionModuleContext(object ActionModuleObject, MethodInfo ActionModuleEntryPointMethodInfo);
}
