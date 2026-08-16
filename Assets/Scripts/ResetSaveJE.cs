using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSaveJE : MonoBehaviour
{
    [SerializeField] private GameObject confirmationPanel; 

    public void OnResetButtonPressed()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(true); // show "Are you sure?" UI, wire its Yes button to ConfirmReset()
        }
        else
        {
            ConfirmReset();
        }
    }

    public void ConfirmReset()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "save.json");
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Reload the current scene so everything re-initializes with a fresh GameData
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}