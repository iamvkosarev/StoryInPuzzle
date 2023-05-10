namespace StoryInPuzzle.Infrastructure
{
    public interface IExitState
    {
        void Exit();
    }

    public interface IState : IExitState
    {
        void Enter();
    }

    public interface IPayloadState<T> : IExitState
    {
        void Enter(T levelIndex);
    }
}