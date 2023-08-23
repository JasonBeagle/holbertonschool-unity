using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    private Toggle invertYAxis;
    public AudioMixer bgmMixer;
    public AudioMixerSnapshot defaultSnapshot;
    public Slider bgmSlider;
    public Slider sfxSlider;
    void Start()
    {
        transform.Find("ApplyButton").gameObject.GetComponent<Button>().onClick.AddListener(Apply);
        invertYAxis = transform.Find("InvertYToggle").gameObject.GetComponent<Toggle>();
        
        if (PlayerPrefs.HasKey("InvertYToggle"))
            invertYAxis.isOn = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;
        
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        bgmSlider.onValueChanged.AddListener(SetVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        Debug.Log("Loaded BGM Volume: " + bgmSlider.value);
        Debug.Log("Loaded SFX Volume: " + sfxSlider.value);
        
        SetVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);
    }
    public void Back()
    {
        if (PlayerPrefs.HasKey("previous-scene"))
            SceneManager.LoadScene(PlayerPrefs.GetString("previous-scene"));
    }

    public void Apply()
    {
        if (invertYAxis.isOn)
            PlayerPrefs.SetInt("InvertYToggle", 1);
        else
            PlayerPrefs.SetInt("InvertYToggle", 0);
    
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
        Debug.Log("Saved BGM Volume: " + bgmSlider.value);
        Debug.Log("Saved SFX Volume: " + sfxSlider.value);
        
        defaultSnapshot.TransitionTo(0.5f);

        // Find the CameraController and update isInverted
        var cameraController = Camera.main?.GetComponent<CameraController>();
        if (cameraController != null)
        {
            cameraController.UpdateInverted();
        }
    
        Back();
    }

    public void SetVolume(float volume)
    {
        float currentVolume;
        bgmMixer.GetFloat("BGMVolume", out currentVolume);
        Debug.Log("Current BGM Volume: " + currentVolume);

        float volumeInDB;

        if (volume > 0.0001f)
        {
            volumeInDB = Mathf.Log10(volume) * 20f;
        }
        else
        {
            volumeInDB = -80f;  // or whatever the minimum value your Audio Mixer allows is
        }

        bgmMixer.SetFloat("BGMVolume", volumeInDB);
    }

    public void SetSFXVolume(float volume)
    {
        bgmMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        float volumeInDB;

        if (volume > 0.0001f)
        {
            volumeInDB = Mathf.Log10(volume) * 20f;
        }
        else
        {
            volumeInDB = -80f;  // or whatever the minimum value your Audio Mixer allows is
        }

        bgmMixer.SetFloat("SFXVolume", volumeInDB);
    }
}
