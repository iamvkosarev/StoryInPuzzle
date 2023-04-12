using System.Collections.Generic;
using Core.Movement;
using Extentions;
using UnityEngine;

namespace Core.Setup
{
    public class CursorManipulator : Singleton<CursorManipulator>
    {
        private List<Component> cursorUsers = new();
        void Start()
        {
            Cursor.visible = false;
        }

        public void AddCursorUser(Component component)
        {
            if (!cursorUsers.Contains(component))
            {
                cursorUsers.Add(component);
                SwitchCursor();
            }
        }

        public void RemoveCursorUser(Component component)
        {
            if (cursorUsers.Contains(component))
            {
                cursorUsers.Remove(component);
                SwitchCursor();
            }
        }

        private void SwitchCursor()
        {
            var moreThenOneUser = cursorUsers.Count > 0;
            Cursor.visible = moreThenOneUser;
            if (moreThenOneUser)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            MovementSwitcher.Instance.SwitchMovement(!moreThenOneUser);
        }
    }
}