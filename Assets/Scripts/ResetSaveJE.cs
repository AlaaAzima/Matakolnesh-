using System.IO;
using UnityEngine;

public class ResetSaveJE : MonoBehaviour
{
    [ContextMenu("Reset Save")]
    public void ResetData()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted!");
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared!");
    }
}