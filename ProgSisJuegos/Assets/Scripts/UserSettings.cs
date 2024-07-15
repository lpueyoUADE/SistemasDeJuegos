using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UserSettings : MonoBehaviour
{
    private static UserSettings instance;
    [SerializeField] private static ShipBase _playership;
    [SerializeField] private AudioMixer _mixer;

    public static UserSettings Instance => instance;
    public ShipBase PlayerShip => _playership;
    public AudioMixer Mixer => _mixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
            instance = this;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void UpdatePlayerShip(ShipBase newShip)
    {
        _playership = newShip;
    }

    public void AudioMixerMaster(float value)
    {
        Mixer.SetFloat("master", value);
    }

    public void AudioMixerEffects(float value)
    {
        Mixer.SetFloat("sfx", value);
    }

    public void AudioMixerUI(float value)
    {
        Mixer.SetFloat("ui", value);
    }

    public void AudioMixerMusic(float value)
    {
        Mixer.SetFloat("music", value);
    }

    public List<float> AudioMixerValues()
    {
        Mixer.GetFloat("master", out float master);
        Mixer.GetFloat("sfx", out float sfx);
        Mixer.GetFloat("ui", out float ui);
        Mixer.GetFloat("music", out float music);

        return new List<float> { master, sfx, ui, music };
    }
}
