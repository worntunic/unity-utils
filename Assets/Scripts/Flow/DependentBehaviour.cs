using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WTUtils
{
    public class DependentBehaviour : FlowBehaviour
    {
        [SerializeField] protected List<FlowBehaviour> _initConditions = new List<FlowBehaviour>();
        private bool _initConditionsReady = false;
        [SerializeField] private bool _dependOnActivation;
        [SerializeField] private bool _dependOnDeactivation;

        protected virtual void SetupInitConditions()
        {
        }
        private void SubscribeToConditions()
        {
            for (int i = 0; i < _initConditions.Count; i++)
            {
                if (!_initConditions[i].Initialized)
                {
                    _initConditions[i].OnInitialized += TryInitialize;
                }
                if (_dependOnActivation)
                {
                    _initConditions[i].OnActivated += OnConditionActivated;
                }
                if (_dependOnDeactivation)
                {
                    _initConditions[i].OnDeactivated += OnConditionDeactivated;
                }
            }
        }
        private void UnsubscribeFromConditions()
        {
            for (int i = 0; i < _initConditions.Count; i++)
            {
                if (!_initConditions[i].Initialized)
                {
                    _initConditions[i].OnInitialized -= TryInitialize;
                }
                if (_dependOnActivation)
                {
                    _initConditions[i].OnActivated -= OnConditionActivated;
                }
                if (_dependOnDeactivation)
                {
                    _initConditions[i].OnDeactivated -= OnConditionDeactivated;
                }
            }
        }
        public override void TryInitialize()
        {
            if (!_initConditionsReady)
            {
                SetupInitConditions();
                SubscribeToConditions();
                _initConditionsReady = true;
            }

            bool canInitialize = true;
            for (int i = 0; i < _initConditions.Count; i++)
            {
                if (!_initConditions[i].Initialized)
                {
                    canInitialize = false;
                }
                else
                {
                    _initConditions[i].OnInitialized -= TryInitialize;
                }
            }

            if (canInitialize)
            {
                base.TryInitialize();
            }
        }
        private void OnConditionActivated()
        {
            TryActivate();
        }
        private void OnConditionDeactivated()
        {
            TryDeactivate();
        }
        protected override bool CanBeActivated()
        {
            return base.CanBeActivated() && (!_dependOnActivation || _initConditions.All(c => c.Activated));
        }
        private void OnDestroy()
        {
            UnsubscribeFromConditions();
        }


    }
}

