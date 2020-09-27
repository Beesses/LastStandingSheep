using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LastStandingSheep
{
    public sealed class SheepController : IUpdate, IAwake, ITearDown
    {
        #region Fields

        private readonly GameContext _context;
        private MonoService _monoService;

        #endregion


        #region ClassLifeCycle

        public SheepController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IUpdate

        public void Updating()
        {
            foreach(var Model in _context.SheepModels)
            {
                Model.Value.Execute();
            }
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            _monoService = GameObject.FindGameObjectWithTag("MonoService").GetComponent<MonoService>();
            foreach(var Model in _context.SheepModels)
            {
                Model.Value.SheepAgent = Model.Value.Sheep.GetComponent<NavMeshAgent>();
            }
            var Sheeps = _context.GetTriggers(InteractableObjectType.Sheep);
            foreach (var trigger in Sheeps)
            {
                var sheepBehaviour = trigger as SheepBehaviour;
                sheepBehaviour.OnFilterHandler += OnFilterHandler;
                sheepBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                sheepBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                sheepBehaviour.OnFilterHandlerCollision += OnFilterHandlerCollision;
                sheepBehaviour.OnCollisionEnterHandler += OnCollisionEnterHandler;
                sheepBehaviour.OnCollisionExitHandler += OnCollisionExitHandler;
            }

        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var Sheeps = _context.GetTriggers(InteractableObjectType.Sheep);
            foreach (var trigger in Sheeps)
            {
                var sheepBehaviour = trigger as SheepBehaviour;
                sheepBehaviour.OnFilterHandler -= OnFilterHandler;
                sheepBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                sheepBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;

                sheepBehaviour.OnFilterHandlerCollision -= OnFilterHandlerCollision;
                sheepBehaviour.OnCollisionEnterHandler -= OnCollisionEnterHandler;
                sheepBehaviour.OnCollisionExitHandler -= OnCollisionExitHandler;
            }
        }

        #endregion


        #region Methods

        private bool OnFilterHandler(Collider tagObject)
        {
            Collider TagCollider;
            if (tagObject.CompareTag("Edge"))
            {
                TagCollider = tagObject;
                return TagCollider;
            }
            if (tagObject.CompareTag("Platform"))
            {
                TagCollider = tagObject;
                return TagCollider;
            }
            return false;
        }

        private bool OnFilterHandlerCollision(Collision tagObject)
        {
            return tagObject.transform.CompareTag("Sheep");
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject, Collider other)
        {
            if (other.CompareTag("Edge"))
            {
                GameObject.Destroy(enteredObject.GameObject.gameObject);
            }
            if (other.CompareTag("Platform"))
            {
                foreach (var Model in _context.SheepModels)
                {
                    if (enteredObject.GameObject.transform == Model.Value.GetPosition())
                    {
                        Model.Value.IsPatrol = false;
                        Model.Value.SheepAgent.ResetPath();
                        Model.Value.SheepAgent.updatePosition = false;
                        Model.Value.IsOnPlatform = true;
                        break;
                    }
                }
            }
        }

        private void OnTriggerExitHandler(ITrigger enteredObject, Collider other)
        {
            enteredObject.IsInteractable = false;
        }

        private void OnCollisionEnterHandler(ITrigger collisionObject, Collision other)
        {
            foreach (var Model in _context.SheepModels)
            {
                if (other.transform == Model.Value.GetPosition())
                {
                    if (!Model.Value.IsOnPlatform)
                    {
                        Model.Value.IsPatrol = false;
                        Model.Value.SheepAgent.ResetPath();
                        Model.Value.SheepAgent.updatePosition = false;
                        _monoService.StartC(Model.Value, 2);
                        break;
                    }
                }
            }
            Services.SharedInstance.PhysicsService.PushEntity(other, _context.SheepModels[0].Force);
        }

        private void OnCollisionExitHandler(ITrigger collisionObject, Collision other)
        {

        }

        #endregion
    }
}
