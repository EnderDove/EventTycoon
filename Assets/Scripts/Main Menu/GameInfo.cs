using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Singleton { get; private set; }

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

[System.Serializable]
public class Save
{
    public string SaveName;
}
