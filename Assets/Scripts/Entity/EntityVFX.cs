using System.Collections;
using TMPro.EditorUtilities;
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

    [Header("Element Colors")]
    [SerializeField] private Color chillVfx = Color.cyan;
    private Color originalHitVfxColor;

    private void Awake() {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        originalHitVfxColor = hitVfxColor;
    }

    public void PlayOnStatusVfx(float duration, ElementalType element) {
        if (element == ElementalType.Ice) {
            StartCoroutine(PlayStatusVfxCo(duration, chillVfx));
        }
    }

    private IEnumerator PlayStatusVfxCo(float duration, Color effectColor) {

        float tickInterval = .25f;
        float timeHasPassed = 0;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * .8f;

        bool toggle = false;

        while (timeHasPassed < duration) {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timeHasPassed += tickInterval;
        }

        sr.color = Color.white;
    }

    public void CreateOnHitVFX(Transform target,bool isCrit) {

        GameObject hitPrefab = isCrit ? critHitVfx : hitVfx;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

        if (entity.facingDir == -1 && isCrit) {
            vfx.transform.Rotate(0, 180, 0);
        }
    }

    public void UpdateOnHitColor(ElementalType element) {
        if (element == ElementalType.Ice) {
            hitVfxColor = chillVfx;
        }

        if (element == ElementalType.None) {
            hitVfxColor = originalHitVfxColor;
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
