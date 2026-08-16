using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WLGamePanelJZ : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject winPanel;
    [SerializeField] private WinPanelAnimJE winPanelAnimation;


    [Header("StarUI")]
    [SerializeField] private StarUIJZ starUI;
    [Header("Win Settings")]
    [SerializeField] private float winDelay = 0.3f;
    [Header("Star Requirements")]
    // Element 0: Level 1-5 required stars
    // Element 1: Level 6-10 required stars
    // Element 2: Level 11-15 required stars
    // Element 3: Level 16-20 required stars
    [SerializeField] private int[] starsRequiredPerTier = new int[] { 0, 10, 20, 30 };

    public void ShowWin(int earnedStars)
    {
        CameraShakeJE.Instance.Shake();
        StartCoroutine(ShowWinCoroutine(earnedStars));
    }


    private IEnumerator ShowWinCoroutine(int earnedStars)
    {

        yield return new WaitForSeconds(winDelay);


        winPanel.SetActive(true);

        if (starUI != null)
        {
            starUI.DisplayStars(earnedStars);
            SoundManager.PlaySound(SoundType.WinSound);
            GameEvents.TriggerPlayVFX(VFXType.GameWin, winPanel.transform.position);

        }

        Time.timeScale = 0f;

        winPanelAnimation.PlayAnimation(earnedStars);
    }

    public void Restart()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void StageMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageMenu");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelJE()
    {
        int nextLevel = GameManagerJE.Instance.currentLevel + 1;

        // 1. Check if next level exceeds max levels
        if (nextLevel > 20)
        {
            StageMenu();
            return;
        }

        // 2. Calculate total accumulated stars
        int playerStars = 0;
        if (GameManagerJE.Instance != null && GameManagerJE.Instance.gameData != null)
        {
            foreach (int stars in GameManagerJE.Instance.gameData.levelStars)
            {
                playerStars += stars;
            }
        }

        // 3. Determine required stars for the upcoming level tier
        // Math: (nextLevel - 1) / 5 calculates index (Levels 1-5 -> Index 0, Levels 6-10 -> Index 1, etc.)
        int tierIndex = (nextLevel - 1) / 5;
        int neededStars = 0;

        if (tierIndex < starsRequiredPerTier.Length)
        {
            neededStars = starsRequiredPerTier[tierIndex];
        }

        // 4. Check if player has enough stars for the next level
        if (neededStars > playerStars)
        {
            StageMenu(); // Not enough stars -> redirect to Stage Menu
            return;
        }

        // 5. Load Next Level
        GameManagerJE.Instance.currentLevel = nextLevel;
        SceneManager.LoadScene("Level" + nextLevel);
    }


}
