using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DayBaseState
{
    public abstract void EnterState(DayStateManager day);
    public abstract void UpdateState(DayStateManager day);
    
}
