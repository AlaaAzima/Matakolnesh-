using UnityEngine;
using System.IO;
public class SaveSystemJE : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved");
        Debug.Log(json);
    }

    public GameData Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No Save File Found");
            return new GameData();
        }
        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        Debug.Log("Game Loaded");
        Debug.Log(json);
        return data;
    }
    
}
