using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneChanger))]
public class PlayWindow : MonoBehaviour
{
    private readonly SaveLoadManager saveLoadManager = new();
    private string companyName;
    private bool useTutorial;
    private SceneChanger sceneChanger;

    [SerializeField] private Image checkmark;
    [SerializeField] private TMP_InputField textField;
    [SerializeField] private List<GameObject> oldGames;

    private void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();

        checkmark.gameObject.SetActive(true);
        useTutorial = true;

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
        companyName = textField.text;
        GameInfo.Singleton.Save = new() { SaveName = companyName };
        GameInfo.Singleton.UseTutorial = useTutorial;
        GameInfo.Singleton.Save.SaveName = companyName;
        saveLoadManager.SaveGame(GameInfo.Singleton.Save);
        sceneChanger.LoadScene("Game");
    }

    public void LoadGame(TMP_Text text)
    {
        companyName = text.text;
        GameInfo.Singleton.Save = saveLoadManager.LoadGame(companyName);
        sceneChanger.LoadScene("Game");
    }

    public void SwitchTutorialUse()
    {
        useTutorial = !useTutorial;
        if (useTutorial)
            checkmark.gameObject.SetActive(true);
        else
            checkmark.gameObject.SetActive(false);

    }



    public void DeleteGame(TMP_Text text)
    {
        saveLoadManager.DeleteGame(text.text);
        text.rectTransform.parent.parent.gameObject.SetActive(false);
    }
}
