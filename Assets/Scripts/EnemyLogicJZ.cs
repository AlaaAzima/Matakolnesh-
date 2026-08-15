/*
// =================================================================================
// DEPRECATED SCRIPT
// =================================================================================
// This script violated the Single Responsibility Principle (SRP) by handling health,
// physics, visuals, audio, and GameManager communication all at once.
//
// It has now been split into 3 separate scripts:
// 1. EnemyHealth.cs
// 2. EnemyEffects.cs
// 3. EnemyController.cs
//
// Please remove this script from your Enemy Prefab and attach the 3 new scripts instead.
// =================================================================================

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
*/