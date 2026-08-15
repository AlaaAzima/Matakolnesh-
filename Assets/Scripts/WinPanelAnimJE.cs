using UnityEngine;

public class WinPanelAnimJE : MonoBehaviour
{
    [SerializeField] private RectTransform[] stars;

    [Header("Star Animation Settings")]
    [SerializeField] private float emptyStarScale = 0.5f;      // placeholder scale for unearned stars
    [SerializeField] private float earnedStarPopScale = 0.6f;  // overshoot scale on pop-in
    [SerializeField] private float earnedStarSettleScale = 0.5f;
    [SerializeField] private float starPopDuration = 0.2f;
    [SerializeField] private float starSettleDuration = 0.1f;
    [SerializeField] private float starDelayStep = 0.15f;

    private RectTransform winPanel;

    private void Awake()
    {
        winPanel = GetComponent<RectTransform>();
    }

    public void PlayAnimation(int earnedStars)
    {
       
        LeanTween.cancel(winPanel.gameObject);
        foreach (var star in stars)
        {
            if (star != null) LeanTween.cancel(star.gameObject);
        }

        winPanel.localScale = Vector3.zero;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].localScale = (i < earnedStars)
                ? Vector3.zero
                : Vector3.one * emptyStarScale;
        }

        LeanTween.scale(winPanel, Vector3.one, 0.5f)
            .setIgnoreTimeScale(true)
            .setEaseOutBack()
            .setOnComplete(() => PlayStarsAnimation(earnedStars));
    }

    private void PlayStarsAnimation(int earnedStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i >= earnedStars) continue; 

            int index = i;

            LeanTween.scale(stars[index].gameObject, Vector3.one * earnedStarPopScale, starPopDuration)
                .setDelay(index * starDelayStep)
                .setIgnoreTimeScale(true)
                .setEaseOutQuad()
                .setOnComplete(() =>
                {
                    LeanTween.scale(stars[index].gameObject, Vector3.one * earnedStarSettleScale, starSettleDuration)
                        .setIgnoreTimeScale(true)
                        .setEaseOutQuad();
                });
        }
    }
}

