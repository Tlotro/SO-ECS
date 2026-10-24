using System.Collections.Generic;
using System.Linq;
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
    [System.NonSerialized]
    public Stack<GameObject> enemiesPool = new Stack<GameObject>();
    [System.NonSerialized]
    public Stack<GameObject> projectilesPool = new Stack<GameObject>();
    public AbilitySO[] AbilitiesStarterUpgradePool;
    [System.NonSerialized]
    public List<Upgrade> CurrentAbilityPool = new List<Upgrade>();
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

    public void Reset()
    {
        wave = 0;
        enemiesToSpawn = Instance.waves[0].spawnCount;
        enemiesPool.Clear();
        projectilesPool.Clear();
        foreach(var ability in Instance.AbilitiesStarterUpgradePool)
        {

        }
    }
}
