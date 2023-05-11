using StoryInPuzzle.Infrastructure.Services.LevelProgress;
using UnityEngine;

namespace StoryInPuzzle.FiddingObjects
{
    public class ObjectHunter : MonoBehaviour, IObjectHunter
    {
        [SerializeField] private float _maxDistance = 4f;
        [SerializeField] private LayerMask _notPlayerLayer;
        private ILevelProgress _levelProgress;
        private Camera _camera;

        public void SetLevelProgress(ILevelProgress levelProgress)
        {
            _levelProgress = levelProgress;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_levelProgress == null) return;
            if (CheckHiddenObject(out var hiddenObject))
            {
                _levelProgress.ChangeHiddenObjectViewAction(true);
                if (!_levelProgress.WasFounded(hiddenObject) && Input.GetKeyDown(KeyCode.E))
                {
                    hiddenObject.gameObject.SetActive(false);
                    _levelProgress.AddFoundedObject(hiddenObject);
                }
            }
            else
            {
                _levelProgress.ChangeHiddenObjectViewAction(false);
            }
        }

        private bool CheckHiddenObject(out HiddenObject hiddenObject)
        {
            hiddenObject = null;
            var ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (!Physics.Raycast(ray, out var hit, _maxDistance, _notPlayerLayer))
            {
                return false;
            }

            var hitObject = hit.collider.gameObject;
            hiddenObject = hitObject.GetComponent<HiddenObject>();

            return hiddenObject != null;
        }

        private void OnDrawGizmos()
        {
            if (_camera != null)
            {
                var ray = new Ray(_camera.transform.position, _camera.transform.forward);
                Gizmos.DrawRay(ray);
            }
        }
    }
}