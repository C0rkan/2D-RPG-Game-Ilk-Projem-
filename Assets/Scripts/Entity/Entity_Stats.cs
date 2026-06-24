using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefansiveGroup defense;

    public float GetPhyiscalDamage(out bool isCrit) {

        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f;  // Every agility point incrases critchance + 0.3.
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f;  // Every str point incrases critpower + 0.5.
        float critPower = (baseCritPower + baseCritPower) /100 ; // We gonna use critpower as multiplier (eg. 150 / 100 = 1.5f )

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;

    }


    public float GetMaxHelath() {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;
        float totalMaxHelath = baseHp + bonusHp;
        return totalMaxHelath;
    }

    public float GetEvasion() {
        
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

}
