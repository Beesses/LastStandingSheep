using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastStandingSheep
{
    public sealed class GameLevelInitialize : IAwake
    {
        #region Field

        GameContext _context;

        #endregion


        #region ClassLifeCycle

        public GameLevelInitialize(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {
            GameLevelModel GameLevelModel = new GameLevelModel(Data.GameLevelData, _context);
            _context.GameLevelModel = GameLevelModel;
        }

        #endregion
    }
}

