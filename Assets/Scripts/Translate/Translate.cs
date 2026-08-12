using UnityEngine;

public class Translate : MonoBehaviour
{
    private enum Axis { Horizontal, Vertical }

    [SerializeField] private Axis moveAxis = Axis.Horizontal;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minLimit = -5f; // Left if Horizontal, Down if Vertical
    [SerializeField] private float maxLimit = -1f; // Right if Horizontal, Up if Vertical

    private float dir = 1f;

    private void Update()
    {
        if (moveAxis == Axis.Horizontal)
        {
            transform.position += Vector3.right * speed * Time.deltaTime * dir;

            if (transform.position.x >= maxLimit)
            {
                transform.position = new Vector3(maxLimit, transform.position.y, transform.position.z);
                dir = -1f;
            }
            else if (transform.position.x <= minLimit)
            {
                transform.position = new Vector3(minLimit, transform.position.y, transform.position.z);
                dir = 1f;
            }
        }
        else // Vertical
        {
            transform.position += Vector3.up * speed * Time.deltaTime * dir;

            if (transform.position.y >= maxLimit)
            {
                transform.position = new Vector3(transform.position.x, maxLimit, transform.position.z);
                dir = -1f;
            }
            else if (transform.position.y <= minLimit)
            {
                transform.position = new Vector3(transform.position.x, minLimit, transform.position.z);
                dir = 1f;
            }
        }
    }
}