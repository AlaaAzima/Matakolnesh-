using UnityEngine;

public class GameTestJE : MonoBehaviour
{
    [SerializeField] private WinAndLoseJE winAndLose;
    [SerializeField] private StarRatingJE starRating;
    [SerializeField] private LevelUnlockJE levelUnlock;
    [SerializeField] private SaveSystemJE saveSystem;
    [SerializeField] private GameManagerJE gameManager;

    [ContextMenu("Test Win")]
    public void TestWin()
    {
    gameManager.remainingEnemies = 1;
    gameManager.EnemyKilled();
   
    }

    [ContextMenu("Shoot 5 Arrows")]
    public void ShootFiveArrows()
    {
        starRating.ResetCounter();

        for (int i = 0; i < 5; i++)
        {
            starRating.ArrowShot();
        }

        Debug.Log("Stars: " + starRating.CalculateStars());
    }


    [ContextMenu("Test Stars")]
    public void TestStars()
    {
        Debug.Log("Stars: " + starRating.CalculateStars());
    }

    

    [ContextMenu("Test Lose")]
    public void TestLose()
    {
        gameManager.isGameOver = false;
        gameManager.PlayerDied();
    }

    [ContextMenu("Test Unlock")]
    public void TestUnlock()
    {
        levelUnlock.UnlockNextLevel(gameManager.currentLevel);
    }

   
   [ContextMenu("Test Save & Load")]
    public void TestSaveLoad()
    {
        gameManager.gameData.highestUnlockedLevel = 5;

        saveSystem.Save(gameManager.gameData);

        GameData data = saveSystem.Load();

        Debug.Log(data.highestUnlockedLevel);
    }



[ContextMenu("Give Max Stars")]
public void GiveMaxStars()
{
    for (int i = 0; i < 19; i++)
    {
        gameManager.gameData.levelStars[i] = 2;
    }

    levelUnlock.UnlockNextLevel(19);
}

}