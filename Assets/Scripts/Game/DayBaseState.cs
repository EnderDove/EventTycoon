public abstract class DayBaseState
{
    public abstract void EnterState(DayStateManager day);
    public abstract void UpdateState(DayStateManager day);
    public abstract void NextState(DayStateManager day);
}
