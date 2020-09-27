using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LastStandingSheep
{
    public sealed class PhysicsService : Service
    {
        #region ClassLifeCycle

        public PhysicsService(Contexts contexts) : base(contexts)
        {

        }

        #endregion


        #region Metods

        public void PushEntity(Collision PushObject, float Force)
        {
            Vector3 push = PushObject.contacts[0].point - PushObject.transform.position;
            push = -push.normalized;
            PushObject.rigidbody.AddForce(push * Force, ForceMode.Impulse);
        }

        #endregion
    }
}
