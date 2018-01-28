using UnityEngine;

namespace Assets.Scripts.Util
{
    public abstract class SingletonPersistentMonobehaviour <T> : MonoBehaviour
        where T : SingletonPersistentMonobehaviour<T>
    {
        public static T Instance { get; private set; }

        public void Awake ()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = (T) this;
            } else
            {
                DestroyObject(gameObject);
            }
        }

    }
}
