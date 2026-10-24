using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [System.NonSerialized]
    public List<Upgrade> CurrentAbilityPool = new List<Upgrade>();
    public List<AbilitySO> abilities = new List<AbilitySO>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var ability in GlobalDataSO.Instance.AbilitiesStarterUpgradePool)
        {
            CurrentAbilityPool.Add(new Upgrade(null, ability));
        }

        foreach (var ability in abilities)
        {
            ability.Run();
            int i = CurrentAbilityPool.FindIndex(x=>x.newAbility == ability);
            if (i >= 0)
            {
                CurrentAbilityPool.RemoveAt(i);
            }
            foreach (var a in ability.nextLevels)
                CurrentAbilityPool.Add(new Upgrade(ability, a));
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
        if (abilities.Contains(ability))
        {
            ability.Stop();
            abilities.Remove(ability);
        }
    }

    private void OnDestroy()
    {
        foreach (var ability in abilities)
        {
            ability.Stop();
        }
    }
}
