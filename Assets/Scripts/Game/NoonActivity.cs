using UnityEngine;

public class NoonActivity : DayBaseState
{
    private WorkerSlot selectedWarker;

    public override void EnterState(DayStateManager day)
    {
        Debug.Log("Starting " + GameInfo.Singleton.Save.CurrentState);
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        Debug.Log("Ending Day");
        GameInfo.Singleton.Save.CurrentState = DayState.Evening;
        UpdateWorkers();
        day.SwitchActivity(day.Evening);
    }

    public bool CanOrganize()
    {
        foreach (var worker in GameInfo.Singleton.Save.Workers)
            if (worker != null && worker.isLearning)
                return false;
        return true;
    }

    public bool CanWork()
    {
        foreach (var worker in GameInfo.Singleton.Save.Workers)
            if (worker != null && !worker.isLearning)
                return true;
        return false;
    }

    public void GoLearning()
    {
        GameInfo.Singleton.Save.Workers[selectedWarker.SlotID].isLearning = true;
    }

    public bool IsLearning(WorkerSlot worker)
    {
        selectedWarker = worker;
        return GameInfo.Singleton.Save.Workers[worker.SlotID].isLearning;
    }

    public void StopLearning()
    {
        GameInfo.Singleton.Save.Workers[selectedWarker.SlotID].isLearning = false;
    }

    public void UpdateWorkers()
    {
        for (int i = 0; i < GameInfo.Singleton.Save.Workers.Count; i++)
        {
            var worker = GameInfo.Singleton.Save.Workers[i];
            if (worker == null)
                continue;
            if (worker.isLearning)
            {
                worker.Expirience += 10; //example
            }
        }
    }
}
