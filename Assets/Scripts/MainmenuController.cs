using UnityEngine;
using System.Collections;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    [Header("UI Buttons")]
    public RectTransform leftButton;
    public RectTransform rightButton;

    [Header("Animation Settings")]
    public float duration = 0.8f;
    public float offscreenOffset = 1000f;

    private Vector2 leftTargetPos;
    private Vector2 rightTargetPos;

    void Start()
    {

        leftTargetPos = leftButton.anchoredPosition;
        rightTargetPos = rightButton.anchoredPosition;


        leftButton.anchoredPosition = new Vector2(leftTargetPos.x - offscreenOffset, leftTargetPos.y);
        rightButton.anchoredPosition = new Vector2(rightTargetPos.x + offscreenOffset, rightTargetPos.y);


        AnimateButtonsIn();
    }

    public void AnimateButtonsIn()
    {

        StartCoroutine(AnimateAnchoredPosition(leftButton, leftTargetPos, duration));
        StartCoroutine(AnimateAnchoredPosition(rightButton, rightTargetPos, duration));
    }

    private IEnumerator AnimateAnchoredPosition(RectTransform rt, Vector2 target, float time)
    {
        Vector2 start = rt.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            // Ease.OutBack formula
            float s = 1.70158f;
            float t1 = t - 1f;
            float ease = 1f + (t1 * t1 * ((s + 1f) * t1 + s));
            rt.anchoredPosition = Vector2.LerpUnclamped(start, target, ease);
            yield return null;
        }
        rt.anchoredPosition = target;
    }
}