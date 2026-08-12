using UnityEngine;

public class PushedBall : MonoBehaviour
{
    private bool isStuck = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck) return;
        if (collision.collider.TryGetComponent<IDeath>(out IDeath ideath))
        {
            ideath.Die();
        }
    }





}
