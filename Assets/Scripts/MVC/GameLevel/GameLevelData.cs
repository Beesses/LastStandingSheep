using UnityEngine;


namespace LastStandingSheep
{
    [CreateAssetMenu(fileName = "NewData", menuName = "CreateData/GameLevelData", order = 0)]
    public class GameLevelData : ScriptableObject
    {
        public GameObject Platform;
        public float Duration;
        public float TimeLeft;
    }
}
