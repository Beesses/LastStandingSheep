using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastStandingSheep
{
    public class MonoService : MonoBehaviour
    {
        #region Metods

        public void StartC(SheepModel Model, int Time)
        {
            StartCoroutine(updateModel(Model, Time));
        }

        public IEnumerator updateModel(SheepModel Model, int Time)
        {
            yield return new WaitForSeconds(Time);
            if (Model.Sheep != null)
            {
                Model.SheepAgent.nextPosition = Model.GetPosition().position;
                Model.SheepAgent.updatePosition = true;
                Model.IsPatrol = true;
            }
        }

        #endregion
    }
}
