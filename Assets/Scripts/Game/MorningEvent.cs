using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("checking mail at morning");
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        day.SwitchActivity(day.Work);
    }
}
