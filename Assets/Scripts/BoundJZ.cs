using UnityEngine;

public class BoundJZ : MonoBehaviour
{
    [SerializeField] float upperBound;
    [SerializeField] float rightBound;

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
