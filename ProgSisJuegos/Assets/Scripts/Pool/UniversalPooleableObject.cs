using System;
using Unity.VisualScripting;
using UnityEngine;

public class UniversalPooleableObject : MonoBehaviour
{
    public UniversalPoolObjectType objectType = UniversalPoolObjectType.Audio;
    private AudioSource _audioSource;
    public Action OnDisabled;

    public bool IsPlaying => _audioSource.isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioSource.clip != null && !IsPlaying) gameObject.SetActive(false);
    }

    public void UpdateAudioAndPlay(AudioClip clip, float volume = 1, float maxDistance = 40,float spatialBlend = 0.8f)
    {
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.maxDistance = maxDistance;
        _audioSource.spatialBlend = spatialBlend;

        _audioSource.Play();
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke();
        _audioSource.clip = null;
    }
}
