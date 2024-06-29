using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private AsyncOperation sceneLoading;
    [SerializeField] private Animator translitionAnimator;

    public void LoadScene(string name)
    {
        sceneLoading = SceneManager.LoadSceneAsync(name);
        sceneLoading.allowSceneActivation = false;
        translitionAnimator.gameObject.SetActive(true);
        translitionAnimator.Play("Open");
        Debug.Log(GameInfo.Singleton.Save.SaveName);
        Invoke(nameof(AllowSceneSwitching), 0.1f);
    }

    private void AllowSceneSwitching()
    {
        sceneLoading.allowSceneActivation = true;
    }

}