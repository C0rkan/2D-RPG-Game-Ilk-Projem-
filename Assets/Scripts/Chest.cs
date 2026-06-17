using UnityEngine;

public class Chest : MonoBehaviour, IDamagable {

    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private EntityVFX fx => GetComponentInChildren<EntityVFX>();

    [Header("Open Detalis")]
    [SerializeField] private Vector2 knockback;
    public bool TakeDamage(float damage, Transform damageDealer) {
        fx.PlayOnDamageVfx();
        anim.SetBool("openChest", true);
        rb.linearVelocity = knockback;

        rb.angularVelocity = Random.Range(-200,200);


        return true;
    }
}
