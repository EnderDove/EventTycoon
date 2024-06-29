using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private NoonActivity noonActivity;
    [SerializeField] private Button organize;
    [SerializeField] private Button work;

    private void OnEnable()
    {
        var colorsOrg = organize.colors;
        if (noonActivity.CanOrganize())
        {
            colorsOrg.normalColor = Color.white;
            organize.interactable = true;
        }
        else
        {
            colorsOrg.normalColor = Color.gray;
            organize.interactable = false;
        }
        var colorsWork = work.colors;
        if (noonActivity.CanWork())
        {
            colorsWork.normalColor = Color.white;
            work.interactable = true;
        }
        else
        {
            colorsWork.normalColor = Color.gray;
            work.interactable = false;
        }
    }
}
