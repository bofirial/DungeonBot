using System;
using System.Collections.Immutable;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class AdventureFeature : Feature<AdventureState>
    {
        public override string GetName() => nameof(AdventureState);

        protected override AdventureState GetInitialState()
        {
            return new AdventureState(ImmutableList.Create(
                new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Rat Infestation",
                    "There is one large rat that has taken over an elderly widow's dock in the harbor.  The widow would like to hire us to get rid of it.  She can't pay us much but she has promised all the apple pie we can eat if we can do this for her.",
                    ImmutableList.Create(new EncounterViewModel("Big Rat", Order: 1, "It's a rat with no friends.", "/images/temp/big-rat.png", EnemyType.Rat)),
                    DungeonBotSlots: 1,
                    "Available",
                    null
                ),

                 new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Rescue the Farmer's Sheep",
                    "A dragon whelp has stolen Farmer Lizzy's prized sheep Sweatersmith.  Sweatersmith has been entered into the regional fair this weekend.  Farmer Lizzy is willing to pay us half the prize money if we can rescue Sweatersmith in time.",
                    ImmutableList.Create(new EncounterViewModel("Hungry Dragon Whelp", Order: 1, "He hasn't eaten.", "/images/temp/hungry-dragon.png", EnemyType.Dragon)),
                    DungeonBotSlots: 1,
                    "Available",
                    null
                ),

                new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Skeleton Cave",
                    "There are some wolves in Skeleton cave.  The cave was known to be home to mighty skeletons hundreds of years ago but adventurers cleared them out.  The wolves are just as dangerous though and need to be defeated before they start attacking the nearby livestock.  The city of Damselville has put a bounty on the Wolf King's head.",
                    ImmutableList.Create(new EncounterViewModel("Wolf King", Order: 1, "The wolf king waits inside the cave.", "/images/temp/wolf-king.png", EnemyType.Wolf)),
                    DungeonBotSlots: 1,
                    "Available",
                    null
                ),

                new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Troll Hill",
                    "Jack and Jill went up a hill and found a troll.  You must avenge them.  Also there is a pixie.",
                    ImmutableList.Create(
                        new EncounterViewModel("Junior Pixie", Order: 1, "The pixie is at the bottom of the hill.", "/images/temp/pixie.png", EnemyType.Pixie),
                        new EncounterViewModel("Troll", Order: 2, "The troll is on top of the hill pretending to be a king.", "/images/temp/troll.png", EnemyType.Troll)),
                    DungeonBotSlots: 1,
                    "Available",
                    null
                ),

                new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Batty Cave",
                    "The bats have gone batty.  We suspect rabies.  Clear out their cave.",
                    ImmutableList.Create(new EncounterViewModel("Bat Swarm", Order: 1, "The bats are just inside the cave.", "/images/temp/bats.png", EnemyType.Bat)),
                    DungeonBotSlots: 1,
                    "Available",
                    null
                ),
                new AdventureViewModel(Guid.NewGuid().ToString(),
                    "Elite Bear Cave Raid",
                    "Some bears have been attacking travelers on the main road out of town.  They appear to be staying in a cave near the river.",
                    ImmutableList.Create(new EncounterViewModel("Bears", Order: 1, "", "/images/temp/bear-family.png", EnemyType.EliteBearFamily)),
                    DungeonBotSlots: 2,
                    "Available",
                    null
                )
            ));
        }
    }
}
