using UnityEngine;

public class PushedBall : MonoBehaviour
{
    private bool isStuck = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        
        if (isStuck) return;
        if (collision.collider.TryGetComponent<IDeath>(out IDeath ideath))
        {
            Debug.Log("IDeath found!");
            ideath.Die();
        }
    }

}
