using System.IO;
using UnityEngine;


namespace LastStandingSheep
{
    [CreateAssetMenu (fileName = "Data", menuName = "DataTest")]
    public sealed class Data : ScriptableObject 
    {
        #region Fields
        
        [SerializeField] private string _sheepDataPath;
        [SerializeField] private string _wolfDataPath;
        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _gameLevelDataPath;

        private static Data _instance;
        private static SheepData _sheepData;
        private static WolfData _wolfData;
        private static PlayerData _playerData;
        private static GameLevelData _gameLevelData;

        #endregion


        #region Properties

        public static Data Instance {
            get {
                if (_instance == null) {
                    _instance = Resources.Load<Data> ("Data/" + typeof (Data).Name);
                }
                return _instance;
            }
        }

        public static GameLevelData GameLevelData
        {
            get
            {
                if (_gameLevelData == null)
                {
                    _gameLevelData = Resources.Load<GameLevelData>("Data/" + Instance._gameLevelDataPath);
                }
                return _gameLevelData;
            }
        }

        public static SheepData SheepData
        {
            get
            {
                if (_sheepData == null)
                {
                    _sheepData = Resources.Load<SheepData>("Data/" + Instance._sheepDataPath);
                }
                return _sheepData;
            }
        }

        public static PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = Resources.Load<PlayerData>("Data/" + Instance._playerDataPath);
                }
                return _playerData;
            }
        }

        public static WolfData WolfData
        {
            get
            {
                if (_wolfData == null)
                {
                    _wolfData = Resources.Load<WolfData>("Data/" + Instance._wolfDataPath);
                }
                return _wolfData;
            }
        }
        #endregion


        #region Methods

        private static T Load<T> (string resourcesPath) where T : Object =>
            Resources.Load<T> (Path.ChangeExtension (resourcesPath, null));

        #endregion
    }
}