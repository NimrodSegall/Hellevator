using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Utils
{
    public class Timer : ITickable
    {
        private float duration;
        private Action onTimerEnds;

        public void Initialize(float duration, Action onTimerEnds)
        {
            this.duration = duration;
            this.onTimerEnds = onTimerEnds;
        }

        public void Tick()
        {
            if (duration != 0)
            {
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    onTimerEnds.Invoke();
                    duration = 0;
                }
            }
        }
    }
}