using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Timer
{
    public class TimerClock : MonoBehaviour
    {
        private YieldInstruction _waitSecond = new WaitForSeconds(1);
        public Text TimeText;

        public void StartTimer(float time, Action callback = null)
        {
            StartCoroutine(TimeProgress(time, callback));
        }

        private IEnumerator TimeProgress(float time, Action callback)
        {
            float startTime = Time.time;
            float endTime = startTime + time;

            while(Time.time < endTime)
            {
                float timeRemaining = endTime - Time.time;
                TimeText.text = timeRemaining.ToString("00");

                yield return _waitSecond;
            }

            if (callback != null)
                callback();

        }
    }
}
