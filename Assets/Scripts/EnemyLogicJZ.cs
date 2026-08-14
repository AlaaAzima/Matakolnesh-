using UnityEngine;

public class EnemyLogicJZ : MonoBehaviour, IDeath
{
    [SerializeField] private float destroyDelay = 1.1f;

    private Animator animator;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private bool isDead = false;

    

    private static readonly int DieTriggerHash = Animator.StringToHash("Death");
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
   

    public void Die()
    {
        if (isDead) return;
        isDead = true;


        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.EnemyKilled();
        }


        if (animator != null)
        {
            animator.SetTrigger(DieTriggerHash);
        }


        if (enemyCollider != null) enemyCollider.enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }


        SoundManager.PlaySound(SoundType.EnemyDeath);
        Destroy(gameObject, destroyDelay);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Salsa"))
        {
            Die();
        }
    }
}