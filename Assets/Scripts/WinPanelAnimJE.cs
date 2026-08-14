using UnityEngine;

public class WinPanelAnimJE : MonoBehaviour
{
       [SerializeField] private RectTransform[] stars;

    private RectTransform winPanel;

    private void Awake()

    {

        winPanel = GetComponent<RectTransform>();

    }

private void PlayStarsAnimation(int earnedStars)
{
    for (int i = 0; i < stars.Length; i++)
    {
        int index = i;

        // كل النجوم تبدأ من صفر
        stars[index].localScale = Vector3.zero;

        if (index < earnedStars)
        {
            LeanTween.scale(
                stars[index].gameObject,
                Vector3.one * 0.6f,
                0.2f
            )
            .setDelay(index * 0.15f)
            .setIgnoreTimeScale(true)
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(
                    stars[index].gameObject,
                    Vector3.one * 0.5f,
                    0.1f
                )
                .setIgnoreTimeScale(true)
                .setEaseOutQuad();
            });
        }
        else
        {
            // النجوم غير المكتسبة تظهر كـ outline
            stars[index].localScale = Vector3.one * 0.5f;
        }
    }
}

    public void PlayAnimation(int earnedStars)

    {

        // Panel يبدأ صغير

        winPanel.localScale = Vector3.zero;

        // كل النجوم تبدأ ظاهرة

        for (int i = 0; i < stars.Length; i++)

        {

            stars[i].localScale = Vector3.one;

        }

        // Panel Animation

        LeanTween.scale(winPanel, Vector3.one, 0.5f)

            .setIgnoreTimeScale(true)

            .setEaseOutBack()

            .setOnComplete(() => PlayStarsAnimation(earnedStars));

    }

    // private void PlayStarsAnimation(int earnedStars)

    // {

    //     for (int i = 0; i < stars.Length; i++)

    //     {

    //         if (i < earnedStars)

    //         {

    //             // النجمة المكتسبة تبدأ من صفر

    //             stars[i].localScale = Vector3.zero;

    //             // Pop Animation

    //             LeanTween.scale(stars[i].gameObject, Vector3.one, 0.25f)
    //             .setDelay(i * 0.15f)
    //             .setIgnoreTimeScale(true)
    //             .setEaseOutQuad();
    //         }

    //     }

    // }

}
