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
    [SerializeField] private Button FinishButton;

    [SerializeField] private GameObject LocationWindow;
    [SerializeField] private GameObject MoneySpreadWindow;
    [SerializeField] private GameObject TimeSpreadWindow;

    private string _type = "";
    private string _thereme = "";

    private int stage = 0;

    public void CalculateOrbs()
    {

    }

    public void AllowFinish()
    {
        FinishButton.gameObject.SetActive(true);
    }

    public void OpenChooseWindow()
    {
        Debug.Log(stage);
        switch (stage)
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
    public void CheckMoneySpendingSum()
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
    private void EndChoosing()
    {
        stage += 1;
        ProgressBar.fillAmount = stage / 4f;
    }
    public void Finish()
    {
        GameInfo.Singleton.Save.EventStory.Add(GameInfo.Singleton.Save.CurrentEvent);
        GameInfo.Singleton.Save.CurrentEvent = null;
        stage = 0;
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
    public float FinalMultiplayer = 1f;

    public int CurrentAppearence = 0;
    public int CurrentMistakes = 0;
    public int CurrentTechnology = 0;
    public int CurrentConfidence = 0;
}
