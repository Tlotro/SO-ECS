using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class XPController : MonoBehaviour
{
    int level = 0;
    int xp = 0;
    public int xpBase;
    public int xpInc;
    public AbilityController abilityController;
    public UnityEvent<float> XPGain;
    public UnityEvent<float, float> XPChange;
    public UnityEvent<string[]> LevelUp;
    private Upgrade[] currentUpgrades = new Upgrade[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddXP(int xp)
    {
        this.xp += xp;
        XPGain.Invoke(xp);
        XPChange.Invoke(this.xp, xpBase + xpInc * level);
        if (this.xp>=xpBase+xpInc*level)
        {
            this.xp = this.xp - (xpBase + xpInc * level);
            level++;
            string[] choice = new string[3];
            if (abilityController.CurrentAbilityPool.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Upgrade upgrade = abilityController.CurrentAbilityPool[UnityEngine.Random.Range(0, abilityController.CurrentAbilityPool.Count)];
                    Debug.Log(upgrade);
                    currentUpgrades[i] = upgrade;
                    choice[i] = upgrade.newAbility.description;
                }
                LevelUp.Invoke(choice);
            }
        }
    }
    
    public void applyUpgrade(int index)
    {
        if (currentUpgrades[index].oldAbility != null)
            abilityController.RemoveAbility(currentUpgrades[index].oldAbility);
        abilityController.AddAbility(currentUpgrades[index].newAbility);
        abilityController.CurrentAbilityPool.Remove(currentUpgrades[index]);
        foreach (var a in currentUpgrades[index].newAbility.nextLevels)
            abilityController.CurrentAbilityPool.Add(new Upgrade(currentUpgrades[index].newAbility, a));
    }
}
public struct Upgrade
{
    public AbilitySO oldAbility;
    public AbilitySO newAbility;

    public Upgrade(AbilitySO oldAbility, AbilitySO newAbility)
    {
        this.oldAbility = oldAbility;
        this.newAbility = newAbility;
    }
}

