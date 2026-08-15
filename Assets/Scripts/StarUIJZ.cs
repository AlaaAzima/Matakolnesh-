using UnityEngine;
using UnityEngine.UI;

public class StarUIJZ : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;

    [SerializeField] private ParticleSystem[] starParticles;

    private void Awake()
    {
        // بيجيب الـ Canvas اللي السكريبت جواه
        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            // يخلي الـ Render Mode يتغير برمجيّاً
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // يربط الكاميرا الرئيسية للـ الليفيل الحالي بالـ Canvas
            if (canvas.worldCamera == null)
            {
                canvas.worldCamera = Camera.main;
            }
        }
    }

    public void DisplayStars(int earnedStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            bool isEarned = (i < earnedStars);
            stars[i].sprite = isEarned ? fullStar : emptyStar;

            if (isEarned && starParticles != null && i < starParticles.Length)
            {
                if (starParticles[i] != null)
                {
                    starParticles[i].Stop();
                    starParticles[i].Play();
                }
            }
        }
    }
}