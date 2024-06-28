using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public void SaveGame(string saveName)
    {
        string filePath = Application.persistentDataPath + "/saves/" + saveName + ".gamesave";

        var formatter = new BinaryFormatter();
        var file = new FileStream(filePath, FileMode.OpenOrCreate);

        var save = new Save
        {
            SaveName = saveName
        };
        formatter.Serialize(file, save);
        file.Close();
    }

    public string[] GetSaves()
    {
        string[] saves = Directory.GetFiles(Application.persistentDataPath + "/saves");
        string[] savesNames = new string[saves.Length];
        for (int i = 0; i < saves.Length; i++)
        {
            savesNames[i] = savesNames[i].Split('/')[^1].Split('.')[^2];
        }
        return savesNames;
    }

    public void LoadGame(string saveName)
    {
        string filePath = Application.persistentDataPath + "/saves/" + saveName + ".gamesave";
        if (!File.Exists(filePath))
            return;

        var formatter = new BinaryFormatter();
        var file = new FileStream(filePath, FileMode.Open);

        var save = (Save)formatter.Deserialize(file);
        file.Close();
    }
}

[System.Serializable]
public class Save
{
    public string SaveName;
}
