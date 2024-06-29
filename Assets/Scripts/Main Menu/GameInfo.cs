using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Singleton
    {
        get
        {
            if (singleton == null)
                Debug.LogWarning("Game Info not created yet, pls start game from main menu scene");
            return singleton;
        }
    }
    private static GameInfo singleton;

    // name only affects the name of a save file
    public bool UseTutorial { get; set; } = false;
    public Save Save
    {
        get
        {
            save ??= new();
            return save;
        }
        set
        {
            save = value;
        }
    }
    private Save save;

    private void Awake()
    {
        if (singleton != null)
            Destroy(gameObject);
        singleton = this;
        DontDestroyOnLoad(gameObject);
    }
}

// класс, хранящий данные, которые нужны во время игры
// название берем из объекта Save или из названия файла
// туториал можно пройти при первом создании сейва (если выйти из игры во
// время туториала, то при перезаходе его не будет)
[System.Serializable]
public class Save
{
    public string SaveName = "Default Company";
    public int Day = 1;
    public DayState CurrentState = DayState.Morning;
    public int Followers = 0;
    public int Money = 10000;
    public int DesignSkill = 0;
    public int Speed = 0;
    public int Social = 0;
}

public enum DayState
{
    Morning = 0,
    Work = 1,
    Study = 2,
    Evening = 3,
}