using UnityEngine;

namespace WTUtils
{
    public class FlowBehaviour : MonoBehaviour
    {
        //Events
        public delegate void BehaviourAction();
        public event BehaviourAction OnInitialized;
        public event BehaviourAction OnActivated;
        public event BehaviourAction OnDeactivated;
        public event BehaviourAction OnPauseStateChanged;

        public bool Initialized { get; protected set; } = false;
        public bool Subscribed { get; protected set; } = false;
        public bool Activated { get; protected set; } = false;
        public bool Paused { get; protected set; } = false;

        private void OnEnable()
        {
            if (CanSelfInitialize())
            {
                TryInitialize();
            }
            TrySubscribe();
            TryActivate();
        }
        private void OnDisable()
        {
            TryUnsubscribe();
            if (!gameObject.activeSelf)
            {
                TryDeactivate();
            }
        }
        public virtual void TryInitialize()
        {
            if (!Initialized)
            {
                Initialize();
                //Debug.Log($"{name} initialized!");
                Initialized = true;
                OnInitialized?.Invoke();
                if (gameObject.activeInHierarchy)
                {
                    TrySubscribe();
                    TryActivate();
                }
            }
        }
        protected virtual void Initialize()
        {
            //Debug.Log($"{name} Init");
        }
        protected void TrySubscribe()
        {
            if (Initialized && !Subscribed)
            {
                Subscribe();
                Subscribed = true;
            }
        }
        protected virtual void Subscribe()
        {

        }
        protected void TryUnsubscribe()
        {
            if (Initialized && Subscribed)
            {
                Unsubscribe();
                Subscribed = false;
            }
        }
        protected virtual void Unsubscribe()
        {

        }
        public void TryActivate()
        {
            if (!Initialized && CanSelfInitialize())
            {
                TryInitialize();
            }
            if (CanBeActivated() && !Activated)
            {
                Activate();
                Activated = true;
                OnActivated?.Invoke();
            }
        }
        protected virtual void Activate()
        {
            //Debug.Log($"{name} Activate");
            if (ControlsObjectState())
            {
                gameObject.SetActive(true);
            }
        }
        public void TryDeactivate(bool forceDeactivate = false)
        {
            if (!Initialized && CanSelfInitialize())
            {
                TryInitialize();
            }
            if (Initialized && (Activated || forceDeactivate))
            {
                Deactivate();
                Activated = false;
                OnDeactivated?.Invoke();
            }
        }
        protected virtual void Deactivate()
        {
            //Debug.Log($"{name} Deactivate");
            if (ControlsObjectState())
            {
                gameObject.SetActive(false);
            }
        }
        public virtual void SetPaused(bool paused)
        {
            this.Paused = paused;
            OnPauseStateChanged?.Invoke();
        }
        public virtual bool CanSelfInitialize()
        {
            return true;
        }
        protected virtual bool CanBeActivated()
        {
            return Initialized;
        }
        protected virtual bool ControlsObjectState()
        {
            return true;
        }
        public virtual bool CanUpdate()
        {
            return Initialized && Subscribed && Activated && !Paused;
        }
    }
}

