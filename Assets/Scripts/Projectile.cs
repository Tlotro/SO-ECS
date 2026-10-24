using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MovementController movementController; 
    [System.NonSerialized]
    public ProjectileSO data;
    public float lifetime = 0;
    public System.Object[] extraData;
    void Start() => data.start(this);

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if ((data.MaxLifetime > 0) && (lifetime > data.MaxLifetime))
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

            HPController t = collision.GetComponent<HPController>();
            if (!t.immune.Contains(gameObject))
                t.TakeDamage(data.damage);
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
        script.lifetime = 0;
        script.data = so;
        script.movementController.speed = so.Speed;
        script.gameObject.GetComponent<SpriteRenderer>().sprite = so.ProjectileSprite;
        return projectile;
    }
}
