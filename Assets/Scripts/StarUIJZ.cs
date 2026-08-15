using UnityEngine;
using UnityEngine.UI;

public class StarUIJZ : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;


    [SerializeField] private ParticleSystem[] starParticles;

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
                    starParticles[i].Play();
                }
            }
        }
    }
}