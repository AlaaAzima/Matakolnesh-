using UnityEngine;

public class ShotPoint : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform playerTransform;

    [Header("Local Positions")]

    [SerializeField] private Vector3 rightOffset = new Vector3(0.5f, 0.1f, 0f);


    [SerializeField] private Vector3 leftOffset = new Vector3(-0.5f, 0.1f, 0f);

    private Camera mainCamera;
    private bool isMouseOnLeft = false;

    private void Awake()
    {
        mainCamera = Camera.main;


        if (playerTransform == null && transform.parent != null)
        {
            playerTransform = transform.parent;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;


        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bool currentlyOnLeft = mouseWorldPos.x < playerTransform.position.x;


        if (currentlyOnLeft != isMouseOnLeft)
        {
            isMouseOnLeft = currentlyOnLeft;
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {

        transform.localPosition = isMouseOnLeft ? leftOffset : rightOffset;
    }
}
