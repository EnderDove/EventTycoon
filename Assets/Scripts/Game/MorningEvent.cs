using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        if (day.NewsWindow.GenerateNews())
        {
            day.NewsWindow.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        GameInfo.Singleton.Save.CurrentState = DayState.Noon;
        day.SwitchActivity(day.Noon);
    }
}
