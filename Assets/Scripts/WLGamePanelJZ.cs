using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WLGamePanelJZ : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject winPanel;
    

    [Header("StarUI")]
    [SerializeField] private StarUIJZ starUI;
    public void ShowWin(int earnedStars)
    {
        winPanel.SetActive(true);

        Time.timeScale = 0f;
        starUI.DisplayStars(earnedStars);
    }
   
    public void Restart()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void StageMenu()
    {
        SceneManager.LoadScene("StageMenu");
    }

    public void MainMenu()
    {
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
