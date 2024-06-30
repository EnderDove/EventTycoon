using System.Collections.Generic;
using TMPro;
using UnityEngine;

// class that process actions in Game scene
[RequireComponent(typeof(SceneChanger))]
public class GameManager : MonoBehaviour
{
    public WorkerSlot[] WorkerSlots { get; private set; }
    [SerializeField] private List<Employee> allEmployees;
    [SerializeField] private TMP_Text money;

    private DayStateManager _dayManager;
    private SceneChanger sceneChanger;
    private readonly SaveLoadManager saveLoadManager = new();
    private EventOrganisationHandler handler;

    // метод, который вызывается, когда игрок запускает менюшку
    // по действию в текущий момент дня
    public void ClickOnAction()
    {
        _dayManager.NextState();
    }

    public Employee GetEmployee(int id)
    {
        return allEmployees[id];
    }

    private void Start()
    {
        _dayManager = GetComponent<DayStateManager>();
        sceneChanger = GetComponent<SceneChanger>();
        handler = GetComponent<EventOrganisationHandler>();

        var slots = FindObjectsOfType<WorkerSlot>();
        WorkerSlots = new WorkerSlot[slots.Length];
        foreach (var slot in slots)
        {
            WorkerSlots[slot.SlotID] = slot;
        }
        money.text = $"{GameInfo.Singleton.Save.Money} руб";
    }

    public void SubtractMoney(int number)
    {
        GameInfo.Singleton.Save.Money -= number;
        money.text = $"{GameInfo.Singleton.Save.Money} руб";
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void Save()
    {
        saveLoadManager.SaveGame(GameInfo.Singleton.Save);
    }

    public void Quit()
    {
        SetTimeScale(1);
        if (GameInfo.Singleton.Save.CurrentEvent != null)
            handler.FinishGainingOrbs();
        Save();
        sceneChanger.LoadScene("Main Menu");
    }
}
