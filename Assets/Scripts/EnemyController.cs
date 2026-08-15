using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1.1f;
    
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private EnemyHealth health;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        health.OnDeathEvent += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDeathEvent -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.EnemyKilled();
        }

        if (enemyCollider != null) enemyCollider.enabled = false;
        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Destroy(gameObject, destroyDelay);
    }
}
