using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{
    public void SaveGame(string saveName)
    {
        string filePath = Application.persistentDataPath + "/saves/" + saveName + ".gamesave";

        var formatter = new BinaryFormatter();
        FileStream file;
        if (!File.Exists(filePath))
            file = new FileStream(filePath, FileMode.Create);
        else
            file = new FileStream(filePath, FileMode.Open);

        var save = new Save
        {
            SaveName = saveName
        };
        formatter.Serialize(file, save);
        file.Close();
    }

    public string[] GetSaves()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            return new string[0];
        }
        string[] saves = Directory.GetFiles(Application.persistentDataPath + "/saves/");
        string[] savesNames = new string[saves.Length];
        for (int i = 0; i < saves.Length; i++)
        {
            savesNames[i] = saves[i].Split('/')[^1].Split('.')[^2];
        }
        return savesNames;
    }

    public Save LoadGame(string saveName)
    {
        string filePath = Application.persistentDataPath + "/saves/" + saveName + ".gamesave";
        if (!File.Exists(filePath))
            return new Save();

        var formatter = new BinaryFormatter();
        var file = new FileStream(filePath, FileMode.Open);

        var save = (Save)formatter.Deserialize(file);
        file.Close();
        return save;
    }

    public void DeleteGame(string saveName)
    {
        string filePath = Application.persistentDataPath + "/saves/" + saveName + ".gamesave";
        if (!File.Exists(filePath))
            return;

        File.Delete(filePath);
    }
}

[System.Serializable]
public class Save
{
    public string SaveName;
}
