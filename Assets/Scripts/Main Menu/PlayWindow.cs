using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayWindow : MonoBehaviour
{
    private readonly SaveLoadManager saveLoadManager = new();

    [SerializeField] private Animator translitionAnimator;
    [SerializeField] private TMP_InputField textField;
    [SerializeField] private List<GameObject> oldGames;

    private void Start()
    {
        string[] saves = saveLoadManager.GetSaves();
        for (int i = 0; i < saves.Length; i++)
        {
            oldGames[i].GetComponentInChildren<TMP_Text>().text = saves[i];
            oldGames[i].SetActive(true);
        }
    }

    public void CreateNewSave()
    {
        if (textField.text == "")
            return;
        saveLoadManager.SaveGame(textField.text);
        translitionAnimator.gameObject.SetActive(true);
        translitionAnimator.Play("Open");
    }

    public void LoadGame(TMP_Text text)
    {
        Save save = saveLoadManager.LoadGame(text.text);
        print(save.SaveName);
        translitionAnimator.gameObject.SetActive(true);
        translitionAnimator.Play("Open");
    }

    public void DeleteGame(TMP_Text text)
    {
        saveLoadManager.DeleteGame(text.text);
        text.rectTransform.parent.parent.gameObject.SetActive(false);
    }
}
