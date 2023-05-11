using StoryInPuzzle.Infrastructure.Services.PlayerInput;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public interface IPlayerInputUser 
    {
        void Init(IPlayerInput playerInput);
    }
}