using UnityEngine;

namespace StoryInPuzzle.Infrastructure.Services.Data
{
    public static class GameDataExtension
    {
        public static int GetLevelSessionNumber(this GameData gameData, int levelIndex)
        {
            PlayerLevelsSessions playerLevelsSession = null;
            foreach (var playersLevelSession in gameData.PlayersLevelSessions)
            {
                if (playersLevelSession.PlayerNickName == gameData.PlayerData.NickName)
                {
                    playerLevelsSession = playersLevelSession;
                    break;
                }
            }

            if (playerLevelsSession == null)
            {
                playerLevelsSession = new PlayerLevelsSessions(gameData.PlayerData.NickName);
                gameData.PlayersLevelSessions.Add(playerLevelsSession);
            }

            if (playerLevelsSession.LevelsSessionsCount.Count <= levelIndex)
            {
                playerLevelsSession.LevelsSessionsCount.Add(1);
                return 1;
            }

            return playerLevelsSession.LevelsSessionsCount[levelIndex];
        }

        public static void AddLevelSessionNumber(this GameData gameData, int levelIndex)
        {
            PlayerLevelsSessions playerLevelsSession = null;
            foreach (var playersLevelSession in gameData.PlayersLevelSessions)
            {
                if (playersLevelSession.PlayerNickName == gameData.PlayerData.NickName)
                {
                    playerLevelsSession = playersLevelSession;
                    break;
                }
            }

            if (playerLevelsSession == null)
            {
                playerLevelsSession = new PlayerLevelsSessions(gameData.PlayerData.NickName);
                gameData.PlayersLevelSessions.Add(playerLevelsSession);
            }

            if (playerLevelsSession.LevelsSessionsCount.Count <= levelIndex)
            {
                while (playerLevelsSession.LevelsSessionsCount.Count <= levelIndex)
                {
                    playerLevelsSession.LevelsSessionsCount.Add(0);
                }
            }

            playerLevelsSession.LevelsSessionsCount[levelIndex]++;
        }
    }
}