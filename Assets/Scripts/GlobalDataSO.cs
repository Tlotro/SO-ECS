using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Game/Global Data")]
public class GlobalDataSO : ScriptableObject
{
    static GlobalDataSO _instance;
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject projectilePrefab;
    [System.NonSerialized]
    public int enemiesToSpawn = 0;
    [System.NonSerialized]
    public int wave = -1;
    public WaveDataSO[] waves;
    public Stack<GameObject> enemiesPool = new Stack<GameObject>();
    public Stack<GameObject> projectilesPool = new Stack<GameObject>();
    public static GlobalDataSO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GlobalDataSO>("GlobalData");
            }
            return _instance;
        }
    }

    public static void Reset()
    {
        Instance.wave = 0;
        Instance.enemiesToSpawn = Instance.waves[0].spawnCount;
        Instance.enemiesPool.Clear();
        Instance.projectilesPool.Clear();
    }
}
