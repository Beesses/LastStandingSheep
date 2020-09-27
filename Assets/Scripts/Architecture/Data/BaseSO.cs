using UnityEngine;

public abstract class BaseSO : ScriptableObject
{
    public float Speed;
    public bool IsAlive;
    public Vector3 SpawnPosition;
    public float Force;
    public GameObject Prefab;
}
