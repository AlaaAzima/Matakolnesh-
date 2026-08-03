using UnityEngine;

public class EnemyLogicJZ : MonoBehaviour, IDeath
{
    public void Die()
    {
        GameManagerJE.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}