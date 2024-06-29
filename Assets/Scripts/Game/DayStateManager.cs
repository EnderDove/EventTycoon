using UnityEngine;

public class DayStateManager : MonoBehaviour
{
    private DayBaseState _dayState;

    public MorningEvent Morning = new MorningEvent();
    public NoonWork Work = new NoonWork();
    public NoonStudy Study = new NoonStudy();
    public EveningOutcome Outcome = new EveningOutcome();

    void Start()
    {
        _dayState = Morning;
        _dayState.EnterState(this);
    }

    void Update()
    {
        _dayState.UpdateState(this);
    }

    public void NextState()
    {
        _dayState.NextState(this);
    }

    public void SwitchActivity(DayBaseState newActivity)
    {
        _dayState = newActivity;
        _dayState.EnterState(this);
    }
}
