using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastStandingSheep
{
    public sealed class GameLevelController: IUpdate, IAwake
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycle

        public GameLevelController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IUpdate

        public void Updating()
        {
            _context.GameLevelModel.Execute();
        }

        #endregion


        #region IAwake

        public void OnAwake()
        {

        }

        #endregion
    }
}
