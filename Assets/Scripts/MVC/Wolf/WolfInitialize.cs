using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastStandingSheep
{
    public sealed class WolfInitialize : IAwake
    {
        #region Field

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public WolfInitialize(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            for (int i = 0; i < 1; i++)
            {
                SpawnWolfs(i);
            }
        }

        #endregion


        public void SpawnWolfs(int i)
        {
            var WolfData = Data.WolfData;
            GameObject instance = GameObject.Instantiate(WolfData.Prefab, new Vector3(-i, 1, -2), Quaternion.identity);
            WolfModel Wolf = new WolfModel(instance, WolfData);
            _context.WolfModels.Add(i, Wolf);
        }
    }
}

