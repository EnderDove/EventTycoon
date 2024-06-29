using UnityEngine;

public class WorkerSlot : MonoBehaviour
{
    public int SlotID;
    [SerializeField] private GameObject startLearningButton;
    [SerializeField] private GameObject endLearningButton;

    [SerializeField] private NoonActivity noonActivity;

    public void OnClick()
    {
        bool isLearning = noonActivity.IsLearning(this);
        startLearningButton.SetActive(!isLearning);
        endLearningButton.SetActive(isLearning);
    }
}
