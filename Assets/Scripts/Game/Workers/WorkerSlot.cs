using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerSlot : MonoBehaviour
{
    public int SlotID;
    [SerializeField] private Button startLearning;
    [SerializeField] private Button endLearning;
    [SerializeField] private Button changeWorker;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private NoonActivity noonActivity;
    [SerializeField] private GameManager manager;
    [SerializeField] private ParticleSystem particles;

    public void OnClick()
    {
        if (GameInfo.Singleton.Save.Workers[SlotID] != null)
            nameText.text = manager.GetEmployee(GameInfo.Singleton.Save.Workers[SlotID].EmployeeIDRef).Name;
        else
            nameText.text = "Никого";

        bool isLearning = noonActivity.IsLearning(this);
        startLearning.gameObject.SetActive(!isLearning);
        endLearning.gameObject.SetActive(isLearning);

        var colorsLearn = startLearning.colors;
        if (GameInfo.Singleton.Save.CurrentEvent == null && GameInfo.Singleton.Save.Workers[SlotID] != null)
        {
            colorsLearn.normalColor = Color.white;
            startLearning.interactable = true;
        }
        else
        {
            colorsLearn.normalColor = Color.gray;
            startLearning.interactable = false;
        }
        var colorsChange = changeWorker.colors;
        if (GameInfo.Singleton.Save.CurrentEvent == null)
        {
            colorsChange.normalColor = Color.white;
            changeWorker.interactable = true;
        }
        else
        {
            colorsChange.normalColor = Color.gray;
            changeWorker.interactable = false;
        }
    }

    public void CreateOrb(OrbType orb)
    {
        var main = particles.main;
        if (orb == OrbType.Mistakes)
            main.startColor = Color.red;
        else
            main.startColor = Color.green;
        particles.Play();
    }
}
