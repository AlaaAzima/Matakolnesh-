using System.Runtime.ConstrainedExecution;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    //================Alaa================


    public int activeArrowCount { get; private set; } = 0;

    public void RegisterArrow()
    {
        activeArrowCount++;
    }

    public void UnregisterArrow()
    {
        activeArrowCount--;
        if (activeArrowCount < 0) activeArrowCount = 0;
        if(winCondition.CheckWin())
        {
            GameWin();
        }
    }

    //================Alaa================
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance Assigned");
            SceneManager.sceneLoaded += OnSceneLoaded; // subscribe
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
        /*in OnSceneLoaded*/
        //InitializeLevel(); 
        //remainingEnemies = FindObjectsByType<EnemyLogicJZ>(FindObjectsSortMode.None).Length;
    }

    public void ArrowShot()
    {
        starRatingSystem.ArrowShot();
    }

    public void EnemyKilled()
    {
        remainingEnemies--;
        Debug.Log("Remaining Enemies: " + remainingEnemies);

        if (winCondition.CheckWin())
        {
            GameWin();
        }
    }

    public void GameWin()
    {
        if (isGameOver) return;

        isGameOver = true;

        int stars = starRatingSystem.CalculateStars();

        gameData.levelStars[currentLevel - 1] =
        Mathf.Max(gameData.levelStars[currentLevel - 1], stars);
        levelUnlockSystem.UnlockNextLevel(currentLevel);
        saveSystem.Save(gameData);

        if (WLGamePanelJZ != null)
        {
            WLGamePanelJZ.ShowWin(stars);
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
            //WLGamePanelJZ.ShowLose();
            CameraFadeJZ.Instance.TriggerDeathSequence();
        }
    }
    public void InitializeLevel()
    {
        isGameOver = false;
        remainingEnemies = FindObjectsOfType<EnemyLogicJZ>().Length;
        starRatingSystem.ResetCounter();
        Debug.Log("Level Initialized");
    }
    //For Restart Button
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // unsubscribe
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        WLGamePanelJZ = FindFirstObjectByType<WLGamePanelJZ>();
        starRatingSystem = FindFirstObjectByType<StarRatingJE>();

        InitializeLevel();
    }
}
