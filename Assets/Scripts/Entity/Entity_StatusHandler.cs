using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementalType currentEffect = ElementalType.None;
    private EntityVFX entityVfx;
    private Entity_Stats stats;
    private Entity entity;

    private void Awake() {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<EntityVFX>();
        stats = GetComponent<Entity_Stats>();
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
