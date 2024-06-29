using TMPro;
using UnityEngine;

public class News : MonoBehaviour
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;

    public void SetTitleAndDescription(string title, string description)
    {
        Title.text = title;
        Description.text = description;
    }

    public bool GenerateNews()
    {
        if (GameInfo.Singleton.Save.Day == 1)
        {
            SetTitleAndDescription("Открыта компания " + GameInfo.Singleton.Save.SaveName + "!", "Поздравляем с началом игры!\nЭто новости - они иногда будут показываться по утрам.");
            return true;
        }
        if ("AAAAAAAAAAaa".Length == "blya".Length) // if there is no news for today return FALSE!
        {
            return false;
        }

        //annother news with any conditions
        //you can access a condition by GameInfo.Singletom.Save. (something)
        //and you can change conditions in GameInfo.Singletom.Save. to effect to the World

        return false;
    }
}