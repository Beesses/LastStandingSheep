using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastStandingSheep
{
    public sealed class GameLevelModel
    {
        #region Fields

        public GameObject Platform;
        public float Duration;
        public float TimeLeft;
        public GameObject[] PlatformSpots;
        public bool IsActivePlatform = false;
        public GameObject CurrentPlatform;
        float timer = 0;
        //public GameObject Wolf;
        private GameContext _context;

        #endregion


        #region ClassLifeCycle

        public GameLevelModel(GameLevelData Data, GameContext context)
        {
            Platform = Data.Platform;
            Duration = Data.Duration;
            TimeLeft = Data.TimeLeft;
            PlatformSpots = GameObject.FindGameObjectsWithTag("Spot");
            _context = context;
        }

        #endregion


        #region Metods

        public void Execute()
        {
            SpawnPlatform();
            if(Duration > 1)
            {
                TimeRemaining();
            }
        }

        public void SpawnPlatform()
        {
            if (!IsActivePlatform)
            {
                if (CurrentPlatform)
                {
                    GameObject.Destroy(CurrentPlatform);
                }
                CurrentPlatform = GameObject.Instantiate(Platform, GetSpot().position, Quaternion.identity);
                _context.WolfModels[0].IsAlive = false;
                IsActivePlatform = true;
            }
        }

        public Transform GetSpot()
        {
            var index = Random.Range(0, PlatformSpots.Length);
            return PlatformSpots[index].transform;
        }

        public void TimeRemaining()
        {
            if (IsActivePlatform)
            {
                timer += Time.deltaTime;
                if(timer >= 3 && !_context.WolfModels[0].IsAlive)
                {
                    _context.WolfModels[0].IsAlive = true;
                }
                if(timer >= Duration)
                {
                    Duration -= TimeLeft;
                    timer = 0;
                    IsActivePlatform = false;
                }
            }
        }
        #endregion
    }
}
