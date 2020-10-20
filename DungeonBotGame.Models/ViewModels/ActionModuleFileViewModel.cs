namespace DungeonBotGame.Models.ViewModels
{
    public class ActionModuleFileViewModel
    {
        public string FileName { get; }

        public string Content { get; }

        public ActionModuleFileViewModel(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
