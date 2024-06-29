using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        if (day.NewsWindow.GenerateNews())
        {
            day.NewsWindow.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
            day.NextState();
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        day.SwitchActivity(day.Work);
    }
}
