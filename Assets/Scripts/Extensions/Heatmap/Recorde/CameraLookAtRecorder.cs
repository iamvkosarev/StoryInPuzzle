using Extentions.Heatmap.Data;
using UnityEngine;

namespace Extentions.Heatmap.Recorder
{
    public class CameraLookAtRecorder : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private string eventName = "cameraLookAt";
        [SerializeField] private float recordDuration = 0.2f;
        [SerializeField] private bool recorde;
        
        private float time;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                recorde = !recorde;
            }
            if(!recorde) return;
            if (time <= 0)
            {
                if (Recorde())
                {
                    time = recordDuration;
                }
            }
            else
            {
                time -= Time.deltaTime;
            }
        }

        private bool Recorde()
        {
            var centerOfScreen = new Vector3(0.5f, 0.5f, 0.5f);
            var ray = camera.ViewportPointToRay(centerOfScreen);

            if (!Physics.Raycast(ray, out var hit)) return false;
            HeatmapEventsContainer.Instance.AddRecord(eventName,
                ComposeJsonString(hit.point, hit.collider.gameObject.GetInstanceID()));
            return true;
        }

        private string ComposeJsonString(Vector3 point, int colliderId)
        {
            return JsonUtility.ToJson(new CameraLookAtData($"{point.x:0.00}",
                $"{point.y:0.00}", $"{point.z:0.00}", colliderId));
        }
    }
}