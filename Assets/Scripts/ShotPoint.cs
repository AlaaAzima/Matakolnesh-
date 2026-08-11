using UnityEngine;

public class ShotPoint : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform playerTransform; // اسحبي مجسم الـ Player الرئيسي هنا

    [Header("Local Positions")]
    [Tooltip("مكان الـ ShotPoint لما الماوس يكون يمين")]
    [SerializeField] private Vector3 rightOffset = new Vector3(0.5f, 0.1f, 0f);

    [Tooltip("مكان الـ ShotPoint لما الماوس يكون شمال")]
    [SerializeField] private Vector3 leftOffset = new Vector3(-0.5f, 0.1f, 0f);

    private Camera mainCamera;
    private bool isMouseOnLeft = false;

    private void Awake()
    {
        mainCamera = Camera.main;

        // لو ما سحبتيش الـ Player في الـ Inspector بياخد الـ Parent تلقائياً
        if (playerTransform == null && transform.parent != null)
        {
            playerTransform = transform.parent;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // 1. حساب موضع الماوس بالنسبة للاعب
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bool currentlyOnLeft = mouseWorldPos.x < playerTransform.position.x;

        // 2. تحديث الموضع فقط عند تغيير الاتجاه
        if (currentlyOnLeft != isMouseOnLeft)
        {
            isMouseOnLeft = currentlyOnLeft;
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        // تغيير الـ Local Position للـ ShotPoint
        transform.localPosition = isMouseOnLeft ? leftOffset : rightOffset;
    }
}
