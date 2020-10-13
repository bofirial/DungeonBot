namespace DungeonBotGame.Models.Display
{
    public class ActionModuleFile
    {
        public string FileName { get; }

        public string Content { get; }

        public ActionModuleFile(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
