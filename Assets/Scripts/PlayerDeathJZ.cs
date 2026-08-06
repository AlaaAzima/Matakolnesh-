using UnityEngine;

public class PlayerDeathJZ : MonoBehaviour,IDeath
{
    bool isDead = false;
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        GameManagerJE.Instance.PlayerDied();
        Destroy(gameObject);

    }
}
