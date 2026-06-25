using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
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

    public float GetElementalDamage() {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        float bonusElemantalDamage = major.intelligence.GetValue();

        float highestDamage = fireDamage;

        if(iceDamage > highestDamage)
            highestDamage = iceDamage;
        
        if(lightningDamage > highestDamage)
            highestDamage = lightningDamage;

        
        if(highestDamage <= 0)
            return 0;

        float bonusFireDamage = (fireDamage == highestDamage)? 0 : fireDamage * .5f;
        float bonusIceDaamge = (iceDamage == highestDamage) ? 0 : iceDamage * .5f;
        float bonusLightningDamage = (lightningDamage == highestDamage) ? 0 : lightningDamage * .5f;

        float weakerElementsDamage = bonusFireDamage + bonusIceDaamge + bonusLightningDamage;

        float finalElementalDamage = highestDamage + weakerElementsDamage + bonusElemantalDamage;

        return finalElementalDamage;
    }

    public float GetArmorMitigation( float armorReduction ) {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); // Every vit point gives 1 armor
        float totalArmor = baseArmor + bonusArmor;
                                 
        float reductionMultipiler = Mathf.Clamp(1 - armorReduction,0,1);
        float effectiveArmor = totalArmor * reductionMultipiler;

        float mitigation = effectiveArmor / (effectiveArmor+ 100); //eg. if u have 150 armor -> 150 / (150 +100) = .6f and thats mean u'll get .4f of damage 
        float mitigationCap = .85f;

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);
        
        return finalMitigation;
    }

    public float GetArmorReduction() {

        float finalReduction = offense.armorReduction.GetValue() / 100;

        return finalReduction;
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
