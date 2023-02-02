using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void Start()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void SetEffect(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("effects", volume);
    }
}
