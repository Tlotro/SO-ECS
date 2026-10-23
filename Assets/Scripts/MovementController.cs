using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    public float speed;
    [System.NonSerialized]
    public Vector2 Direction; 
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.linearVelocity = Direction.normalized * speed;
    }
}
