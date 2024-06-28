using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayStateManager : MonoBehaviour
{

    DayBaseState _dayState = null;

   public MorningEvent _morning = new MorningEvent();
   public NoonWork _work = new NoonWork();
   public NoonStudy _study = new NoonStudy();
   public EveningOutcome _outcome = new EveningOutcome();

    // Start is called before the first frame update
    void Start()
    {
        _dayState = _morning;
        _dayState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _dayState.UpdateState(this);
    }
   
   public void SwitchState(DayBaseState day)
    {
        _dayState = day;
        day.EnterState(this);
    }
}
