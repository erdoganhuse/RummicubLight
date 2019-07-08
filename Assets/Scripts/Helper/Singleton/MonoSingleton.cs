using UnityEngine;

namespace Helper.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        /// <summary>
        ///     Returns the instance of this singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject go = new GameObject("~" + typeof(T).Name);
                        _instance = go.AddComponent<T>();
                    }
                    _instance.Init();
                }

                return _instance;
            }
        }

        protected virtual void Init() { }

        /// <summary>
        ///     Dispose the instance of this singleton.
        /// </summary>
        protected static void Dispose()
        {
            Destroy(_instance);
            _instance = null;
        }
    }
}