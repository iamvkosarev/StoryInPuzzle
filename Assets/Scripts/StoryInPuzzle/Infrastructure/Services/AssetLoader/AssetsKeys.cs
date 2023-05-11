namespace StoryInPuzzle.Infrastructure.Services.AssetLoader
{
    public static class AssetsKeys
    {
        public static string LoginScreen => "LoginScreen";
        public static string SelectLevelScreen => "SelectLevelScreen";
        public static string PlayerGameScreen => "PlayerGameScreen";
        public static string RecordeScreen => "RecorderScreen";
        public static string HelpGameScreen => "HelpGameScreen";

        public static string LevelTaskScreen(int taskScreenIndex) => $"Level Task Screen {taskScreenIndex}";
    }
}