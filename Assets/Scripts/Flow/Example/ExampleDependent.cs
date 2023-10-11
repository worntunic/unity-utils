using UnityEngine;

namespace WTUtils
{
    public class ExampleDependent : DependentBehaviour
    {
        [SerializeField] private ExampleFlow _exampleFlow;
        [SerializeField] private ExampleManaged _exampleManaged;

        [SerializeField] private string _someDescriptionText;

        protected override void Initialize()
        {
            base.Initialize();
            _exampleManaged.Setup(_someDescriptionText);
        }
        protected override void SetupInitConditions()
        {
            base.SetupInitConditions();
            _initConditions.Add(_exampleFlow);
        }
        protected override void Subscribe()
        {
            base.Subscribe();
            _exampleFlow.OnTimerDone += OnTimerDone;
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            _exampleFlow.OnTimerDone -= OnTimerDone;

        }

        public override void SetPaused(bool paused)
        {
            base.SetPaused(paused);
            _exampleManaged.SetPaused(paused);
        }

        private void OnTimerDone()
        {
            Debug.Log("Ding");
        }
    }
}