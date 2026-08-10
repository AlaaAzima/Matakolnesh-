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
        Debug.Log("ShowWin called, winPanel active before: " + winPanel.activeSelf);
        winPanel.SetActive(true);
        Debug.Log("winPanel active after: " + winPanel.activeSelf);
        Time.timeScale = 0f;
        starUI.DisplayStars(earnedStars);
    }
   
    public void Restart()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void NextLevelJE()
    {
        int nextLevel = GameManagerJE.Instance.currentLevel + 1;

        if (nextLevel > 20)

        {
            SceneManager.LoadScene("StageMenu"); //if it the last level then go to the stagemenu
            return;
        }
          GameManagerJE.Instance.currentLevel = nextLevel;

        SceneManager.LoadScene("Level" + nextLevel);
    }
    
}
