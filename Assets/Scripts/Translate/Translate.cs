
using UnityEngine;

public class Translate : MonoBehaviour
{
   
    [SerializeField] float speed = 2;
    [SerializeField] float dir =1;


    void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime * dir);

        if (transform.position.x >= -1)
        {
            
           transform.position = new Vector3(-1, transform.position.y, transform.position.z);
           dir = -1;
        }
        else if(transform.position.x <= -5)
        {
             transform.position = new Vector3(-5, transform.position.y, transform.position.z);
            dir =1;
        }
    }

}
