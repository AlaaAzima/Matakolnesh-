using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WLGamePanelJZ : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    public void ShowWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameManagerJE.Instance.InitializeLevel();
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
