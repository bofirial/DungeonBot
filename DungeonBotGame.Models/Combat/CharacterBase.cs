using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Combat
{
    public abstract class CharacterBase : ICharacter
    {
        public CharacterBase(string characterName, int maximumHealth, IImmutableList<ActionModuleFileViewModel> sourceCodeFiles, IDictionary<AbilityType, AbilityContext> abilities)
        {
            Name = characterName;
            MaximumHealth = maximumHealth;
            SourceCodeFiles = sourceCodeFiles;
            CurrentHealth = maximumHealth;
            Id = Guid.NewGuid().ToString();
            Abilities = abilities;
        }

        public string Name { get; }

        public int CurrentHealth { get; set; }

        public int MaximumHealth { get; }

        public IImmutableList<ActionModuleFileViewModel> SourceCodeFiles { get; }

        public IDictionary<AbilityType, AbilityContext> Abilities { get; }

        public string Id { get; }
    }
}
