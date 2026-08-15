using UnityEngine;

public class WinAndLoseJE : MonoBehaviour
{
    [Header("Conditions")]
    [Tooltip("Drag and drop LevelCondition scripts here (e.g. KillAllEnemiesCondition). ALL conditions must be met to win.")]
    [SerializeField] private LevelCondition[] winConditions;

    public bool CheckWin()
    {
        if (GameManagerJE.Instance != null && GameManagerJE.Instance.CurrentState is GameOverState)
            return false;

        if (winConditions == null || winConditions.Length == 0)
        {
            Debug.LogWarning("WinAndLoseJE: No Win Conditions assigned in the inspector!");
            return false;
        }

        foreach (var condition in winConditions)
        {
            if (!condition.IsConditionMet())
            {
                Debug.Log($"WinAndLoseJE: Condition '{condition.GetType().Name}' is NOT met yet!");
                return false; 
            }
        }

        Debug.Log("WinAndLoseJE: YOU WIN! All conditions met.");
        return true;
    }

    public bool CheckLose()
    {
        if (GameManagerJE.Instance != null && GameManagerJE.Instance.CurrentState is GameOverState)
        {
            Debug.Log("YOU LOSE!");
            return true;
        }

        return false;
    }
}
