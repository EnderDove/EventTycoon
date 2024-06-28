using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoonWork : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("now working");
    }
    public override void UpdateState(DayStateManager day)
    {

    }
}
