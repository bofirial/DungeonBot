namespace DungeonBotGame.Models.ViewModels
{
    public record ActionModuleFileViewModel
    {
        public string FileName { get; init; }

        public string Content { get; init; }

        public ActionModuleFileViewModel(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
