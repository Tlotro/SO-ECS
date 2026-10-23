using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public List<AbilitySO> abilities = new List<AbilitySO>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var ability in abilities)
        {
            ability.Run();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAbility(AbilitySO ability)
    {
        abilities.Add(ability);
        ability.Run();
    }

    public void RemoveAbility(AbilitySO ability)
    {
        ability.Stop();
        abilities.Remove(ability);
    }

    public void Upgrade(AbilitySO ability)
    {
        if (ability.nextLevel != null)
        {
            RemoveAbility(ability);
            AddAbility(ability.nextLevel);
        }
    }
}
