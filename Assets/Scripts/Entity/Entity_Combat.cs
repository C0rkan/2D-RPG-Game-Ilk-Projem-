 using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

public class Entity_Combat : MonoBehaviour {

    private EntityVFX vfx;
    private Entity_Stats stats;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status Effect details")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chillSlowMultiplier = .2f;
    [SerializeField] private float electrifyChargeBuildUp = .4f;

    private void Awake() {
        vfx = GetComponent<EntityVFX>();
        stats = GetComponent<Entity_Stats>();
    }


    public void PerformAttack() {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders()) {
            IDamagable damagable = target.GetComponent<IDamagable>();
            
            if (damagable == null) {
                continue;
            }
            float elementalDamage = stats.GetElementalDamage(out ElementalType element, .6f);
            float damage = stats.GetPhyiscalDamage(out bool isCrit);
            bool targetGotHit = damagable.TakeDamage(damage ,elementalDamage ,element ,transform);                //same as    if(targetHelath != null) 

            if (element != ElementalType.None) {
                ApplyStatusEffect(target.transform, element);
            }

            if (targetGotHit) {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform,isCrit);                                //              tagetHealth.TakeDamage(daamge);
            }


        }

    }

    private void ApplyStatusEffect(Transform target, ElementalType element, float scaleFactor = 1) {

        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if (statusHandler == null){
            return;
        }

        if (element == ElementalType.Ice && statusHandler.CanBeApplied(ElementalType.Ice)) {
            statusHandler.ApplyChilledEffect(defaultDuration, chillSlowMultiplier);
        }
        if (element == ElementalType.Fire && statusHandler.CanBeApplied(ElementalType.Fire)) {
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnEffect(defaultDuration, fireDamage); 
        }
        if (element == ElementalType.Lightnig && statusHandler.CanBeApplied(ElementalType.Lightnig)) {
            float lightningDamage = stats.offense.lightningDamage.GetValue() * scaleFactor;
            statusHandler.ApplyElectrifyEffect(defaultDuration,lightningDamage,electrifyChargeBuildUp);
        }
    }


    protected Collider2D[] GetDetectedColliders() {

        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadius,whatIsTarget);
    } 

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
