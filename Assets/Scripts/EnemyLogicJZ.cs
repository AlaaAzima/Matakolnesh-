using UnityEngine;

public class EnemyLogicJZ : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Die();
        }
    }

    public void Die()
    {
        GameManagerJE.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}