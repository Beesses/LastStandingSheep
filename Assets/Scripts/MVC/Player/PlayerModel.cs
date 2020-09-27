using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastStandingSheep
{
    public sealed class PlayerModel
    {
        #region Fields

        public Rigidbody SheepRigidbody { get; }
        public Transform transfrom { get; }
        public float Speed;
        public float Force;
        public GameObject Player;

        #endregion


        #region ClassLifeCycle

        public PlayerModel(GameObject prefab, PlayerData playerData)
        {
            Speed = playerData.Speed;
            Force = playerData.Force;
            SheepRigidbody = prefab.GetComponent<Rigidbody>();
            Player = prefab;
        }

        #endregion


        #region Metods

        public void Execute()
        {
            if (Player)
            {
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                float moveVertical = Input.GetAxisRaw("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                if (movement != Vector3.zero)
                {
                    Player.transform.rotation = Quaternion.LookRotation(movement);
                }
                Player.transform.Translate(movement * Speed * Time.deltaTime, Space.World);
            }
        }

        #endregion
    }
}

