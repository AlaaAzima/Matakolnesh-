using UnityEngine;

public class BoundJZ : MonoBehaviour
{
     float upperBound = 5.61f;
     float rightBound = 9.32f;

    private void Update()
    {
        Bound();
    }
    private void Bound()
    {
        if ((transform.position.y > upperBound) || (transform.position.y < -upperBound))
        {
            Destroy(gameObject);
        }
        if ((transform.position.x > rightBound) || (transform.position.x < -rightBound))
        { Destroy(gameObject); }

    }
}
