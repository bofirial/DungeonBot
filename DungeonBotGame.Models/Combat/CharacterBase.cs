﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Models.Combat
{
    public abstract class CharacterBase : ICharacter
    {
        public CharacterBase(
            string characterName,
            short level,
            short power,
            short armor,
            short speed,
            IImmutableList<ActionModuleFileViewModel> sourceCodeFiles,
            IDictionary<AbilityType, AbilityContext> abilities,
            IList<CombatEffect> combatEffects)
        {
            Name = characterName;
            Level = level;
            Power = power;
            Armor = armor;
            Speed = speed;
            SourceCodeFiles = sourceCodeFiles;
            Id = Guid.NewGuid().ToString();
            Abilities = abilities;
            CombatEffects = combatEffects;
        }

        public string Id { get; }

        public string Name { get; }
        public short Level { get; }
        public short Power { get; }
        public short Armor { get; }
        public short Speed { get; }
        public IImmutableList<ActionModuleFileViewModel> SourceCodeFiles { get; }

        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public IDictionary<AbilityType, AbilityContext> Abilities { get; }
        public IList<CombatEffect> CombatEffects { get; }
    }
}
