using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementalType currentEffect = ElementalType.None;
    private EntityVFX entityVfx;
    private Entity_Stats stats;
    private Entity entity;
    private Entity_Health entityHealth;

    private void Awake() {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<EntityVFX>();
        stats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public void ApplyBurnEffect(float duration, float fireDamage) {

        float fireResistance = stats.GetElementalResitance(ElementalType.Fire);
        float finalDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    public IEnumerator BurnEffectCo(float duration, float totalDamage) {
        currentEffect = ElementalType.Fire;
        entityVfx.PlayOnStatusVfx(duration,ElementalType.Fire);

        int ticksPerSecond = 2;
        int ticksCount = Mathf.RoundToInt(ticksPerSecond * duration);
        
        float damagePerTick = totalDamage / ticksCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < ticksCount; i++) {
            entityHealth.ReduceHp(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementalType.None;
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier) {
        float iceResistance = stats.GetElementalResitance(ElementalType.Ice);
        float reduceDuration = duration * (1 - iceResistance);
        
        StartCoroutine(ChilledEffectCo(reduceDuration,slowMultiplier));
    }

    public IEnumerator ChilledEffectCo(float duration, float slowMultiplier) {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementalType.Ice;
        entityVfx.PlayOnStatusVfx(duration,ElementalType.Ice);

        yield return new WaitForSeconds(duration);

        //stop vfx
        currentEffect = ElementalType.None;
    }

    public bool CanBeApplied(ElementalType element) {
        return currentEffect == ElementalType.None;
    }

}
