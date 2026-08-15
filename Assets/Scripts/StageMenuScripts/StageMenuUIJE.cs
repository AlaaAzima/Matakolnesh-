using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenuUIJE : MonoBehaviour
{
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private LevelsUIJE levelUI;

    [SerializeField] private Button[] buttons;
    private void Start()
    {
        Time.timeScale = 1f;

        if (GameManagerJE.Instance != null)
        {
            GameManagerJE.Instance.LoadGameData();
        }

        ButtonsToArray();
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (GameManagerJE.Instance == null || GameManagerJE.Instance.gameData == null) return;

        GameData gameData = GameManagerJE.Instance.gameData;


        int unlockedLevel = Mathf.Max(1, gameData.highestUnlockedLevel);
        unlockedLevel = Mathf.Min(unlockedLevel, buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {

            bool isUnlocked = (i + 1) <= unlockedLevel;
            buttons[i].interactable = isUnlocked;

            int levelIndex = i + 1;

            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OpenLevel(levelIndex));
        }

        if (levelUI != null)
        {
            levelUI.UpdateButtons(buttons, unlockedLevel, gameData.levelStars);
        }
    }

    public void OpenLevel(int levelId)
    {
        GameManagerJE.Instance.currentLevel = levelId;
        SceneManager.LoadScene("Level" + levelId);
    }

    private void ButtonsToArray()
    {
        buttons = levelButtons.GetComponentsInChildren<Button>(true);
    }
}
