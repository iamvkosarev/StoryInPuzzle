using Extentions;
using UnityEngine;

namespace Core.Common
{
    public class SavingDataProvider : ProtectedSingleton<SavingDataProvider>
    {
        [SerializeField] private PlayerComponent playerComponent;
        public static PlayerComponent PlayerComponent => Instance != null ? Instance.playerComponent : null;
    }
}