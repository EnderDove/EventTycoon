using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("checking mail at morning");
    }
    public override void UpdateState(DayStateManager day)
    {
        day.SwitchState(day._work);
    }
}
