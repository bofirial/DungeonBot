using System.Collections.Generic;
using DungeonBotGame.Models.ViewModels;
using Fluxor;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class AdventureFeature : Feature<AdventureState>
    {
        public override string GetName() => nameof(AdventureState);

        protected override AdventureState GetInitialState()
        {
            return new AdventureState(new List<AdventureViewModel>()
            {
                new AdventureViewModel("Rat Infestation",
                    "There is one large rat that has taken over an elderly widow's dock in the harbor.  The widow would like to hire us to get rid of it.  She can't pay us much but she has promised all the apple pie we can eat if we can do this for her.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Big Rat", 1, "It's a rat with no friends.", "/images/temp/big-rat.png", EnemyType.Rat)
                        },
                    "Available",
                    null
                ),

                 new AdventureViewModel("Rescue the Farmer's Sheep",
                    "A dragon whelp has stolen Farmer Lizzy's prized sheep Sweatersmith.  Sweatersmith has been entered into the regional fair this weekend.  Farmer Lizzy is willing to pay us half the prize money if we can rescue Sweatersmith in time.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Hungry Dragon Whelp", 1, "He hasn't eaten.", "/images/temp/hungry-dragon.png", EnemyType.Dragon)
                        },
                    "Available",
                    null
                ),

                new AdventureViewModel("Skeleton Cave",
                    "There are some wolves in Skeleton cave.  The cave was known to be home to mighty skeletons hundreds of years ago but adventurers cleared them out.  The wolves are just as dangerous though and need to be defeated before they start attacking the nearby livestock.  The city of Damselville has put a bounty on the Wolf King's head.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Wolf King", 1, "The wolf king waits inside the cave.", "/images/temp/wolf-king.png", EnemyType.Wolf)
                        },
                    "Available",
                    null
                )
            });
        }
    }
}
