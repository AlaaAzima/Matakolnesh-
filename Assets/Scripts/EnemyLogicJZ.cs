using UnityEngine;

public class EnemyLogicJZ : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    public void Die()
    {
        GameManagerJE.Instance.PlayerDied();
        Destroy(gameObject);
    }
}
