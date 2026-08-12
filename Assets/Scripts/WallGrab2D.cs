using UnityEngine;

public class WallGrab2D : MonoBehaviour
{
    [Header("Wall Settings")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float highGravity = 5f;
    [SerializeField] private Transform checkPoint; // نقطة تحت رجل/جسم الأنمي
    [SerializeField] private float checkRadius = 0.2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // فحص مبدئي أول ما اللعبة تفتح: هل الأنمي لمس الحيطة وهو بيبدأ؟
        if (IsTouchingWall())
        {
            StickToWall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsWallLayer(collision.gameObject.layer))
        {
            StickToWall();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsWallLayer(collision.gameObject.layer))
        {
            // رجّع الجاذبية لما يبعد عن الحيطة
            rb.gravityScale = highGravity;
        }
    }

    private void StickToWall()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero; // إلغاء أي حركة راسية أو افقية متراكمة
    }

    private bool IsTouchingWall()
    {
        // لو مش حاطة CheckPoint هيستخدم موقع الأنمي نفسه
        Vector3 pos = checkPoint != null ? checkPoint.position : transform.position;
        return Physics2D.OverlapCircle(pos, checkRadius, wallLayer);
    }

    private bool IsWallLayer(int layer)
    {
        return ((1 << layer) & wallLayer) != 0;
    }

    // لرسم دائرة الفحص في الـ Scene عشان تشوفي مقاسها
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = checkPoint != null ? checkPoint.position : transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, checkRadius);
    }
}