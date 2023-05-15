using System;
using TMPro;
using UnityEngine;

namespace StoryInPuzzle
{
    public class FPSChecker : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        public float deltaTime;

        private static FPSChecker Instance;

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

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            var fps = 1.0f / deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
        }
    }
}