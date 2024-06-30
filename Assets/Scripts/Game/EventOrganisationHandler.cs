using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventOrganisationHandler : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private TMP_Text Appearence;
    [SerializeField] private TMP_Text Mistakes;
    [SerializeField] private TMP_Text Technology;
    [SerializeField] private TMP_Text Confidence;

    [SerializeField] private Image ProgressBar;
    [SerializeField] private float BarFillDurationSec = 15;
    [SerializeField] private GameObject score;

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
        manager = GetComponent<GameManager>();
        foreach (var item in MoneyWastes)
        {
            item.GetComponent<Toggle>().onValueChanged.AddListener((bool _) => CheckMoneySpendingSum());
        }
        foreach (var item in TimeSliders)
        {
            item.onValueChanged.AddListener((float _) => RecalculateSlidersValues());
        }
        if (GameInfo.Singleton.Save.CurrentEvent != null)
        {
            score.SetActive(true);
            SyncPoints();
        }
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
            case 4: StartCriticalManagement(); break;
            default: AllowFinish(); break;
        }
    }

    public void StartOrganising()
    {
        //Calculate Multip.

        GameInfo.Singleton.Save.CurrentEvent = new GameEvent() { Name = NameField.text, Type = _type, Threme = _thereme };
        FinishButton.gameObject.SetActive(false);
        SyncPoints();
        EndChoosing();
    }
    private void SyncPoints()
    {
        Mistakes.text = GameInfo.Singleton.Save.CurrentEvent.CurrentMistakes.ToString();
        Appearence.text = GameInfo.Singleton.Save.CurrentEvent.CurrentAppearence.ToString();
        Confidence.text = GameInfo.Singleton.Save.CurrentEvent.CurrentConfidence.ToString();
        Technology.text = GameInfo.Singleton.Save.CurrentEvent.CurrentTechnology.ToString();
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
        GameInfo.Singleton.Save.CurrentEvent.Location = location;
        EndChoosing();

    }
    private void CheckMoneySpendingSum()
    {
        float sum = 0;
        float maxSum = 50000;
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
        StartCoroutine(FillProgress());
        StartCoroutine(GenerateOrbs(Random.Range(0, 4), Random.Range(2, 6), Random.Range(2, 6), Random.Range(2, 6)));
    }

    private void StartCriticalManagement()
    {
        StartCoroutine(FillProgress());
        StartCoroutine(GenerateOrbs(Random.Range(6, 13), 0, 0, 0));
    }

    public void FinishGainingOrbs()
    {
        StopAllCoroutines();
        GameInfo.Singleton.Save.CurrentEvent.DevelopingStage += 1;
        ProgressBar.fillAmount = GameInfo.Singleton.Save.CurrentEvent.DevelopingStage / 5f;
    }

    private IEnumerator GenerateOrbs(int type1, int type2, int type3, int type4)
    {
        List<WorkerSlot> availableWorkers = new();
        for (int i = 0; i < GameInfo.Singleton.Save.Workers.Length; i++)
            if (GameInfo.Singleton.Save.Workers[i] != null)
                availableWorkers.Add(manager.WorkerSlots[i]);
        var availableWorkersCount = availableWorkers.Count;

        int orbCnt = type1 + type2 + type3 + type4;
        // here we create all the orbs in random times between begin and end of filling
        var timeToType = new SortedDictionary<float, OrbType>();
        //timeToType.Add(0, (OrbType)5); // fictionary record to help processing real ones

        for (int i = 0; i < orbCnt; ++i)
        {
            if (i < type1) timeToType.Add(Random.Range(0, BarFillDurationSec), OrbType.Mistakes);
            else if (i < type1 + type2) timeToType.Add(Random.Range(0, BarFillDurationSec), OrbType.Appearence);
            else if (i < orbCnt - type4) timeToType.Add(Random.Range(0, BarFillDurationSec), OrbType.Technology);
            else if (i < orbCnt) timeToType.Add(Random.Range(0, BarFillDurationSec), OrbType.Confidence);
        }
        float prevOrbTime = 0;
        // here we spawn orbs and wait between different spawns
        foreach (var entry in timeToType)
        {
            yield return new WaitForSeconds(entry.Key - prevOrbTime);
            prevOrbTime = entry.Key;
            var slot = availableWorkers[Random.Range(0, availableWorkersCount)];
            slot.CreateOrb(entry.Value);
            switch (entry.Value)
            {
                case OrbType.Mistakes:
                    if (GameInfo.Singleton.Save.CurrentEvent.DevelopingStage == 4)
                    {
                        GameInfo.Singleton.Save.CurrentEvent.CurrentMistakes -= 1;
                        if (GameInfo.Singleton.Save.CurrentEvent.CurrentMistakes == 0)
                            FinishGainingOrbs();
                    }
                    else
                    {
                        GameInfo.Singleton.Save.CurrentEvent.CurrentMistakes += 1;
                    }
                    break;
                case OrbType.Appearence: GameInfo.Singleton.Save.CurrentEvent.CurrentAppearence += 1; break;
                case OrbType.Confidence: GameInfo.Singleton.Save.CurrentEvent.CurrentConfidence += 1; break;
                case OrbType.Technology: GameInfo.Singleton.Save.CurrentEvent.CurrentTechnology += 1; break;
            }
            SyncPoints();
            // instantiate orb and increase corresponding stat if it is not a type = 5
        }
    }

    private IEnumerator FillProgress()
    {
        // when stopping event we should deactivate bar?
        if (ProgressBar == null) yield break;

        // what to do when bar is filled??
        float timeSpend = 0;
        while (timeSpend <= BarFillDurationSec)
        {
            ProgressBar.fillAmount = (timeSpend / BarFillDurationSec + GameInfo.Singleton.Save.CurrentEvent.DevelopingStage) / 5f;
            timeSpend += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void Finish()
    {
        ScoreCalculator.Calculate(GameInfo.Singleton.Save.CurrentEvent);
        Debug.Log(GameInfo.Singleton.Save.CurrentEvent.FinalMultiplayer);
        GameInfo.Singleton.Save.EventHitory.Add(GameInfo.Singleton.Save.CurrentEvent);
        GameInfo.Singleton.Save.CurrentEvent = null;
    }
}

public enum OrbType
{
    Mistakes = 0,
    Appearence = 1,
    Technology = 2,
    Confidence = 3,
}

[System.Serializable]
public class GameEvent
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
