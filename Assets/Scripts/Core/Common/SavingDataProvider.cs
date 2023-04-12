using Core.Settings;
using Extentions;
using UnityEngine;

namespace Core.Common
{
    public class SavingDataProvider : ProtectedSingleton<SavingDataProvider>
    {
        [SerializeField] private PlayerComponent playerComponent;
        [SerializeField] private SettingsView settingsView;

        public static SettingsView SettingsView => Instance != null ? Instance.settingsView : null;

        public static PlayerComponent PlayerComponent => Instance != null ? Instance.playerComponent : null;
    }
}