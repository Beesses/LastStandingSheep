using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace LastStandingSheep
{
    public sealed class SheepModel
    {
        #region Fields

        private Rigidbody SheepRigidbody { get; }
        public Transform transfrom { get; }
        public float Speed;
        public float Force;
        public GameObject Sheep;
        private float _idleTime = 0;
        public Vector3 _destination;
        public NavMeshAgent SheepAgent;
        public bool IsPatrol = true;
        public bool IsAlive;
        private GameObject CurrentPlatform;
        private PlatformScript PlatformScript;
        private Animator SheepAnimator;
        public bool IsOnPlatform;

        #endregion


        #region ClassLifeCycle

        public SheepModel(GameObject prefab, SheepData sheepData)
        {
            Speed = sheepData.Speed;
            Force = sheepData.Force;
            SheepRigidbody = prefab.GetComponent<Rigidbody>();
            SheepAnimator = prefab.GetComponent<Animator>();
            Sheep = prefab;
            IsAlive = sheepData.IsAlive;
        }

        #endregion


        #region Metods

        public void Execute()
        {
            if (IsAlive)
            {
                if (!CurrentPlatform)
                {
                    if (SheepAgent)
                    {
                        SheepAgent.nextPosition = GetPosition().position;
                        SheepAgent.updatePosition = true;
                        IsPatrol = true;
                        IsOnPlatform = false;
                    }
                    FindPlatform();
                    if (CurrentPlatform)
                    {
                        PlatformScript = CurrentPlatform.GetComponent<PlatformScript>();
                        if (PlatformScript.IsPossibleToClimb)
                        {
                            if (SheepAgent)
                            {
                                IsPatrol = false;
                                SheepAgent.destination = CurrentPlatform.transform.position;
                            }
                        }
                        else
                        {
                            IsPatrol = true;
                            SheepAgent.ResetPath();
                        }
                    }
                }
                if (SheepAgent)
                {
                    Patrol(SheepAgent, 15, IsPatrol);
                }
                if (SheepRigidbody)
                {
                    if (SheepRigidbody.velocity.y < -10 || SheepRigidbody.velocity.y > 10)
                    {
                        SheepRigidbody.velocity = new Vector3(SheepRigidbody.velocity.x, 0, SheepRigidbody.velocity.z);
                    }
                }
            }
            
        }

        public void Patrol(NavMeshAgent Agent, float PatrolDistance, bool IsPatrol)
        {
            _idleTime -= Time.deltaTime;
            if (Agent.remainingDistance < 0.1f)
            {
                SheepAnimator.SetBool("Run", false);
            }
            if (Agent.pathPending || Agent.remainingDistance > 0.1f)
            {
                SheepAnimator.SetBool("Run", true);
                return;
            }
            else if (IsPatrol && _idleTime <= 0)
            {
                _destination = PatrolDistance * UnityEngine.Random.insideUnitSphere;
                _destination.y = 0;
            }
            if (_idleTime <= 0)
            {
                _idleTime = UnityEngine.Random.Range(0, 15);
            }
            Agent.destination = _destination;
        }

        public Transform GetPosition()
        {
            if (Sheep)
            {
                return Sheep.transform;
            }
            return null;
        }


        public void FindPlatform()
        {
            try
            {
                CurrentPlatform = GameObject.FindGameObjectWithTag("Platform");
            }
            finally
            {

            }
        }
        #endregion
    }
}

