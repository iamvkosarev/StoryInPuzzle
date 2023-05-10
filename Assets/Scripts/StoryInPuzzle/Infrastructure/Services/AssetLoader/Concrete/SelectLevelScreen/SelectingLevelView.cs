using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.SelectLevelScreen
{
    public class SelectingLevelView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _setLevelTextFrom;
        private Action<SelectingLevelView> _activatingAction;
        public int LevelNumber { get; private set; }


        public void SetLevel(int levelNumber)
        {
            _setLevelTextFrom.text = levelNumber.ToString();
            LevelNumber = levelNumber;
        }

        public void SetActivatingAction(Action<SelectingLevelView> activatingAction)
        {
            _activatingAction = activatingAction;
        }

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(ActivateAction);
        }

        private void ActivateAction()
        {
            _activatingAction?.Invoke(this);
        }

        private void OnDisable()
        {
            {
                _selectButton.onClick.RemoveListener(ActivateAction);
            }
        }
    }
}