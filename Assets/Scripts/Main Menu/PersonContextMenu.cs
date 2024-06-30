using UnityEngine;
using UnityEngine.UI;

public class PersonContextMenu : MonoBehaviour
{
    [SerializeField] private Button learn;

    private void OnEnable()
    {
        var colorsOrg = learn.colors;
        if (GameInfo.Singleton.Save.CurrentEvent == null)
        {
            colorsOrg.normalColor = Color.white;
            learn.interactable = true;
        }
        else
        {
            colorsOrg.normalColor = Color.gray;
            learn.interactable = false;
        }
    }
}
