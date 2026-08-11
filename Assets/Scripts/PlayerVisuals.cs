using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [Header("Visual Container or Renderers")]
    [Header("Visual GameObjects")]
    [SerializeField] private GameObject playerRight;
    [SerializeField] private GameObject playerLeft;

    private Camera mainCamera;
    private bool isFacingLeft = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        ApplyVisualState();
    }

    private void Update()
    {
        HandleFacingDirection();
    }

    private void HandleFacingDirection()
    {

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bool mouseIsOnLeft = mouseWorldPos.x < transform.position.x;

        if (mouseIsOnLeft != isFacingLeft)
        {
            isFacingLeft = mouseIsOnLeft;
            ApplyVisualState();
        }
    }

    private void ApplyVisualState()
    {
        if (playerRight != null && playerLeft != null)
        {
            playerRight.SetActive(!isFacingLeft);
            playerLeft.SetActive(isFacingLeft);
        }
    }
}