using System.Diagnostics.Contracts;
using UnityEngine;

public class Entity_Combat : MonoBehaviour {


    public float damage = 10;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack() {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders()) {
            Entity_Health tagetHealth = target.GetComponent<Entity_Health>();

            tagetHealth?.TakeDamage(damage,transform);              //same as    if(targetHelath != null) 
                                                                    //              tagetHealth.TakeDamage(daamge);

        }

    }

    private Collider2D[] GetDetectedColliders() {

        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadius,whatIsTarget);
    } 

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
