using JetBrains.Annotations;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private GameStats _gameStats { get; set; }
    
    public static GameInfo Singleton { get; private set; }

    // name only affects the name of a save file
    public string CompanyName { get; set; }
    public bool UseTutorial { get; set; } = false;
    public Save Save { get; set; }

    private void Awake()
    {
        if (Singleton != null)
            Destroy(gameObject);
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }
}

// класс, хранящий данные, которые нужны во время игры
// название берем из объекта Save или из названия файла
// туториал можно пройти при первом создании сейва (если выйти из игры во
// время туториала, то при перезаходе его не будет)
[System.Serializable]
public class GameStats
{
    public int Day = 1;
    public int Followers = 0;
    public int Money = 0;
    public int DesignSkill = 0;
    public int Speed = 0;
    public int Social = 0;
}

[System.Serializable]
public class Save
{
    public string SaveName = "blank";
}
