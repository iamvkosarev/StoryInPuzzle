using System;
using System.Collections.Generic;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    [Serializable]
    public class GameData
    {
        public PlayerData PlayerData;
        public List<PlayerLevelsSessions> PlayersLevelSessions;

        public GameData()
        {
            PlayerData = new PlayerData();
            PlayersLevelSessions = new List<PlayerLevelsSessions>();
        }
    }
}