namespace StoryInPuzzle.Infrastructure.Services.PlayerInput
{
    public interface IPlayerInput : IService
    {
        void Switch(bool mode);
        float Horizontal { get;}
        float Vertical { get;}
        float MouseY { get;}
        float MouseX { get;}
        bool GetKeySitDown { get;}
        bool GetKeySitUp { get;}
    }
}