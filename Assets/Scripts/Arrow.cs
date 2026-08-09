using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float stickCheckRadius = 5f;

    private Rigidbody2D rb;
    private bool isStuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(StickToNearestWallRoutine());
    }

    void Update()
    {

        if (!isStuck && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private IEnumerator StickToNearestWallRoutine()
    {

        yield return new WaitForSeconds(lifetime);

        if (!isStuck)
        {
            StickToWall();
        }
    }

    private void StickToWall()
    {

        Collider2D wallCollider = Physics2D.OverlapCircle(transform.position, stickCheckRadius, wallLayer);

        if (wallCollider != null)
        {

            Vector2 closestPoint = wallCollider.ClosestPoint(transform.position);


            transform.position = closestPoint;


            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;

            isStuck = true;


            Destroy(gameObject, 2f);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isStuck) return;

        IDeath ideath = collision.GetComponent<IDeath>();
        if (ideath != null)
        {
            ideath.Die();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stickCheckRadius);
    }
}