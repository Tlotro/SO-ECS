using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MovementController movementController;
    public HPController hpController;
    [System.NonSerialized]
    public EnemySO data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (GlobalDataSO.Instance.player != null)
        {
            movementController.Direction = GlobalDataSO.Instance.player.transform.position - transform.position;
        }
    }

    public void Kill()
    {
        GlobalDataSO.Instance.player.GetComponent<XPController>().AddXP(data.XPValue);
        GlobalDataSO.Instance.enemiesPool.Push(gameObject);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public static GameObject CreateEnemy(EnemySO so)
    {
        GameObject enemy;
        if (GlobalDataSO.Instance.enemiesPool.Count == 0)
            enemy = GameObject.Instantiate(GlobalDataSO.Instance.enemyPrefab);
        else
            enemy = GlobalDataSO.Instance.enemiesPool.Pop();
        enemy.SetActive(true);
        Enemy script = enemy.GetComponent<Enemy>();
        script.data = so;
        script.movementController.speed = so.Speed;
        script.hpController.HP = so.MaxHP;
        script.hpController.MAXHP = so.MaxHP;
        script.gameObject.GetComponent<SpriteRenderer>().sprite = so.EnemySprite;
        return enemy;
    }
}
