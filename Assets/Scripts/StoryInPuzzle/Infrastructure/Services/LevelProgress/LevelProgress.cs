using System;
using System.Collections.Generic;
using System.Linq;
using StoryInPuzzle.FiddingObjects;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.LevelProgress
{
    [CreateAssetMenu(menuName = "StoryInPuzzle/LevelProgress")]
    public class LevelProgress : ScriptableObject, ILevelProgress
    {
        private Action _completeAction;
        private Action<bool> _changeHiddenObjectViewAction;
        private List<HiddenObject> FoundedObjectsList { get; } = new();
        private List<HiddenObject> HiddenObjectsList { get; } = new();

        public void SetCompleteAction(Action completeAction)
        {
            _completeAction = completeAction;
        }

        public void ClearLevelProgress()
        {
            FoundedObjectsList.Clear();
            HiddenObjectsList.Clear();
        }

        public void ChangeHiddenObjectViewAction(bool canSee)
        {
            _changeHiddenObjectViewAction?.Invoke(canSee);
        }
        public void SetChangeHiddenObjectViewAction(Action<bool> changeHiddenObjectViewAction)
        {
            _changeHiddenObjectViewAction = changeHiddenObjectViewAction;
        }

        public void AddHiddenObject(HiddenObject hiddenObject)
        {
            HiddenObjectsList.Add(hiddenObject);
        }

        public List<HiddenObject> StillHiddenObjectsList => HiddenObjectsList.Where(hiddenObject => !FoundedObjectsList.Contains(hiddenObject)).ToList();

        public void AddFoundedObject(HiddenObject hiddenObject)
        {
            FoundedObjectsList.Add(hiddenObject);
            if(FoundedObjectsList.Count == HiddenObjectsList.Count)
                _completeAction?.Invoke();
        }

        public bool WasFounded(HiddenObject hiddenObject) => FoundedObjectsList.Contains(hiddenObject);
    }
}