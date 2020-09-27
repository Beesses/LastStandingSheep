using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace LastStandingSheep
{
    public sealed class WolfModel
    {
        #region Fields

        public Rigidbody WolfRigidbody { get; }
        public float Speed;
        public float Force;
        public GameObject Wolf;
        private float _idleTime = 0;
        private Vector3 _destination;
        public NavMeshAgent WolfAgent;
        public GameObject[] Targets;
        public GameObject Target;
        private GameObject ShuffleObject;
        public Animator WolfAnimator;
        public bool IsAlive;

        #endregion


        #region ClassLifeCycle

        public WolfModel(GameObject prefab, WolfData wolfData)
        {
            Wolf = prefab;
            Speed = wolfData.Speed;
            Force = wolfData.Force;
            WolfRigidbody = prefab.GetComponent<Rigidbody>();
            WolfAnimator = Wolf.GetComponent<Animator>();
            IsAlive = wolfData.IsAlive;
        }

        #endregion


        #region Metods

        public void Execute()
        {
            if (IsAlive)
            {
                Shuffle();
                foreach (var Model in Targets)
                {
                    if (Model != null && Target == null)
                    {
                        Target = Model;
                        break;
                    }
                }
                if (Target != null)
                {
                    WolfAgent.destination = Target.transform.position;
                    WolfAnimator.SetBool("Run", true);
                }
                else
                {
                    WolfAnimator.SetBool("Run", false);
                }
                if (WolfRigidbody.velocity.y < -10 || WolfRigidbody.velocity.y > 10)
                {
                    WolfRigidbody.velocity = new Vector3(WolfRigidbody.velocity.x, 0, WolfRigidbody.velocity.z);
                }
            }
            else
            {
                WolfAnimator.SetBool("Run", false);
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                int rnd = Random.Range(0, Targets.Length);
                ShuffleObject = Targets[rnd];
                Targets[rnd] = Targets[i];
                Targets[i] = ShuffleObject;
            }
        }
            #endregion
        }
}
