using UnityEngine;

public class MenuToCursor : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = Input.mousePosition;
    }
}