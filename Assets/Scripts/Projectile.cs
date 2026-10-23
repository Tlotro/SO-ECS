using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MovementController movementController; 
    [System.NonSerialized]
    public ProjectileSO data;
    void Start() => data.start(this);

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - GlobalDataSO.Instance.player.transform.position).magnitude > 20)
            Kill();
        data.update(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            data.OnWallHit(this);
        }

        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<HPController>().TakeDamage(data.damage);
            data.OnHit(this, collision.GetComponent<Enemy>());
        }
    }

    public void Kill()
    {
        GlobalDataSO.Instance.projectilesPool.Push(gameObject);
        gameObject.SetActive(false);
    }

    public static GameObject CreateProjectile(ProjectileSO so)
    {
        GameObject projectile;
        if (GlobalDataSO.Instance.projectilesPool.Count == 0)
            projectile = GameObject.Instantiate(GlobalDataSO.Instance.projectilePrefab);
        else
            projectile = GlobalDataSO.Instance.projectilesPool.Pop();
        projectile.SetActive(true);
        Projectile script = projectile.GetComponent<Projectile>();
        script.data = so;
        script.movementController.speed = so.Speed;
        script.gameObject.GetComponent<SpriteRenderer>().sprite = so.ProjectileSprite;
        return projectile;
    }
}
