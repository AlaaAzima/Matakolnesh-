using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ButtonAnimaJE : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float clickScale = 0.9f;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private bool ignoreTimeScale = true;

    private Vector3 originalScale;
    private Button button; 
    private bool isPointerInside;

    private void Awake()
    {
        originalScale = transform.localScale;
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;
        isPointerInside = false;
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = originalScale;
    }

    private bool IsInteractable()
    {
        return button == null || button.interactable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        if (!IsInteractable()) return;
        AnimateTo(originalScale * hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
        AnimateTo(originalScale);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable()) return;
        AnimateTo(originalScale * clickScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable()) return;
        AnimateTo(isPointerInside ? originalScale * hoverScale : originalScale);
    }

    private void AnimateTo(Vector3 targetScale)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, targetScale, duration)
            .setEase(LeanTweenType.easeOutBack)
            .setIgnoreTimeScale(ignoreTimeScale);
    }
}