using UnityEngine;

namespace WTUtils
{
    public class ExampleFlow : FlowBehaviour
    {
        [SerializeField] private float _duration;

        private Timer _timer;

        public delegate void TimerAction();
        public event TimerAction OnTimerDone;

        protected override void Initialize()
        {
            base.Initialize();
            _timer = new Timer(_duration);
        }
        protected override void Activate()
        {
            base.Activate();
            _timer.Reset();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            _timer.Ticked += OnTimerTick;
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            _timer.Ticked -= OnTimerTick;
        }

        public override void SetPaused(bool paused)
        {
            base.SetPaused(paused);
            _timer.SetPaused(paused);
        }

        private void OnTimerTick()
        {
            Debug.Log("Tock");
            if (_timer.Done())
            {
                _timer.Stop();
                OnTimerDone?.Invoke();
            }
        }
    }
}