using System.Collections.Generic;
using DungeonBotGame.Models.Display;
using Fluxor;

namespace DungeonBotGame.Client.Store.Dungeons
{
    public class DungeonFeature : Feature<DungeonState>
    {
        public override string GetName() => nameof(DungeonState);

        protected override DungeonState GetInitialState()
        {
            return new DungeonState(new List<DungeonViewModel>()
            {
                new DungeonViewModel("Rat Infestation",
                    "There is one large rat that has taken over an elderly widow's dock in the harbor.  The widow would like to hire us to get rid of it.  She can't pay us much but she has promised all the apple pie we can eat if we can do this for her.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Big Rat", "It's a rat with no friends.", "/images/temp/big-rat.png")
                        },
                    "Available",
                    null
                ),

                 new DungeonViewModel("Rescue the Farmer's Sheep",
                    "A dragon whelp has stolen Farmer Lizzy's prized sheep Sweatersmith.  Sweatersmith has been entered into the regional fair this weekend.  Farmer Lizzy is willing to pay us half the prize money if we can rescue Sweatersmith in time.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Hungry Dragon Whelp", "He hasn't eaten.", "/images/temp/hungry-dragon.png")
                        },
                    "Available",
                    null
                ),

                new DungeonViewModel("Skeleton Cave",
                    "There are some wolves in Skeleton cave.  The cave was known to be home to mighty skeletons hundreds of years ago but adventurers cleared them out.  The wolves are just as dangerous though and need to be defeated before they start attacking the nearby livestock.  The city of Damselville has put a bounty on the Wolf King's head.",
                    new List<EncounterViewModel>()
                        {
                             new EncounterViewModel("Wolf King", "The wolf king waits inside the cave.", "/images/temp/wolf-king.png")
                        },
                    "Available",
                    null
                )
            });
        }
    }
}
