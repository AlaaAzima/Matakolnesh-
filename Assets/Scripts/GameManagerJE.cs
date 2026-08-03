using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class GameManagerJE : MonoBehaviour
{
    public static GameManagerJE Instance { get; private set; }

    [Header("Game State")]
    public int remainingEnemies;
    public bool isGameOver = false;
    public int currentLevel;

    [Header("Systems")]
    [SerializeField] private WinAndLoseJE winCondition;
    [SerializeField] private StarRatingJE starRatingSystem;
    [SerializeField] private LevelUnlockJE levelUnlockSystem;
    [SerializeField] private SaveSystemJE saveSystem;

    [Header("GamePanel")]
    [SerializeField] WLGamePanelJZ WLGamePanelJZ;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance Assigned");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public GameData gameData;
    private void Start()
    {
        gameData = saveSystem.Load();
        Debug.Log("Loaded Level: " + currentLevel);
        InitializeLevel();

        remainingEnemies = FindObjectsByType<EnemyLogicJZ>(FindObjectsSortMode.None).Length;
    }

    public void EnemyKilled()
    {
        remainingEnemies--;
        Debug.Log("Remaining Enemies: " + remainingEnemies);

        if (winCondition.CheckWin())
        {
            int stars = starRatingSystem.CalculateStars();
    
            gameData.levelStars[currentLevel - 1] =
            Mathf.Max(gameData.levelStars[currentLevel - 1], stars);

            levelUnlockSystem.UnlockNextLevel(currentLevel);
            saveSystem.Save(gameData);

            WLGamePanelJZ.ShowWin();
        }
    }

    public void PlayerDied()
    {
        if (isGameOver)
        return;

        isGameOver = true;
        if (winCondition.CheckLose())
        {
            saveSystem.Save(gameData);
            WLGamePanelJZ.ShowLose();
        }
    }

    public void InitializeLevel()
    {
        isGameOver = false;
        remainingEnemies = FindObjectsOfType<EnemyLogicJZ>().Length;
        starRatingSystem.ResetCounter();
        Debug.Log("Level Initialized");

    }

}
