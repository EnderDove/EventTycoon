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
}