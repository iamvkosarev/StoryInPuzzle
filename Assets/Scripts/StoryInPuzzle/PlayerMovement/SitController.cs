using Core.Common;
using UnityEngine;

namespace StoryInPuzzle.PlayerMovement
{
    public class SitController : MonoBehaviour
    {
        [SerializeField] private float sitHeight;

        private static PlayerComponent playerComponent => SavingDataProvider.PlayerComponent;

        private float standHeight;

        private bool sit;
        private bool tryStand;

        private void Start()
        {
            standHeight = playerComponent.transform.localScale.y;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!sit)
                {
                    sit = true;
                    tryStand = false;
                    playerComponent.transform.localScale = new Vector3(1f, sitHeight, 1f);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (sit && !tryStand)
                {
                    tryStand = true;
                }
            }

            if (tryStand && sit && CanStand())
            {
                tryStand = false;
                sit = false;
                playerComponent.transform.localScale = new Vector3(1f, standHeight, 1f);
            }
        }

        private bool CanStand()
        {
            var startPoint = playerComponent.transform.position + playerComponent.transform.up *
                (playerComponent.transform.localScale.y * playerComponent.CapsuleCollider.height) / 2f;
            var ratCast = Physics.Raycast(new Ray(startPoint,
                playerComponent.transform.up), out var hit);
            if (!ratCast)
                return true;
            return Vector3.Distance(hit.point, startPoint) >= playerComponent.CapsuleCollider.height / 2f;
        }
    }
}