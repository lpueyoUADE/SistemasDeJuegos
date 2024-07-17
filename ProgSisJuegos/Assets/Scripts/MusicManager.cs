using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audio.clip = ScenarioPersistentData.SceneData.SceneMusic;
        _audio.Play();
    }
}
