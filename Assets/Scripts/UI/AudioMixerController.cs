using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    private const string MasterVolumeKey = "MasterVolume";
    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f); // �⺻�� 0.5
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        m_MusicMasterSlider.value = masterVolume;
        m_MusicBGMSlider.value = bgmVolume;
        m_MusicSFXSlider.value = sfxVolume;

        ApplyVolumeToMixer(masterVolume, bgmVolume, sfxVolume);
    }
    
    private void ApplyVolumeToMixer(float masterVolume, float bgmVolume, float sfxVolume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }
}
