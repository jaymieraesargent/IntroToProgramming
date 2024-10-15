using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    //public AudioSource audioSource;
    //public void ChangeAudioSourceVolume(float volume)
    //{
    //    audioSource.volume = volume;
    //}
    public AudioMixer audioMixer;
    string _mixerChannel;
    Text _channelVolumePercent;
    public void CurrentMixer(string name)
    {
        _mixerChannel = name;
    }  
    public void GetText(Text uiText)
    {
        _channelVolumePercent = uiText;
    }
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(_mixerChannel, volume);
        ChangeTextValue(volume);
    }
    void ChangeTextValue(float volume)
    {
        _channelVolumePercent.text = $"{Mathf.Clamp01((volume+80)/100):P0}";
    }
}
