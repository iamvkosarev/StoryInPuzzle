using System;
using System.Collections.Generic;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    [Serializable]
    public class PlayerLevelsSessions
    {
        public List<int> LevelsSessionsCount;
        public string PlayerNickName;

        public PlayerLevelsSessions(string playerNickName)
        {
            PlayerNickName = playerNickName;
            LevelsSessionsCount = new List<int>();
        }
    }
}