using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastStandingSheep
{
    public sealed class SheepInitialize: IAwake
    {
        #region Field

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public SheepInitialize(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            for(int i = 0; i < 7; i++)
            {
                SpawnSheeps(i);
            }
        }

        #endregion


        public void SpawnSheeps(int i)
        {
            var SheepData = Data.SheepData;
            GameObject instance = GameObject.Instantiate(SheepData.Prefab, new Vector3(i, 1, i), Quaternion.identity);
            SheepModel Sheep = new SheepModel(instance, SheepData);
            _context.SheepModels.Add(i, Sheep);
        }
    }
}
