using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Bounce Settings")]

    [SerializeField] private int maxBounces = 4;

    [Header("Targeting & Layers")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Despawn Settings")]
    [SerializeField] private float destroyDelayAfterStick = 2f;

    private Rigidbody2D rb;
    private int currentBounceCount = 0;
    private bool isStuck = false;

    private float upperBound = 5.91f;
    private float rightBound = 9.61f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.RegisterArrow();
        }
    }

    private void OnDestroy()
    {
        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.UnregisterArrow();
        }
    }
    private void Update()
    {
        Bound();
        if (!isStuck && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck) return;


        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            currentBounceCount++;


            if (currentBounceCount >= maxBounces)
            {
                StickToWall(collision);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStuck) return;


        if (collision.TryGetComponent<IDeath>(out IDeath ideath))
        {
            ideath.Die();
        }

        if(collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }

    private void StickToWall(Collision2D collision)
    {
        isStuck = true;


        ContactPoint2D contact = collision.GetContact(0);
        transform.position = contact.point;


        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;


        Destroy(gameObject, destroyDelayAfterStick);
    }

    private void Bound()
    {
        if((transform.position.y> upperBound) || (transform.position.y < -upperBound))
        {
            Destroy(gameObject);
        }
        if((transform.position.x>rightBound)||(transform.position.x < -rightBound))
        { Destroy(gameObject); }
               
    }
}