using UnityEngine;

public class PauseSystemJZ : MonoBehaviour
{
    public static PauseSystemJZ Instance { get; private set; }
    
    [Header("UI")]
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManagerJE.Instance == null || GameManagerJE.Instance.CurrentState is GameOverState) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameManagerJE.Instance == null) return;

        if (GameManagerJE.Instance.CurrentState is PausedState)
            Resume();
        else if (GameManagerJE.Instance.CurrentState.CanPause)
            Pause();
    }

    public void Pause()
    {
        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.ChangeState(new PausedState());
        }
        
        Debug.Log("Pause");
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.ChangeState(new PlayingState());
        }

        Debug.Log("Resumed");
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }
}