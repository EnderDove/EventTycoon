using UnityEngine;

public class NoonStudy : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("now studying");
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        day.SwitchActivity(day.Outcome);
    }
}
