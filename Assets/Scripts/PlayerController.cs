using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public MovementController movementController;
    public HPController hpController;
    public InputActionReference move;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalDataSO.Instance.Reset();
        GlobalDataSO.Instance.player = this.gameObject;
        hpController.HP = 100;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movementController.Direction = move.action.ReadValue<Vector2>();
        if (movementController.Direction.x != 0) spriteRenderer.flipX = movementController.Direction.x > 0;
    }

    public void Kill()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.gameObject.layer == 7)
        {
            hpController.TakeDamage(collision.gameObject.GetComponent<Enemy>().data.damage);
            collision.gameObject.GetComponent<HPController>().Kill.Invoke();
        }
    }
}
