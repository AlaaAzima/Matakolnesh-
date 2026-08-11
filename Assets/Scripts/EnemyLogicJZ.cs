using UnityEngine;

public class EnemyLogicJZ : MonoBehaviour, IDeath
{
    [SerializeField] private float destroyDelay = 1.1f;

    private Animator animator;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private bool isDead = false;

    private float upperBound = 5.91f;
    private float rightBound = 9.61f;

    private static readonly int DieTriggerHash = Animator.StringToHash("Death");
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Bound();
    }

    public void Die()
    {
        //GameManagerJE.Instance.EnemyKilled();
        //animator.SetTrigger("Death");
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


        Destroy(gameObject, destroyDelay);
    }

    private void Bound()
    {
        if ((transform.position.y > upperBound) || (transform.position.y < -upperBound))
        {
            Destroy(gameObject);
        }
        if ((transform.position.x > rightBound) || (transform.position.x < -rightBound))
        { Destroy(gameObject); }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Salsa"))
        {
            Die();
        }
    }
}