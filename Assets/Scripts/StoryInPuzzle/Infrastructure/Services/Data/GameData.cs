using System;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    [Serializable]
    public class GameData
    {
        public PlayerData PlayerData;

        public GameData()
        {
            PlayerData = new PlayerData();
        }
    }
}