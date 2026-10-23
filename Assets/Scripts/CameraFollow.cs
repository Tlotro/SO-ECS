using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public InputActionReference mouseAction;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseAction.action.ReadValue<Vector2>().x, mouseAction.action.ReadValue<Vector2>().y));
        transform.position = Vector3.Lerp(transform.position, (target.transform.position + (mousePos - target.transform.position) * 0.2f), 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
