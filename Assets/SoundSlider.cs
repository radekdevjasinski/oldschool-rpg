using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    Music,
    SFX
}

public class SoundSlider : MonoBehaviour
{
    public SoundType soundType;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = soundType == SoundType.Music ? AudioManager.Instance.musicSource.volume : AudioManager.Instance.sfxSource.volume;
    }
    public void OnSliderValueChanged()
    {
        if (soundType == SoundType.Music)
        {
            AudioManager.Instance.musicSource.volume = slider.value;
        }
        else if (soundType == SoundType.SFX)
        {
            AudioManager.Instance.sfxSource.volume = slider.value;
        }
    }
}
