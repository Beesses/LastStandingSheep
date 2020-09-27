using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastStandingSheep
{
    public class PlayerInitialize : IAwake
    {
        #region Field

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public PlayerInitialize(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            var PlayerData = Data.PlayerData;
            GameObject instance = GameObject.Instantiate(PlayerData.Prefab, new Vector3(-3, 1, -3), Quaternion.identity);
            PlayerModel Player = new PlayerModel(instance, PlayerData);
            _context.PlayerModel = Player;
        }

        #endregion
    }
}

