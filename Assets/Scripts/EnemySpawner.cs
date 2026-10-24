using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float range; 
    private CancellationTokenSource cts;

    async void Start()
    {
        cts = new CancellationTokenSource(); 
        try
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                EnemySO so = GlobalDataSO.Instance.waves[GlobalDataSO.Instance.wave % GlobalDataSO.Instance.waves.Length].enemy;
                GameObject enemy = Enemy.CreateEnemy(so);
                Vector2 r = UnityEngine.Random.onUnitCircle * range;
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
        catch (OperationCanceledException)
        {
            cts?.Dispose();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        cts?.Cancel();
    }
}
