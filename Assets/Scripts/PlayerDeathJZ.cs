using UnityEngine;

public class PlayerDeathJZ : MonoBehaviour, IDeath
{
    [SerializeField] private float destroyDelay = 1.1f;

    private Animator animator;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private bool isDead = false;


    private static readonly int DieTriggerHash = Animator.StringToHash("IsDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;


        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.PlayerDied();
        }


        if (animator != null)
        {
            animator.SetTrigger(DieTriggerHash);
        }


        if (playerCollider != null) playerCollider.enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }


        Destroy(gameObject, destroyDelay);
    }
}