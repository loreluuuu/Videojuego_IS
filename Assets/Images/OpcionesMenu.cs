using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OpcionesMenu : MonoBehaviour
{
    [Header("Referencias de Audio")]
    public AudioMixer audioMixer;

    [Header("Referencias de la UI")]
    public Slider sliderMusica;
    public Slider sliderSFX;

    void Start()
    {
        // Configuramos límites del slider para la fórmula Logarítmica (Evita el -Infinity)
        sliderMusica.minValue = 0.0001f;
        sliderMusica.maxValue = 1f;
        
        sliderSFX.minValue = 0.0001f;
        sliderSFX.maxValue = 1f;

        // Iniciamos los sliders visualmente al 60%
        sliderMusica.value = 0.6f;
        sliderSFX.value = 0.6f;

        // Aplicamos el volumen inicial al Mixer al arrancar
        SetVolumeMusica(0.6f);
        SetVolumeSFX(0.6f);

        // Escuchamos los cambios del Slider en tiempo real
        sliderMusica.onValueChanged.AddListener(SetVolumeMusica);
        sliderSFX.onValueChanged.AddListener(SetVolumeSFX); // ⚡ CORREGIDO: Ahora coincide exactamente con el nombre de la función abajo
    }

    public void SetVolumeMusica(float value)
    {
        if (audioMixer != null)
        {
            // Parámetro exacto: "MusicVolume"
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        }
    }

    public void SetVolumeSFX(float value)
    {
        if (audioMixer != null)
        {
            // Parámetro exacto: "SFXVolume"
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        }
    }
}