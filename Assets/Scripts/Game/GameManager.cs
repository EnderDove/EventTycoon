using UnityEngine;

// class that process actions in Game scene
[RequireComponent(typeof(SceneChanger))]
public class GameManager : MonoBehaviour
{
    private DayStateManager _dayManager;
    private SceneChanger sceneChanger;
    private readonly SaveLoadManager saveLoadManager = new();

    // метод, который вызывается, когда игрок запускает менюшку
    // по действию в текущий момент дня
    public void ClickOnAction()
    {
        _dayManager.NextState();
    }

    private void Start()
    {
        _dayManager = GetComponent<DayStateManager>();
        sceneChanger = GetComponent<SceneChanger>();
    }

    public void SaveAndQuit()
    {
        saveLoadManager.SaveGame(GameInfo.Singleton.Save);
        sceneChanger.LoadScene("Main Menu");
    }
}
