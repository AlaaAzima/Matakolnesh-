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

        if (nextLevel > 20)

        {
            StageMenu(); //if it the last level then go to the stagemenu
            return;
        }
        GameManagerJE.Instance.currentLevel = nextLevel;

        SceneManager.LoadScene("Level" + nextLevel);
    }

}
