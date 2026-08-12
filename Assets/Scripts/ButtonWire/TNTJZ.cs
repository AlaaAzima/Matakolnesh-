using UnityEngine;

public class TNTJZ : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] ButtonJZ button;
    private bool hasExploded = false;
    

    private void OnEnable()
    {
        button.OnButtonClick += Explode;
    }

    private void OnDisable()
    {
        button.OnButtonClick -= Explode;
    }
    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D hit in hitObjects)
        {
            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb == null) continue;

            Vector2 direction = (rb.position - (Vector2)transform.position);
            float distance = direction.magnitude;
            direction.Normalize();

            float falloff = 1f - Mathf.Clamp01(distance / explosionRadius);
            float appliedForce = explosionForce * falloff;

            rb.AddForce(direction * appliedForce, ForceMode2D.Impulse);
        }

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

   
}