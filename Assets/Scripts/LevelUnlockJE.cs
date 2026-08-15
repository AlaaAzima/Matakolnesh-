using UnityEngine;

public class LevelUnlockJE : MonoBehaviour
{
    [Header("Every-5-Levels Gate")]
    [SerializeField] private int gateInterval = 5;      
    [SerializeField] private int minStarsPerLevel = 2;  
    [SerializeField] private int requiredLevelsMeetingMin = 3; 

    public void UnlockNextLevel(int currentLevel)
    {
        

        int nextLevel = currentLevel + 1;

        
        if (nextLevel % gateInterval == 0)
        {
            if (!CanUnlockGatedLevel(currentLevel))
            {
                Debug.Log($"Level {nextLevel} locked. Get at least {minStarsPerLevel} stars on {requiredLevelsMeetingMin} previous levels to unlock it.");
                return;
            }
        }

        GameManagerJE.Instance.gameData.highestUnlockedLevel =
            Mathf.Max(GameManagerJE.Instance.gameData.highestUnlockedLevel, nextLevel);
        Debug.Log("Level " + nextLevel + " Unlocked");
    }

    
    public bool CanUnlockGatedLevel(int currentLevel)
    {
        int[] levelStars = GameManagerJE.Instance.gameData.levelStars;
        int levelsMeetingMin = 0;

        for (int i = 0; i < currentLevel; i++)
        {
            if (i >= levelStars.Length) break;

            if (levelStars[i] >= minStarsPerLevel)
                levelsMeetingMin++;
        }

        return levelsMeetingMin >= requiredLevelsMeetingMin;
    }
}