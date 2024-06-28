using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveningOutcome : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        Debug.Log("checking forums for feedback");
    }
    public override void UpdateState(DayStateManager day)
    {

    }
}
