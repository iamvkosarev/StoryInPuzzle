namespace StoryInPuzzle.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}