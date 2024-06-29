using UnityEngine;

public class DayStateManager : MonoBehaviour
{
    private DayBaseState _dayState;

    public News NewsWindow;
    public bool wantsWork = true;

    [HideInInspector] public MorningEvent Morning;
    [HideInInspector] public NoonActivity Noon;
    [HideInInspector] public EveningOutcome Evening;

    public Employee BasicEmployee;

    void Start()
    {
        Morning = GetComponent<MorningEvent>();
        Noon = GetComponent<NoonActivity>();
        Evening = GetComponent<EveningOutcome>();

        if (GameInfo.Singleton.Save.Day == 1 && GameInfo.Singleton.Save.CurrentState == DayState.Morning)
        {  // if game just started we give player new worker
            GameInfo.Singleton.Save.Workers[0] = Employee.ToWorkerData(BasicEmployee);
        }

        if (GameInfo.Singleton.Save.CurrentState == DayState.Morning)
            _dayState = Morning;
        else if (GameInfo.Singleton.Save.CurrentState == DayState.Noon)
            _dayState = Noon;
        else
            _dayState = Evening;


        _dayState.EnterState(this);
    }

    void Update()
    {
        _dayState.UpdateState(this);
    }

    public void NextState()
    {
        _dayState.NextState(this);
        print(GameInfo.Singleton.Save.CurrentState);
    }

    public void SwitchActivity(DayBaseState newActivity)
    {
        _dayState = newActivity;
        _dayState.EnterState(this);
    }
}
