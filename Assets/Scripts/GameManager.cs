using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : SingletonPersistentMonobehaviour<GameManager>
    {
        public bool Paused = false;
        public AudioSource BackgroundMusic;
        public AudioSource ButtonClick;
    }
}
