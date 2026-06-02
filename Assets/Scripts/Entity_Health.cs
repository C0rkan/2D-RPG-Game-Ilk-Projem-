using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_Health : MonoBehaviour
{
    private EntityVFX entityVFX;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;


    protected virtual void Awake() {
        entityVFX = GetComponent<EntityVFX>();
    }


    public virtual void TakeDamage(float damage,Transform damageDealer) {

        if (isDead)
            return;

        entityVFX?.PlayOnDamageVfx();
        //burada kullanýlan '?' bir null check'tir. eðer boþ deðer varsa hata fýrlatmamasý için. 

        ReduceHp(damage);
    }

    protected void ReduceHp(float damage) {

        maxHp -= damage;

        if(maxHp < 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        Debug.Log("Entity died!!!");
    }
}
