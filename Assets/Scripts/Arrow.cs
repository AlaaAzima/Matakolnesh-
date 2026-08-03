using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDeath ideath= collision.GetComponent<IDeath>();
        if (ideath !=null)
        {
            ideath.Die();
           
        }
    }
}
