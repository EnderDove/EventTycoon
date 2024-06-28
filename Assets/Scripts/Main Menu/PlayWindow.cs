using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayWindow : MonoBehaviour
{
    private readonly SaveLoadManager saveLoadManager = new();
    private AsyncOperation sceneLoading;
    private string companyName;
    public bool useTutorial { get; private set; }

    [SerializeField] private Image checkmark;
    [SerializeField] private Animator translitionAnimator;
    [SerializeField] private TMP_InputField textField;
    [SerializeField] private List<GameObject> oldGames;

    private void Start()
    {
        checkmark.color = Color.green;
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
        GameInfo.Save = saveLoadManager.SaveGame(companyName);
        GameInfo.UseTutorial = useTutorial;
        LoadScene();
    }

    public void LoadGame(TMP_Text text)
    {
        companyName = text.text;
        GameInfo.Save = saveLoadManager.LoadGame(companyName);
        LoadScene();
    }

    public void SwitchTutorialUse()
    {
        useTutorial = !useTutorial;
        if (useTutorial)
            checkmark.color = Color.green;
        else
            checkmark.color = Color.gray;
    }

    private void LoadScene()
    {
        sceneLoading = SceneManager.LoadSceneAsync("Game");
        sceneLoading.allowSceneActivation = false;
        translitionAnimator.gameObject.SetActive(true);
        translitionAnimator.Play("Open");
        GameInfo.CompanyName = companyName;
        Debug.Log(GameInfo.Save.SaveName);
        Invoke(nameof(AllowSceneSwitching), 0.1f);
    }

    public void AllowSceneSwitching()
    {
        sceneLoading.allowSceneActivation = true;
    }

    public void DeleteGame(TMP_Text text)
    {
        saveLoadManager.DeleteGame(text.text);
        text.rectTransform.parent.parent.gameObject.SetActive(false);
    }
}
