using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPController : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    public float HP {  get { return _hp; } set { _hp = value; HealthChange.Invoke(_hp, _maxhp); } }
    [SerializeField]
    private float _maxhp;
    public float MAXHP { get { return _maxhp; } set { _maxhp = value; HealthChange.Invoke(_hp, _maxhp); } }
    public List<GameObject> immune = new List<GameObject>();
    public UnityEvent Kill;
    public UnityEvent<float> Damage;
    public UnityEvent<float> Healing;
    public UnityEvent<float, float> HealthChange;
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

    public void Heal(float healing)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + healing, MAXHP);
        Healing.Invoke(healing);
        float trueHealing = HP - oldHP;
    }
}
