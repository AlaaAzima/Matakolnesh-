using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenuUIJE : MonoBehaviour
{
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private LevelsUIJE levelUI;

    private Button[] buttons;

    private void Start()
    {
        ButtonsToArray();
        RefreshUI();
    }

    private void RefreshUI()
    {
        GameData gameData = GameManagerJE.Instance.gameData;
        int unlockedLevel = Mathf.Min(gameData.highestUnlockedLevel, buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i + 1) <= unlockedLevel;
        }

        levelUI.UpdateButtons(buttons, unlockedLevel, gameData.levelStars);
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
