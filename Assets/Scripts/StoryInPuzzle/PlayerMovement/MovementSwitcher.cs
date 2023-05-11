using Core.Common;
using Extentions;

namespace StoryInPuzzle.PlayerMovement
{
    public class MovementSwitcher : Singleton<MovementSwitcher>
    {
        private static PlayerComponent playerComponent => SavingDataProvider.PlayerComponent;
        public bool IsMoving { private set; get; } = true;

        public void SwitchMovement(bool mode)
        {
            IsMoving = mode;
            playerComponent.Rigidbody.isKinematic = !mode;
        }
    }
}