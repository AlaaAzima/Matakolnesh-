using UnityEngine;

public class RotatingBar : MonoBehaviour
{
    [SerializeField] private float speed ;


    void Update()
    {
        transform.Rotate(0,0,45 * speed * Time.deltaTime);
    }
}
