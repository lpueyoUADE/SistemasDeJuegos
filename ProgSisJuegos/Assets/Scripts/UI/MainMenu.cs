using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelShipSelector;

    [Header("Sound settings")]
    [SerializeField] private UISoundsDatabase _soundsDatabase;

    [Header("Menu settings and references")]
    [SerializeField] private VerticalLayoutGroup _mainMenu;
    [SerializeField] private VerticalLayoutGroup _optionsMenu;

    [Header("Individual menu components")]
    [SerializeField] private GameObject _mainMenuButtons;
    [SerializeField] private GameObject _optionsMenuButtons;
    [SerializeField] private GameObject _optionsSoundSettings;

    [Header("Individual options components")]
    [SerializeField] private GameObject _optionsAudioButton;
    [SerializeField] private GameObject _optionsControlsButton;
    [SerializeField] private GameObject _optionsBackButton;

    [Header("Audio sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Slider _uiSlider;
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private AudioSource _effectsAudio;
    [SerializeField] private AudioSource _uiAudio;
    [SerializeField] private AudioSource _musicAudio;

    private AudioSource _audio;
    public AudioSource Audio => _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audio.Play(); 
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MenuMain()
    {
        _mainMenuButtons.SetActive(true);
        _optionsMenuButtons.SetActive(false);
    }

    public void MenuOptions()
    {
        _mainMenuButtons.SetActive(false);
        _optionsMenuButtons.SetActive(true);
    }

    public void MenuAudioMixer()
    {
        _optionsAudioButton.SetActive(false);
        _optionsControlsButton.SetActive(false);
        _optionsBackButton.SetActive(false);
        _optionsSoundSettings.SetActive(true);
        AudioMixerGetValues();
    }

    private void AudioMixerGetValues()
    {
        List<float> mixerValues = UserSettings.Instance.AudioMixerValues();

        _masterSlider.value = mixerValues[0];
        _effectsSlider.value = mixerValues[1];
        _uiSlider.value = mixerValues[2];
        _musicSlider.value = mixerValues[3];
    }

    public void AudioMixerMaster()
    {
        UserSettings.Instance.AudioMixerMaster(_masterSlider.value);
    }

    public void AudioMixerEffects()
    {
        UserSettings.Instance.AudioMixerEffects(_effectsSlider.value);
    }

    public void AudioMixerUI()
    {
        UserSettings.Instance.AudioMixerUI(_uiSlider.value);
    }

    public void AudioMixerMusic()
    {
        UserSettings.Instance.AudioMixerMusic(_musicSlider.value);
    }

    public void AudioMixerReset()
    {
        _masterSlider.value = 0;
        _effectsSlider.value = 0;
        _uiSlider.value = 0;
        _musicSlider.value = 0;
    }

    public void AudioMixerPlayPause(string target)
    {
        switch (target)
        {
            case "sfx": 
                if (_effectsAudio.isPlaying) _effectsAudio.Stop();
                else _effectsAudio.Play();
                return;
            case "ui":
                if (_uiAudio.isPlaying) _uiAudio.Stop();
                else _uiAudio.Play();
                return;
            case "music":
                if (_musicAudio.isPlaying) _musicAudio.Stop();
                else _musicAudio.Play();
                return;
        }
    }

    public void MenuAudioMixerExit()
    {
        _optionsSoundSettings.SetActive(false);
        _optionsAudioButton.SetActive(true);
        _optionsControlsButton.SetActive(true);
        _optionsBackButton.SetActive(true);
    }

    public void ChangeMenu(int newSection)
    {
        _mainMenu.gameObject.SetActive(false);
        _optionsMenu.gameObject.SetActive(false);

        switch (newSection)
        {
            default: 
                _mainMenu.gameObject.SetActive(true); 
                break;
            case 1:
                _optionsMenu.gameObject.SetActive(true); 
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToLevelShipSelector()
    {
        _levelShipSelector.SetActive(true);
        gameObject.SetActive(false);
    }
}
