using TMPro;
using UnityEngine;

public class DayStateManager : MonoBehaviour
{
    private DayBaseState _dayState;

    public News NewsWindow;

    [HideInInspector] public MorningEvent Morning;
    [HideInInspector] public NoonActivity Noon;
    [SerializeField] private TMP_Text currentDay;

    public Employee BasicEmployee;

    void Start()
    {
        Morning = GetComponent<MorningEvent>();
        Noon = GetComponent<NoonActivity>();


        if (GameInfo.Singleton.Save.Day == 1 && GameInfo.Singleton.Save.CurrentState == DayState.Morning)
        {  // if game just started we give player new worker
            GameInfo.Singleton.Save.Workers[0] = Employee.ToWorkerData(BasicEmployee);
        }

        if (GameInfo.Singleton.Save.CurrentState == DayState.Morning)
            _dayState = Morning;
        else if (GameInfo.Singleton.Save.CurrentState == DayState.Noon)
            _dayState = Noon;


        _dayState.EnterState(this);
    }

    public void NextState()
    {
        _dayState.NextState(this);
        currentDay.text = $"{GameInfo.Singleton.Save.Day} День";
    }

    public void SwitchActivity(DayBaseState newActivity)
    {
        _dayState = newActivity;
        _dayState.EnterState(this);
    }
}
