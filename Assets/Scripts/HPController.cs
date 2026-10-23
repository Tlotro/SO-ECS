using UnityEngine;
using UnityEngine.Events;

public class HPController : MonoBehaviour
{
    [System.NonSerialized]
    public float HP;
    public UnityEvent Kill;
    public UnityEvent<float> Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        float oldHP = HP;
        HP -= damage;
        Damage.Invoke(damage);
        float trueDamage = oldHP - HP;
        if (HP <= 0)
        {
            Kill.Invoke();
        }
    }
}
