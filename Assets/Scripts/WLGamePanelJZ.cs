using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WLGamePanelJZ : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    [Header("StarUI")]
    [SerializeField] private StarUIJZ starUI;
    public void ShowWin(int earnedStars)
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        starUI.DisplayStars(earnedStars);
    }
    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    
}
