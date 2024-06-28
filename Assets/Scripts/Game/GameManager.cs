using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class that process actions in Game scene
public class GameManager : MonoBehaviour
{
    GameInfo _gameInfo = null;
    DayStateManager _dayManager = null;

    // метод, который вызывается, когда игрок запускает менюшку
    // по действию в текущий момент дня
    public void ClickOnAction()
    {
        _dayManager.SwitchState(_dayManager._work);
    }

    private void Awake()
    {
        Debug.Log("game scene");
        _gameInfo = FindAnyObjectByType<GameInfo>();
        // поч 2 раза выводит название??
        Debug.Log($"{_gameInfo.CompanyName}");
    }

    private void Start()
    {
        
    }
}
