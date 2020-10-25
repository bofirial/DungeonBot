using System.Text;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public class ActionComponentAbilityExtensionMethodsClassBuilder : IActionComponentAbilityExtensionMethodsClassBuilder
    {
        private readonly IAbilityDescriptionProvider _abilityDescriptionProvider;

        public ActionComponentAbilityExtensionMethodsClassBuilder(IAbilityDescriptionProvider abilityDescriptionProvider)
        {
            _abilityDescriptionProvider = abilityDescriptionProvider;
        }

        public string BuildAbilityExtensionMethodsClass(DungeonBotViewModel dungeonBot)
        {
            var classStringBuilder = new StringBuilder();

            classStringBuilder.AppendLine("using DungeonBotGame;");
            classStringBuilder.AppendLine("using DungeonBotGame.Models.Combat;");
            classStringBuilder.AppendLine("namespace DungeonBotGame");
            classStringBuilder.AppendLine("{");
            classStringBuilder.AppendLine("    public static class ActionComponentExtensionMethods");
            classStringBuilder.AppendLine("    {");

            foreach (var ability in dungeonBot.Abilities)
            {
                if (_abilityDescriptionProvider.GetAbilityDescription(ability).IsTargettedAbility)
                {
                    classStringBuilder.AppendLine($"        public static ITargettedAbilityAction Use{ability}(this IActionComponent actionComponent, ITarget target) => ((ActionComponent)actionComponent).UseTargettedAbility(target, AbilityType.{ability});");
                }
                else
                {
                    classStringBuilder.AppendLine($"        public static IAbilityAction Use{ability}(this IActionComponent actionComponent) => ((ActionComponent)actionComponent).UseAbility(AbilityType.{ability});");
                }

                classStringBuilder.AppendLine($"        public static bool {ability}IsAvailable(this IActionComponent actionComponent) => ((ActionComponent)actionComponent).AbilityIsAvailable(AbilityType.{ability});");
            }

            classStringBuilder.AppendLine("    }");
            classStringBuilder.AppendLine("}");

            return classStringBuilder.ToString();
        }
    }
}
