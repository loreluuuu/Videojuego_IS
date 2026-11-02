using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sliderMusica;
    public Slider sliderFX;

    void Start()
    {
        float musicValue = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float fxValue = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        sliderMusica.value = musicValue;
        sliderFX.value = fxValue;

        mixer.SetFloat("MusicVolume", Mathf.Log10(musicValue) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(fxValue) * 20);
    }

    public void CambiarMusica(float value)
    {
        // Si el valor es casi 0, usamos -80 (que es silencio total en decibelios)
        float volume = value > 0 ? Mathf.Log10(value) * 20 : -80;
        mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void CambiarFX(float value)
    {
        float volume = value > 0 ? Mathf.Log10(value) * 20 : -80;
        mixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}