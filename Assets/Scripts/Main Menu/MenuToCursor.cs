using UnityEngine;

public class MenuToCursor : MonoBehaviour
{
    private RectTransform rectTransform;
    private float scaleCoefficient => 1920f / Screen.width;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = Input.mousePosition * scaleCoefficient;
    }
}
