using UnityEngine;

public class NoonActivity : DayBaseState
{
    private WorkerSlot selectedWarker;
    [SerializeField] private EventOrganisationHandler handler;

    public override void EnterState(DayStateManager day)
    {
        Debug.Log("Starting " + GameInfo.Singleton.Save.CurrentState);
        if (GameInfo.Singleton.Save.CurrentEvent != null)
        {
            handler.OpenChooseWindow();
        }

    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        Debug.Log("Ending Day here should be smth instead of evening");
        UpdateWorkers();
    }

    public bool CanOrganize()
    {
        foreach (var worker in GameInfo.Singleton.Save.Workers)
            if (worker != null && worker.IsLearning)
                return false;
        return true;
    }

    public bool CanWork()
    {
        foreach (var worker in GameInfo.Singleton.Save.Workers)
            if (worker != null && !worker.IsLearning)
                return true;
        return false;
    }

    public void GoLearning()
    {
        GameInfo.Singleton.Save.Workers[selectedWarker.SlotID].IsLearning = true;
    }

    public bool IsLearning(WorkerSlot worker)
    {
        selectedWarker = worker;
        return GameInfo.Singleton.Save.Workers[worker.SlotID].IsLearning;
    }

    public void StopLearning()
    {
        GameInfo.Singleton.Save.Workers[selectedWarker.SlotID].IsLearning = false;
    }

    public void UpdateWorkers()
    {
        for (int i = 0; i < GameInfo.Singleton.Save.Workers.Length; i++)
        {
            var worker = GameInfo.Singleton.Save.Workers[i];
            if (worker == null)
                continue;
            if (worker.IsLearning)
            {
                worker.CommunicationSkills += 10; //example
            }
        }
    }
}
