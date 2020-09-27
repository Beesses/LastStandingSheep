using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LastStandingSheep
{
    public sealed class WolfController : IAwake, IUpdate, ITearDown
    {
        #region Fields

        private readonly GameContext _context;
        private MonoService _monoService;

        #endregion


        #region ClassLifeCycle

        public WolfController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IUpdate

        public void Updating()
        {
            foreach (var Model in _context.WolfModels)
            {
                Model.Value.Execute();
            }
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            _monoService = GameObject.FindGameObjectWithTag("MonoService").GetComponent<MonoService>();
            _context.WolfModels[0].Targets = new GameObject[_context.SheepModels.Count + 1];
            for(int i = 0; i < _context.SheepModels.Count; i++)
            {
                _context.WolfModels[0].Targets[i] = _context.SheepModels[i].Sheep;
            }
            _context.WolfModels[0].Targets[_context.WolfModels[0].Targets.Length - 1] = _context.PlayerModel.Player;
            foreach (var Model in _context.WolfModels)
            {
                Model.Value.WolfAgent = Model.Value.Wolf.GetComponent<NavMeshAgent>();
            }
            var Wolfs = _context.GetTriggers(InteractableObjectType.Wolf);
            foreach (var trigger in Wolfs)
            {
                var wolfBehaviour = trigger as WolfBehaviour;
                wolfBehaviour.OnFilterHandler += OnFilterHandler;
                wolfBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                wolfBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                wolfBehaviour.OnFilterHandlerCollision += OnFilterHandlerCollision;
                wolfBehaviour.OnCollisionEnterHandler += OnCollisionEnterHandler;
                wolfBehaviour.OnCollisionExitHandler += OnCollisionExitHandler;
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var Wolfs = _context.GetTriggers(InteractableObjectType.Wolf);
            foreach (var trigger in Wolfs)
            {
                var wolfBehaviour = trigger as WolfBehaviour;
                wolfBehaviour.OnFilterHandler -= OnFilterHandler;
                wolfBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                wolfBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;

                wolfBehaviour.OnFilterHandlerCollision -= OnFilterHandlerCollision;
                wolfBehaviour.OnCollisionEnterHandler -= OnCollisionEnterHandler;
                wolfBehaviour.OnCollisionExitHandler -= OnCollisionExitHandler;
            }
        }

        #endregion


        #region Methods

        private bool OnFilterHandler(Collider tagObject)
        {
            return tagObject.CompareTag("Sheep");
        }

        private bool OnFilterHandlerCollision(Collision tagObject)
        {
            return tagObject.transform.CompareTag("Sheep");
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject, Collider other)
        {

        }

        private void OnTriggerExitHandler(ITrigger enteredObject, Collider other)
        {

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
                        _monoService.StartC(Model.Value, 6);
                        break;
                    }
                }
            }
            Services.SharedInstance.PhysicsService.PushEntity(other, _context.WolfModels[0].Force);
        }

        private void OnCollisionExitHandler(ITrigger collisionObject, Collision other)
        {

        }

        #endregion
    }
}

