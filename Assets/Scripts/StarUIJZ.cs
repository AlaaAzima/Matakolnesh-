using UnityEngine;
using UnityEngine.UI;
public class StarUIJZ : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;

    public void DisplayStars(int earnedStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = (i < earnedStars) ? fullStar : emptyStar;
        }
    }
}
