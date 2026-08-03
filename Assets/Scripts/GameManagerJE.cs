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
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

        //remainingEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
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
        }
    }

    public void InitializeLevel()
    {
        isGameOver = false;
        //remainingEnemies = FindObjectsOfType<Enemy>().Length;
        starRatingSystem.ResetCounter();
        Debug.Log("Level Initialized");

    }

}
