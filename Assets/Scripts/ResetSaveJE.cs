using System.IO;
using UnityEngine;

public class ResetSaveJE : MonoBehaviour
{
    [ContextMenu("Reset Save")]
    void ResetData()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save deleted!");
        }
    }
}