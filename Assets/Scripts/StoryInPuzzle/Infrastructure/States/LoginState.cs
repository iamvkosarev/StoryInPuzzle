using System.Threading.Tasks;
using Sirenix.Utilities;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.LoginScreen;
using StoryInPuzzle.Infrastructure.Services.Data;
using StoryInPuzzle.Infrastructure.Services.LoadingScreen;
using UnityEngine;

namespace StoryInPuzzle.Infrastructure.States
{
    public class LoginState : IState
    {
        private readonly ILoadingScreen _loadingScreen;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILoginScreenProvider _loginScreenProvider;
        private readonly IGameDataContainer _gameDataContainer;
        private readonly ISaveLoadData _saveLoadData;
        private LoginScreen _screen;

        public LoginState(ILoadingScreen loadingScreen, IGameStateMachine gameStateMachine, ILoginScreenProvider loginScreenProvider, IGameDataContainer gameDataContainer, ISaveLoadData saveLoadData)
        {
            _loadingScreen = loadingScreen;
            _gameStateMachine = gameStateMachine;
            _loginScreenProvider = loginScreenProvider;
            _gameDataContainer = gameDataContainer;
            _saveLoadData = saveLoadData;
        }

        public async void Enter()
        {
            _loadingScreen.Show();
            _screen = await _loginScreenProvider.Load();
            _loadingScreen.Hide();
            _screen.ErrorMessage.gameObject.SetActive(false);
            _screen.SendButton.onClick.AddListener(TrySaveLogin);
        }

        private void TrySaveLogin()
        {
            if (_screen.Input.text.IsNullOrWhitespace())
            {
                _screen.ErrorMessage.gameObject.SetActive(true);
                _screen.ErrorMessage.text = "Логин должен не должен быть пустым";
                return;
            }

            if (_screen.Input.text.Length >= 16)
            {
                _screen.ErrorMessage.gameObject.SetActive(true);
                _screen.ErrorMessage.text = "Логин должен стоять максимум из 16 символов";
                return;
            }
            SaveLogin();
            _gameStateMachine.Enter<SelectLevelsState>();
        }

        private void SaveLogin()
        {
            _gameDataContainer.GameData.PlayerData.NickName = _screen.Input.text;
            _saveLoadData.Save();
        }

        public void Exit()
        {
            _screen.SendButton.onClick.RemoveListener(TrySaveLogin);
            _loginScreenProvider.Unload();
        }
    }
}