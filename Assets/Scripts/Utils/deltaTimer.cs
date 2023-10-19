using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Utils
{
    /// <summary>
    /// <c>DeltaTimer</c> Is a timer that every tick removes Time.deltaTime 
    /// amount of time from the timer.
    /// </summary>
    public class DeltaTimer : ITickable
    {
        private bool _isInitialized = false;
        private float _duration;
        private Action _onTimerEnds;
        private Action _onTimerTicks;

        public void Initialize(float duration, Action onTimerEnds, Action onTimerTicks = null)
        {
            _duration = duration;
            _onTimerEnds = onTimerEnds;
            _onTimerTicks = onTimerTicks;
            _isInitialized = true;
        }

        public void Tick()
        {
            if (_isInitialized && (_duration != 0f) )
            {
                _duration -= Time.deltaTime;
                if (_duration < 0f)
                {
                    _duration = 0f;
                    _onTimerEnds.Invoke();
                }
                else
                {
                    _onTimerTicks?.Invoke();
                }
            }
        }
    }
}