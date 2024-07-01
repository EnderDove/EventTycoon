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
    [SerializeField] private List<GameObject> Tutors;
    private readonly List<MoneyGainer> moneyGainers = new();

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

        if (!GameInfo.Singleton.UseTutorial)
        {
            CloseTutorials();
        }
    }

    public void CloseTutorials()
    {
        for (int i = 0; i < Tutors.Count; i++)
        {
            Tutors[i].SetActive(false);
        }
    }

    public void SubtractMoney(int number)
    {
        GameInfo.Singleton.Save.Money -= number;
        money.text = $"{GameInfo.Singleton.Save.Money} руб";
        if (GameInfo.Singleton.Save.Money <= 50_000)
        {
            saveLoadManager.DeleteGame(GameInfo.Singleton.Save.SaveName);
            sceneChanger.LoadScene("Main Menu");
        }
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void StartGainingMoney(GameEvent gameEvent)
    {
        MoneyGainer moneyGainer = new GameObject().AddComponent<MoneyGainer>();
        moneyGainer.gameManager = this;
        moneyGainer.StartGaining(gameEvent);
        moneyGainers.Add(moneyGainer);
    }

    public void UpdateGainers()
    {
        foreach (var moneyGainer in moneyGainers)
        {
            GameInfo.Singleton.Save.Money += moneyGainer.GainMoney();
            money.text = $"{GameInfo.Singleton.Save.Money} руб";
        }

    }

    public void EndGainingMoney(MoneyGainer moneyGainer)
    {
        moneyGainers.Remove(moneyGainer);
        Destroy(moneyGainer.gameObject);
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
