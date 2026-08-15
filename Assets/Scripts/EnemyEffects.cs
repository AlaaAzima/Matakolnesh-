using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyEffects : MonoBehaviour
{
    private Animator animator;
    private EnemyHealth health;
    private static readonly int DieTriggerHash = Animator.StringToHash("Death");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        health.OnDeathEvent += PlayDeathEffects;
    }

    private void OnDisable()
    {
        health.OnDeathEvent -= PlayDeathEffects;
    }

    private void PlayDeathEffects()
    {
        if (animator != null)
        {
            animator.SetTrigger(DieTriggerHash);
        }
        
        SoundManager.PlaySound(SoundType.EnemyDeath);
    }
}
