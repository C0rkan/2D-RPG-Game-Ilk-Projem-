using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour {

    private SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageVfxMaterial;
    [SerializeField] private float onDamageDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;


    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void PlayOnDamageVfx() {

        if (onDamageVfxCoroutine != null) {
            StopCoroutine(onDamageVfxCoroutine);
        }

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo() {
        sr.material = onDamageVfxMaterial;

        yield return new WaitForSeconds(onDamageDuration);
        sr.material = originalMaterial;
    }

}
