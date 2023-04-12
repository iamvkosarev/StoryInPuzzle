using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Settings
{
    public class ExitButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private void OnEnable()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(CloseGame);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(CloseGame);
        }

        private void CloseGame()
        {
            Application.Quit();
        }
    }
}