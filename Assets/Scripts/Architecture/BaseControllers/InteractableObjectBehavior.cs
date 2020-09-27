using System;
using UnityEngine;


namespace LastStandingSheep
{
    public class InteractableObjectBehavior : MonoBehaviour, ITrigger
    {
        #region Fields

        [SerializeField] private InteractableObjectType _type;

        #endregion


        #region Properties

        public Predicate<Collider> OnFilterHandler { get; set; }
        public Predicate<Collision> OnFilterHandlerCollision { get; set; }
        public Action<ITrigger, Collider> OnTriggerEnterHandler { get; set; }
        public Action<ITrigger, Collider> OnTriggerExitHandler { get; set; }
        public Action<ITrigger, Collision> OnCollisionEnterHandler { get; set; }
        public Action<ITrigger, Collision> OnCollisionExitHandler { get; set; }
        public Action<ITrigger, InteractableObjectType> DestroyHandler { get; set; }
        public GameObject GameObject => gameObject;
        public InteractableObjectType Type { get => _type; }
        public bool IsInteractable { get; set; }

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (OnFilterHandler != null && OnFilterHandler.Invoke(other))
            {
                OnTriggerEnterHandler?.Invoke(this, other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (OnFilterHandler != null && OnFilterHandler.Invoke(other))
            {
                OnTriggerExitHandler?.Invoke(this, other);
            }
        }

        private void OnDisable()
        {
            DestroyHandler?.Invoke(this, _type);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (OnFilterHandler != null && OnFilterHandlerCollision.Invoke(other))
            {
                OnCollisionEnterHandler?.Invoke(this, other);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (OnFilterHandler != null && OnFilterHandlerCollision.Invoke(other))
            {
                OnCollisionExitHandler?.Invoke(this, other);
            }
        }

        #endregion


        #region Methods

        public void SetType(InteractableObjectType type)
        {
            _type = type;
        }
        
        #endregion
    }
}
