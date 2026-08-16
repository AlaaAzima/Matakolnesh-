using UnityEngine;
using System.IO;
public class SaveSystemJE : MonoBehaviour
{
    private const int CURRENT_SAVE_VERSION = 1;

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void Save(GameData data)
    {
       data.saveVersion = CURRENT_SAVE_VERSION;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

    }

    public GameData Load()
    {
        if (!File.Exists(savePath))
            return new GameData();

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        if (data.saveVersion != CURRENT_SAVE_VERSION)
        {
            return new GameData();
        }

        return data;
    }
    
}
