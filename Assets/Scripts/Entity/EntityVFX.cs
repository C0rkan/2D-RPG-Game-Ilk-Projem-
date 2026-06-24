using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class EntityVFX : MonoBehaviour {

    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageVfxMaterial;
    [SerializeField] private float onDamageDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfx;
    [SerializeField] private GameObject critHitVfx;


    private void Awake() {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void CreateOnHitVFX(Transform target,bool isCrit) {

        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

        if (entity.facingDir == -1 && isCrit) {
            vfx.transform.Rotate(0, 180, 0);
        }
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
