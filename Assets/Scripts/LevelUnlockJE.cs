using UnityEngine;

public class LevelUnlockJE : MonoBehaviour
{
    [SerializeField] private int BossRequiredStars = 40;
    public void UnlockNextLevel(int currentLevel)
    {
        GameManagerJE.Instance.isGameOver = false;
        if (currentLevel == 19)
        {
            if (CanUnlockBoss())
            {
                Debug.Log("Boss Level Unlocked");
            }
            else
            {
                Debug.Log("Collect at least 40 stars to unlock the Boss.");
            }

            return;
        }
        Debug.Log("Level " + (currentLevel + 1) + " Unlocked");
    }


    public bool CanUnlockBoss()
    {
        int totalStars = 0;

        for (int i = 0; i < 19; i++) 
        {
            totalStars += GameManagerJE.Instance.gameData.levelStars[i];
        }

        return totalStars >= BossRequiredStars;
    }

}
