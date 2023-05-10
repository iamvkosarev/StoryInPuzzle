using System.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    public class SaveLoadData : ISaveLoadData
    {
        private const string GameDataKey = "Game Data";
        private readonly IGameDataContainer _dataContainer;

        public SaveLoadData(IGameDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }

        public async Task Load()
        {
            var dataString = PlayerPrefs.GetString(GameDataKey, "");
            _dataContainer.GameData = dataString.IsNullOrWhitespace() ? new GameData() : JsonUtility.FromJson<GameData>(dataString);
        }

        public async Task Save()
        {
            PlayerPrefs.SetString(GameDataKey, JsonUtility.ToJson(_dataContainer.GameData));
        }
    }
}