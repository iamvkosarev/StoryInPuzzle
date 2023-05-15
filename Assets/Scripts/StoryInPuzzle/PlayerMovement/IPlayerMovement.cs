using Core.Common;
using StoryInPuzzle.Infrastructure.Services.PlayerInput;

namespace StoryInPuzzle.PlayerMovement
{
    public interface IPlayerMovement
    {
        void Init(IPlayerComponent playerComponent, IPlayerInput playerInput);
    }
}