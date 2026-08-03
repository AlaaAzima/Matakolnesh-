using UnityEngine;

public class PlayerDeathJZ : MonoBehaviour,IDeath
{
    public void Die()
    {
        GameManagerJE.Instance.PlayerDied();
        Destroy(gameObject);

    }
}
