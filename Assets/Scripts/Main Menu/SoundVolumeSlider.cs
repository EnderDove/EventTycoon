using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private TMP_Text text;

    public const float volumeMultiplaer = 20;
    private float _volumeValue = 0;

    private void Start()
    {
        slider = GetComponent<Slider>();
        _volumeValue = PlayerPrefs.GetFloat(audioMixerGroup.name, 1);
        slider.value = Mathf.Pow(10f, _volumeValue / volumeMultiplaer);
        text.text = Mathf.FloorToInt(slider.value * 100).ToString() + "%";
        mixer.SetFloat(audioMixerGroup.name, _volumeValue);
        slider.onValueChanged.AddListener(UpdateVolumeLevel);
    }

    private void UpdateVolumeLevel(float volume)
    {
        _volumeValue = Mathf.Log10(volume) * volumeMultiplaer;
        text.text = Mathf.FloorToInt(volume * 100).ToString() + "%";
        mixer.SetFloat(audioMixerGroup.name, _volumeValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(audioMixerGroup.name, _volumeValue);
    }
}
