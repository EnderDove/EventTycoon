using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventOrganisationHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text Appearence;
    [SerializeField] private TMP_Text Mistakes;
    [SerializeField] private TMP_Text Technology;
    [SerializeField] private TMP_Text Confidence;
    [SerializeField] private Image ProgressBar;

    [SerializeField] private TMP_InputField NameField;
    [SerializeField] private List<MoneyWaste> MoneyWastes;
    [SerializeField] private TMP_Text MoneySum;
    [SerializeField] private Button AcceptMoneyButton;
    [SerializeField] private List<Slider> TimeSliders;
    [SerializeField] private Image LeftParameter;
    [SerializeField] private Image RightParameter;
    [SerializeField] private Button FinishButton;

    [SerializeField] private GameObject LocationWindow;
    [SerializeField] private GameObject MoneySpreadWindow;
    [SerializeField] private GameObject TimeSpreadWindow;

    private string _type = "";
    private string _thereme = "";

    public void Start()
    {
        foreach (var item in MoneyWastes)
        {
            item.GetComponent<Toggle>().onValueChanged.AddListener((bool _) => CheckMoneySpendingSum());
        }
        foreach (var item in TimeSliders)
        {
            item.onValueChanged.AddListener((float _) => RecalculateSlidersValues());
        }
    }

    public void CalculateOrbs()
    {

    }

    public void AllowFinish()
    {
        FinishButton.gameObject.SetActive(true);
    }

    public void OpenChooseWindow()
    {
        switch (GameInfo.Singleton.Save.CurrentEvent.DevelopingStage)
        {
            case 1: LocationWindow.SetActive(true); break;
            case 2: MoneySpreadWindow.SetActive(true); break;
            case 3: TimeSpreadWindow.SetActive(true); break;
            default: AllowFinish(); break;
        }
    }

    public void StartOrganising()
    {
        //Calculate Multip.

        GameInfo.Singleton.Save.CurrentEvent = new Event() { Name = NameField.text, Type = _type, Threme = _thereme };
        EndChoosing();
    }
    public void SetThreme(string threme)
    {
        //Calculate Multip.

        _thereme = threme;
    }
    public void SetType(string type)
    {
        //Calculate Multip.

        _type = type;
    }
    public void ChooseLocation(string location)
    {
        //Calculate Multip.

        GameInfo.Singleton.Save.CurrentEvent.Location = location;
        EndChoosing();
    }
    private void CheckMoneySpendingSum()
    {
        float sum = 0;
        float maxSum = 100;
        foreach (var moneyWaste in MoneyWastes)
        {
            if (moneyWaste.IsOn)
                sum += moneyWaste.Cost;
        }
        MoneySum.text = $"Итоговая Сумма {sum}/{maxSum}";
        if (sum > maxSum)
        {
            AcceptMoneyButton.enabled = false;
            MoneySum.color = Color.red;
        }
        else
        {
            AcceptMoneyButton.enabled = true;
            MoneySum.color = Color.black;
        }
    }
    public void SpendMoney()
    {
        GameInfo.Singleton.Save.CurrentEvent.MoneySpendedOn = new bool[MoneyWastes.Count];
        for (int i = 0; i < MoneyWastes.Count; i++)
            GameInfo.Singleton.Save.CurrentEvent.MoneySpendedOn[i] = MoneyWastes[i].IsOn;
        EndChoosing();
    }
    private void RecalculateSlidersValues()
    {
        float first = TimeSliders[0].value;
        float third = TimeSliders[2].value;
        float sum = first + TimeSliders[1].value + third;
        LeftParameter.fillAmount = first / sum;
        RightParameter.fillAmount = third / sum;
    }
    public void SpendTime()
    {
        float sum = 0;
        foreach (var t in TimeSliders)
            sum += t.value;
        GameInfo.Singleton.Save.CurrentEvent.TimeSpendedOn = new float[TimeSliders.Count];
        for (int i = 0; i < TimeSliders.Count; i++)
        {
            GameInfo.Singleton.Save.CurrentEvent.TimeSpendedOn[i] = TimeSliders[i].value / sum;
        }
        EndChoosing();
    }
    private void EndChoosing()
    {
        GameInfo.Singleton.Save.CurrentEvent.DevelopingStage += 1;
        ProgressBar.fillAmount = GameInfo.Singleton.Save.CurrentEvent.DevelopingStage / 4f;
    }
    public void Finish()
    {
        GameInfo.Singleton.Save.EventHitory.Add(GameInfo.Singleton.Save.CurrentEvent);
        GameInfo.Singleton.Save.CurrentEvent = null;
        GameInfo.Singleton.Save.CurrentEvent.DevelopingStage = 0;
    }
}

[System.Serializable]
public class Event
{
    public string Name = "Базовое Мероприятие";
    public string Type = "";
    public string Threme = "";
    public string Location = "";
    public bool[] MoneySpendedOn;
    public float[] TimeSpendedOn;
    public int DevelopingStage = 0;
    public float FinalMultiplayer = 1f;

    public int CurrentAppearence = 0;
    public int CurrentMistakes = 0;
    public int CurrentTechnology = 0;
    public int CurrentConfidence = 0;
}
