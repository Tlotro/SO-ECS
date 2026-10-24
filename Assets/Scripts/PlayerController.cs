using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public MovementController movementController;
    public HPController hpController;
    public InputActionReference move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalDataSO.Instance.Reset();
        GlobalDataSO.Instance.player = this.gameObject;
        hpController.HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
       movementController.Direction = move.action.ReadValue<Vector2>();
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
