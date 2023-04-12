using System;
using Core.Common;
using Core.Setup;
using UnityEngine;

namespace Core.Settings
{
    public class SettingsSwitcher : MonoBehaviour
    {
        private static SettingsView settingsView => SavingDataProvider.SettingsView;

        private bool opened;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchSettings();
            }
        }

        private void SwitchSettings()
        {
            opened = !opened;
            settingsView.gameObject.SetActive(opened);
            if (opened)
            {
                CursorManipulator.Instance.AddCursorUser(this);
            }
            else
            {
                CursorManipulator.Instance.RemoveCursorUser(this);
            }
        }
    }
}