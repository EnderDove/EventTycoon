using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMetaStatsUI : MonoBehaviour
{
    public TMP_Text Appearence;
    public TMP_Text Mistakes;
    public TMP_Text Technology;
    public TMP_Text Confidence;
    // Start is called before the first frame update
    void Start()
    {
        int tmp = 7;
        Appearence.text = $"appearance: {tmp}";
        Mistakes.text = $"mistakes: {tmp}";
        Technology.text = $"technology: {tmp}";
        Confidence.text = $"confidence: {tmp}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
