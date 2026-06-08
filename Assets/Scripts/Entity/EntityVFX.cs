using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour {

    private SpriteRenderer sr;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageVfxMaterial;
    [SerializeField] private float onDamageDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfx;


    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void CreateOnHitVFX(Transform target) {

        GameObject vfx = Instantiate(hitVfx, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

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
