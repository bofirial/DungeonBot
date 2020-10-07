namespace DungeonBot.Models.Combat
{
    public class ActionResult : IActionResult
    {
        public string DisplayText { get; set; }

        public IAction Action { get; set; }

        public ICharacter Character { get; set; }
    }
}
