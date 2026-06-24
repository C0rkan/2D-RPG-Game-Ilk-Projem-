using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamagable
{
    private EntityVFX entityVFX;
    private Entity entity;
    private Slider healthBar;
    private Entity_Stats stats;

    [SerializeField] protected float currentHp;
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
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();

        currentHp = stats.GetMaxHelath();
        UpdateHealth();
    }


    public virtual bool TakeDamage(float damage,Transform damageDealer) {

        if (isDead)
            return false;

        if (AttackEvaded()) {
            Debug.Log($"{gameObject.name} evaded the attack! ");
            return false;
        }

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;


        float mitigation = stats.GetArmorMitigation(armorReduction);
        float finalDamage = damage * ( 1 - mitigation );

        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer);
        float duration = CalculationDuration(finalDamage);
        
        entity?.ReciveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVfx();
        //burada kullanýlan '?' bir null check'tir. eðer boþ deðer varsa hata fýrlatmamasý için. 

        ReduceHp(finalDamage);
        Debug.Log("Final Damage : " + finalDamage);

        return true;
    }

    private bool AttackEvaded() => UnityEngine.Random.Range(0, 100) < stats.GetEvasion();

    protected void ReduceHp(float damage) {

        currentHp -= damage;
        UpdateHealth();

        if(currentHp <= 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealth() {
        if (healthBar == null) {
            return;
        }
         
        healthBar.value = currentHp / stats.GetMaxHelath();
    }

    private Vector2 CalculateKnockback(float damage , Transform damageDealer) {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knocback = IsHeavyDamage(damage) ? heavyKnockbackPower : knocbackPower;
        knocback.x *= direction;
        
        return knocback;
    }

    private float CalculationDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knocbackDuration;

    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHelath() > heavyKnockbackThreshold;

}
