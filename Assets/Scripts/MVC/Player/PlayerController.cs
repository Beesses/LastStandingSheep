using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastStandingSheep
{
    public sealed class PlayerController: IAwake, IUpdate
    {
        #region Fields

        private readonly GameContext _context;
        private MonoService _monoService;

        #endregion


        #region ClassLifeCycle

        public PlayerController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IUpdate

        public void Updating()
        {
            _context.PlayerModel.Execute();
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            _monoService = GameObject.FindGameObjectWithTag("MonoService").GetComponent<MonoService>();
            var Player = _context.GetTriggers(InteractableObjectType.Player);
            foreach (var trigger in Player)
            {
                var playerBehaviour = trigger as PlayerBehaviour;
                playerBehaviour.OnFilterHandler += OnFilterHandler;
                playerBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                playerBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                playerBehaviour.OnFilterHandlerCollision += OnFilterHandlerCollision;
                playerBehaviour.OnCollisionEnterHandler += OnCollisionEnterHandler;
                playerBehaviour.OnCollisionExitHandler += OnCollisionExitHandler;
            }

        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var Player = _context.GetTriggers(InteractableObjectType.Player);
            foreach (var trigger in Player)
            {
                var playerBehaviour = trigger as PlayerBehaviour;
                playerBehaviour.OnFilterHandler -= OnFilterHandler;
                playerBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                playerBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;

                playerBehaviour.OnFilterHandlerCollision -= OnFilterHandlerCollision;
                playerBehaviour.OnCollisionEnterHandler -= OnCollisionEnterHandler;
                playerBehaviour.OnCollisionExitHandler -= OnCollisionExitHandler;
            }
        }

        #endregion


        #region Methods

        private bool OnFilterHandler(Collider tagObject)
        {
            return tagObject.CompareTag("Edge");
        }

        private bool OnFilterHandlerCollision(Collision tagObject)
        {
            return tagObject.transform.CompareTag("Sheep");
        }

        private void OnTriggerEnterHandler(ITrigger enteredObject, Collider other)
        {
            enteredObject.IsInteractable = true;
            Debug.Log("Enter");
            GameObject.Destroy(enteredObject.GameObject.gameObject);
        }

        private void OnTriggerExitHandler(ITrigger enteredObject, Collider other)
        {
            enteredObject.IsInteractable = false;
            Debug.Log("Exit");
        }

        private void OnCollisionEnterHandler(ITrigger collisionObject, Collision other)
        {
            foreach (var Model in _context.SheepModels)
            {
                if (other.transform == Model.Value.GetPosition())
                {
                    Model.Value.IsPatrol = false;
                    Model.Value.SheepAgent.ResetPath();
                    Model.Value.SheepAgent.updatePosition = false;
                    _monoService.StartC(Model.Value, 3);
                    break;
                }
            }
            Services.SharedInstance.PhysicsService.PushEntity(other, _context.PlayerModel.Force);
        }

        private void OnCollisionExitHandler(ITrigger collisionObject, Collision other)
        {

        }

        #endregion
    }

}
