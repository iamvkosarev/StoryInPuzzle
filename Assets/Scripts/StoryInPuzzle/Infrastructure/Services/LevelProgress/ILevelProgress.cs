using System;
using System.Collections.Generic;
using StoryInPuzzle.FiddingObjects;

namespace StoryInPuzzle.Infrastructure.Services.LevelProgress
{
    public interface ILevelProgress : IService
    {
        void AddHiddenObject(HiddenObject hiddenObject);
        List<HiddenObject> StillHiddenObjectsList { get; }
        void SetCompleteAction(Action completeAction);
        void ClearLevelProgress();
        void SetChangeHiddenObjectViewAction(Action<bool> changeHiddenObjectViewAction);
        void ChangeHiddenObjectViewAction(bool p0);
        bool WasFounded(HiddenObject hiddenObject);
        void AddFoundedObject(HiddenObject hiddenObject);
    }
}