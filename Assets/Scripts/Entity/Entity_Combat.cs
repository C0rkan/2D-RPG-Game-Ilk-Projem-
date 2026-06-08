 using System.Diagnostics.Contracts;
using UnityEngine;

public class Entity_Combat : MonoBehaviour {

    private EntityVFX vfx;
    public float damage = 10;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;


    private void Awake() {
        vfx = GetComponent<EntityVFX>();
    }


    public void PerformAttack() {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders()) {
            IDamagable damagable = target.GetComponent<IDamagable>();
            
            if (damagable == null) {
                continue;
            }
            
            damagable.TakeDamage(damage, transform);                //same as    if(targetHelath != null) 
            vfx.CreateOnHitVFX(target.transform);                                //              tagetHealth.TakeDamage(daamge);

        }

    }

    protected Collider2D[] GetDetectedColliders() {

        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadius,whatIsTarget);
    } 

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
