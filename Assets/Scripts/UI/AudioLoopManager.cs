using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLoopManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup bgmMixerGroup; // BGM AudioMixerGroup ÂüÁ¶

    private void Start()
    {
        LoopAllBGMAudioSources();
    }

    private void LoopAllBGMAudioSources()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (var source in audioSources)
        {
            if (source.outputAudioMixerGroup == bgmMixerGroup)
            {
                source.loop = true;
            }
        }
    }
}
