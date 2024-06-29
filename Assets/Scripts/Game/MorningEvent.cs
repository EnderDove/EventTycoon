using UnityEngine;

public class MorningEvent : DayBaseState
{
    public override void EnterState(DayStateManager day)
    {
        day.NewsWindow.transform.GetChild(0).gameObject.SetActive(true);
        GenerateNews(day.NewsWindow);
    }
    public override void UpdateState(DayStateManager day)
    {

    }
    public override void NextState(DayStateManager day)
    {
        day.SwitchActivity(day.Work);
    }

    public void GenerateNews(News news)
    {
        if (GameInfo.Singleton.Save.Day == 1)
        {
            news.SetTitleAndDescription("Открыта компания " + GameInfo.Singleton.Save.SaveName, "Поздравляем с началом игры!/nЭто новости - они иногда будут показываться по утрам.");
        }
    }
}
