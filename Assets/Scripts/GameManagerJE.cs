using System.Runtime.ConstrainedExecution;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerJE : MonoBehaviour
{
    public static GameManagerJE Instance { get; private set; }
    [Header("Game State")]
    public int remainingEnemies;
    public IGameState CurrentState { get; private set; }
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
        if (winCondition.CheckWin())
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
            //DontDestroyOnLoad(gameObject);
            Debug.Log("Instance Assigned");
            ChangeState(new PlayingState());
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load game data when the GameManager is initialized
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGameData()
    {
        if (saveSystem != null)
        {
            gameData = saveSystem.Load();
        }


        if (gameData == null || gameData.highestUnlockedLevel < 1)
        {
            if (gameData == null) gameData = new GameData();
            gameData.highestUnlockedLevel = 1;
        }
    }

    public GameData gameData;
    private void Start()
    {
        // gameData = new GameData { highestUnlockedLevel = 1 };
        // saveSystem.Save(gameData);
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

    public void ChangeState(IGameState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }

        CurrentState = newState;
        CurrentState.EnterState();
    }

    public void GameWin()
    {
        if (CurrentState is GameOverState) return;

        Debug.Log("GameManagerJE: GameWin() called!");
        ChangeState(new GameOverState());

        int stars = starRatingSystem.CalculateStars();

        gameData.levelStars[currentLevel - 1] =
        Mathf.Max(gameData.levelStars[currentLevel - 1], stars);
        levelUnlockSystem.UnlockNextLevel(currentLevel);
        saveSystem.Save(gameData);

        if (WLGamePanelJZ != null)
        {
            Debug.Log("GameManagerJE: Showing Win Panel...");
            WLGamePanelJZ.ShowWin(stars);
        }
        else
        {
            Debug.LogError("GameManagerJE: WLGamePanelJZ is NULL! The panel cannot be shown.");
        }
    }
    public void PlayerDied()
    {
        if (CurrentState is GameOverState)
            return;

        ChangeState(new GameOverState());
        if (winCondition.CheckLose())
        {
            saveSystem.Save(gameData);
            //WLGamePanelJZ.ShowLose();
            CameraFadeJZ.Instance.TriggerDeathSequence();
        }
    }
    public void InitializeLevel()
    {
        ChangeState(new PlayingState());
        remainingEnemies = FindObjectsOfType<EnemyHealth>().Length;
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
        WLGamePanelJZ = FindFirstObjectByType<WLGamePanelJZ>(FindObjectsInactive.Include);
        starRatingSystem = FindFirstObjectByType<StarRatingJE>(FindObjectsInactive.Include);

        InitializeLevel();
    }
}
