using System;

namespace WTUtils
{
    public class Timer
    {
        private float _duration;
        private float _time;
        private bool _paused = false;

        public event Action Ticked;

        public Timer(float duration)
        {
            _duration = duration;
            _time = 0;
        }
        public void Reset()
        {
            _time = 0;
        }
        public void Stop()
        {
            _time = 0;
        }
        public void SetPaused(bool paused)
        {
            _paused = paused;
        }

        public bool Done()
        {
            return _time >= _duration;
        }

    }
}

