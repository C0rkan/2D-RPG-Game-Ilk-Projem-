using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_Health : MonoBehaviour
{
    private EntityVFX entityVFX;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knocbackPower = new Vector2(1.5f , 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float knocbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyKnockbackThreshold = .3f;

    protected virtual void Awake() {
        entityVFX = GetComponent<EntityVFX>();
        entity = GetComponent<Entity>();

        currentHp = maxHp;
    }


    public virtual void TakeDamage(float damage,Transform damageDealer) {

        if (isDead)
            return;

        Vector2 knockback = CalculateKnockback(damage,damageDealer);
        float duration = CalculationDuration(damage);

        entity?.ReciveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVfx();
        //burada kullanýlan '?' bir null check'tir. eðer boþ deðer varsa hata fýrlatmamasý için. 

        ReduceHp(damage);
    }

    protected void ReduceHp(float damage) {

        currentHp -= damage;

        if(currentHp < 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        Debug.Log("Entity died!!!");
    }

    private Vector2 CalculateKnockback(float damage , Transform damageDealer) {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knocback = IsHeavyDamage(damage) ? heavyKnockbackPower : knocbackPower;
        knocback.x *= direction;
        
        return knocback;
    }

    private float CalculationDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knocbackDuration;

    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyKnockbackThreshold;

}
