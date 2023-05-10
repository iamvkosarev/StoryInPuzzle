namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen
{
    public class LoginScreenProvider : AssetProvider<LoginScreen>, ILoginScreenProvider
    {
        protected override string AssetKey => AssetsKeys.LoginScreen;
    }
}