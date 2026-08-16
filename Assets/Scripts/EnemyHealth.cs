using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDeath
{
    public event Action OnDeathEvent;
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        OnDeathEvent?.Invoke();
        GameEvents.TriggerPlayVFX(VFXType.EnemyDeath, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Salsa"))
        {
            Die();
        }
    }
}
