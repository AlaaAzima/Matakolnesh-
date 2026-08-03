using UnityEngine;

public class PauseSystemJZ : MonoBehaviour
{
    public static PauseSystemJZ Instance { get; private set; }

    public bool IsPaused { get; private set; } = false;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Pause");
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Resumed");
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }
}
