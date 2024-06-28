using TMPro;
using UnityEngine;

public class SaveDeleter : MonoBehaviour
{
    private readonly SaveLoadManager saveLoadManager = new();
    private TMP_Text _toDelete;

    public void StartDeleting(TMP_Text toDelete)
    {
        _toDelete = toDelete;
    }

    public void DeleteGame()
    {
        saveLoadManager.DeleteGame(_toDelete.text);
        _toDelete.rectTransform.parent.parent.gameObject.SetActive(false);
    }
}
