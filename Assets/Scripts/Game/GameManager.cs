using UnityEngine;

// class that process actions in Game scene
public class GameManager : MonoBehaviour
{
    DayStateManager _dayManager;

    // метод, который вызывается, когда игрок запускает менюшку
    // по действию в текущий момент дня
    public void ClickOnAction()
    {
        _dayManager.NextState();
    }

    private void Start()
    {
        _dayManager = GetComponent<DayStateManager>();
    }
}
