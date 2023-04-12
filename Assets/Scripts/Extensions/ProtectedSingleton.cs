using UnityEngine;

namespace Extentions
{
    public class ProtectedSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        protected static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(protectedSingleton) " + typeof(T).ToString();
                    }
                }

                return instance;
            }
        }
    }
}
