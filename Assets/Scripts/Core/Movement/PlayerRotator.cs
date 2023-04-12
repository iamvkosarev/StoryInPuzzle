using Core.Common;
using UnityEngine;

namespace Core.Movement
{
    public class PlayerRotator : MonoBehaviour
    {
        private static PlayerComponent playerComponent => SavingDataProvider.PlayerComponent;
        [SerializeField] private float xSensitivity = 3f;
        [SerializeField] private float ySensitivity = 3f;


        private bool isMoving = true;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) isMoving = !isMoving;
            if(!isMoving) return;
            playerComponent.Rigidbody.MoveRotation(playerComponent.Rigidbody.rotation *
                                                   Quaternion.Euler(new Vector3(0,
                                                       Input.GetAxis("Mouse X") * xSensitivity, 0)));
            playerComponent.Camera.transform.eulerAngles -= new Vector3(
                Input.GetAxis("Mouse Y") * ySensitivity, 0f, 0f);
        }
    }
}