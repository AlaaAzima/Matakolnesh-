using UnityEngine;

public class ArrowFadeInIntro : MonoBehaviour
{
    [Header("Fade In Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutSine;
    [SerializeField] private float startAlpha = 0f;
    [SerializeField] private float endAlpha = 1f;

    private SpriteRenderer sr;
    private int tweenId = -1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // snap to invisible, then fade up once
        Color c = sr.color;
        c.a = startAlpha;
        sr.color = c;

        tweenId = LeanTween.alpha(gameObject, endAlpha, fadeDuration)
            .setEase(easeType)
            .id;
    }

    private void OnDisable()
    {
        if (LeanTween.isTweening(tweenId))
            LeanTween.cancel(tweenId);
    }
}