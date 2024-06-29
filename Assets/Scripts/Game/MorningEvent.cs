using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("good morning!");
        if (day.NewsWindow.GenerateNews())
        {
            Debug.Log("here is a news");
            day.NewsWindow.transform.GetChild(0).gameObject.SetActive(true);
        }
        
            // ask a person what they want here
            if (false)
            {
                day.wantsWork = false;
            }
            day.NextState();
        
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        if (day.wantsWork) {
            day.SwitchActivity(day.Work);
        }
        else
        {
            day.SwitchActivity(day.Study);
        }
    
    }
}
