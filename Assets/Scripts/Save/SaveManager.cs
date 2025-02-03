using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        // 持久数据路径
        savePath = Application.persistentDataPath + "/savedata.json";
        // savePath = Application.dataPath + "/savedata.json";
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }
}
