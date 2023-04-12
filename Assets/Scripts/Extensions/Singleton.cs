using UnityEngine;

namespace Extentions
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    InitInstance();
                }

                return instance;
            }
        }

        private static void InitInstance()
        {
            if (instance != null)
                return;

            instance = (T)FindObjectOfType(typeof(T));

            if (instance == null)
            {
                GameObject singleton = new GameObject();
                instance = singleton.AddComponent<T>();
                singleton.name = "(singleton) " + typeof(T).ToString();
            }
        }

        public static bool HasInstance
        {
            get => instance != null;
        }

        protected bool SetDontDestroy()
        {
            if (HasInstance && Instance != this)
            {
                Destroy(gameObject);
                return false;
            }
            else
            {
                InitInstance();
                DontDestroyOnLoad(gameObject);
                return true;
            }
        }
    }
}