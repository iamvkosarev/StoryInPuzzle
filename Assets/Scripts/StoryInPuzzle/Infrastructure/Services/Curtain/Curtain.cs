using System.Collections;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Curtain
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Curtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private float _hideDuration = 0.4f;

        private CanvasGroup _canvasGroup;
        private Coroutine _hideCoroutine;
        private static Curtain Instance;


        private CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup != null) return _canvasGroup;
                _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void Show()
        {
            if (_hideCoroutine != null) StopCoroutine(_hideCoroutine);
            CanvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _hideCoroutine = StartCoroutine(Hiding());
        }

        private IEnumerator Hiding()
        {
            var time = _hideDuration;
            while (time > 0)
            {
                time -= Time.deltaTime;
                CanvasGroup.alpha = time / _hideDuration;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}