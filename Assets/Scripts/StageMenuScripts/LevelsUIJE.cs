using UnityEngine;
using UnityEngine.UI;


public class LevelsUIJE : MonoBehaviour
{
    [SerializeField] Color lockedColor = Color.gray;
    [SerializeField] Color unlockedColor = Color.white;
    [SerializeField] Sprite starFilled;
    [SerializeField] Sprite starEmpty;

    public void UpdateButtons(Button[] buttons, int unlockedLevel, int[] levelStars)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int levelNumber = i + 1; // 1-indexed
            bool unlocked = levelNumber <= unlockedLevel;

            Image image = buttons[i].GetComponent<Image>();
            if (image != null)
                image.color = unlocked ? unlockedColor : lockedColor;

            UpdateStars(buttons[i], levelStars, levelNumber, unlocked);
        }
    }


    void UpdateStars(Button button, int[] levelStars, int levelNumber, bool unlocked)
{
    Transform starsContainer = button.transform.Find("StarsContainer");
    if (starsContainer == null) return;

    starsContainer.gameObject.SetActive(unlocked);
    if (!unlocked) return;

    int starsEarned = (levelNumber - 1 < levelStars.Length)
        ? levelStars[levelNumber - 1]
        : 0;

    for (int s = 0; s < starsContainer.childCount; s++)
    {
        Image starImage = starsContainer.GetChild(s).GetComponent<Image>();
        if (starImage == null) continue;

        starImage.sprite = s < starsEarned ? starFilled : starEmpty;
    }
}


    // void UpdateStars(Button button, int[] levelStars, int levelNumber, bool unlocked)
    // {
    //     Transform starsContainer = button.transform.Find("StarsContainer");
    //     if (starsContainer == null) return;

    //     int starsEarned = (unlocked && levelNumber - 1 < levelStars.Length)
    //         ? levelStars[levelNumber - 1]
    //         : 0;

    //     for (int s = 0; s < starsContainer.childCount; s++)
    //     {
    //         Image starImage = starsContainer.GetChild(s).GetComponent<Image>();
    //         if (starImage == null) continue;

    //         starImage.gameObject.SetActive(unlocked);

    //         //starImage.sprite = s < starsEarned ? starFilled : starEmpty;

    //         
    //      
    //     }
    // }
}
