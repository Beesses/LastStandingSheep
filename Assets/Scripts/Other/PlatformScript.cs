using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformScript : MonoBehaviour
{
    #region Fields

    public float timer = 0;
    public bool IsPossibleToClimb = true;

    #endregion


    #region UnityMetods

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3 && timer <= 5)
        {
            IsPossibleToClimb = false;
            MovingUp();
        }
    }

    #endregion


    #region Metods

    public void MovingUp()
    {
        transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
    }

    #endregion
}
