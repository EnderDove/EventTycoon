using System.Collections;
using System.Collections.Generic;
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
}
