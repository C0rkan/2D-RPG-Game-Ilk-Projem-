 using System.Diagnostics.Contracts;
using UnityEngine;

public class Entity_Combat : MonoBehaviour {

    private EntityVFX vfx;
    private Entity_Stats stats;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;


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
             
            float damage = stats.GetPhyiscalDamage(out bool isCrit);
            bool targetGotHit = damagable.TakeDamage(damage, transform);                //same as    if(targetHelath != null) 
            
            if (targetGotHit) {
                vfx.CreateOnHitVFX(target.transform,isCrit);                                //              tagetHealth.TakeDamage(daamge);
            }


        }

    }

    protected Collider2D[] GetDetectedColliders() {

        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadius,whatIsTarget);
    } 

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
