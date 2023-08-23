using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer bgmMixer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySavedSettings();
    }

    public void ApplySavedSettings()
    {
        if (bgmMixer)
        {
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            SetVolume(savedBGMVolume);
        }
        // Add other settings as needed
    }

    public void SetVolume(float volume)
    {
        float volumeInDB;
        if (volume > 0.0001f)
        {
            volumeInDB = Mathf.Log10(volume) * 20f;
        }
        else
        {
            volumeInDB = -80f; 
        }

        bgmMixer.SetFloat("BGMVolume", volumeInDB);
    }
}
