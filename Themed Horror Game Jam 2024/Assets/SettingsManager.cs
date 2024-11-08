using UnityEngine.UI;
using UnityEngine;

/*
 * To set and save values, put two sliders and give this refs to them
 * To get saved values:
 *  * SettingsManager.Volume || SettingsManager.Brightness
 * Should be the same thing
 */
public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;

    private const string VolumePrefKey = "Volume";
    private const string BrightnessPrefKey = "Brightness";

    public static float Volume => PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
    public static float Brightness => PlayerPrefs.GetFloat(BrightnessPrefKey, 1.0f);

    void Start()
    {
        // Load saved values or set defaults if no values exist
        volumeSlider.value = Volume;
        brightnessSlider.value = Brightness;
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    private void OnBrightnessChanged(float value)
    {
        PlayerPrefs.SetFloat(BrightnessPrefKey, value);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
