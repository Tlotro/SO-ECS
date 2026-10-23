using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Game/Wave Data")]
public class WaveDataSO : ScriptableObject
{
    public EnemySO enemy;
    public float spawnDelay;
    public int spawnCount;
}
