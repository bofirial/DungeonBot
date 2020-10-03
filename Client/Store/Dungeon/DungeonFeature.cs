using System.Collections.Generic;
using DungeonBot.Models.Display;
using Fluxor;

namespace DungeonBot.Client.Store.ActionModule
{
    public class DungeonFeature : Feature<DungeonState>
    {
        public override string GetName() => nameof(DungeonState);

        protected override DungeonState GetInitialState()
        {
            return new DungeonState(new List<Dungeon>()
            {
                new Dungeon()
                {
                    Name = "Rat Infestation",
                    Description = "There is one large rat that has taken over an elderly widow's dock in the harbor.  The widow would like to hire us to get rid of it.  She can't pay us much but she has promised all the apple pie we can eat if we can do this for her.",
                    Status = "Available",
                    Encounters = new List<Encounter>()
                    {
                         new Encounter()
                         {
                             Name = "Big Rat",
                             Description = "It's a rat with no friends.",
                             ProfileImageLocation = "/images/temp/big-rat.png"
                         }
                    }
                },

                new Dungeon()
                {
                    Name = "Rescue the Farmer's Sheep",
                    Description = "A dragon whelp has stolen Farmer Lizzy's prized sheep Sweatersmith.  Sweatersmith has been entered into the regional fair this weekend.  Farmer Lizzy is willing to pay us half the prize money if we can rescue Sweatersmith in time.",
                    Status = "Available",
                    Encounters = new List<Encounter>()
                    {
                         new Encounter()
                         {
                             Name = "Hungry Dragon Whelp",
                             Description = "He hasn't eaten.",
                             ProfileImageLocation = "/images/temp/hungry-dragon.png"
                         }
                    }
                },

                new Dungeon()
                {
                    Name = "Skeleton Cave",
                    Description = "There are some wolves in Skeleton cave.  The cave was known to be home to mighty skeletons hundreds of years ago but adventurers cleared them out.  The wolves are just as dangerous though and need to be defeated before they start attacking the nearby livestock.  The city of Damselville has put a bounty on the Wolf King's head.",
                    Status = "Available",
                    Encounters = new List<Encounter>()
                    {
                         new Encounter()
                         {
                             Name = "Wolf King",
                             Description = "The wolf king waits inside the cave.",
                             ProfileImageLocation = "/images/temp/wolf-king.png"
                         }
                    }
                }
            });
        }
    }
}
