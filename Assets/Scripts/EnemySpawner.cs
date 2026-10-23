using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float range;

    async void Start()
    {
        while (true)
        {
            EnemySO so = GlobalDataSO.Instance.waves[GlobalDataSO.Instance.wave % GlobalDataSO.Instance.waves.Length].enemy;
            GameObject enemy = Enemy.CreateEnemy(so);
            Vector2 r = Random.onUnitCircle * range;
            enemy.transform.position = transform.position + new Vector3(r.x, r.y);
            GlobalDataSO.Instance.enemiesToSpawn--;
            await Awaitable.WaitForSecondsAsync(GlobalDataSO.Instance.waves[GlobalDataSO.Instance.wave % GlobalDataSO.Instance.waves.Length].spawnDelay);

            if (GlobalDataSO.Instance.enemiesToSpawn == 0)
            {
                GlobalDataSO.Instance.wave++;
                GlobalDataSO.Instance.enemiesToSpawn = GlobalDataSO.Instance.waves[GlobalDataSO.Instance.wave % GlobalDataSO.Instance.waves.Length].spawnCount;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
