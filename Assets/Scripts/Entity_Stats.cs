using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat vitality;

    public float GetMaxHelath() {
        float baseHp = maxHealth.GetValue();
        float bonusHp = vitality.GetValue() * 5;

        return baseHp + bonusHp;
    }
    
}
