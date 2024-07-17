using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UISoundsDatabase _soundsDatabase;

    [Header("References")]
    [SerializeField] private HorizontalLayoutGroup _UIWeaponsBox;
    [SerializeField] private GameObject _weaponSingleItem;
    [SerializeField] private GameObject _weaponSelector;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _endScreenText;

    [Header("Buttons")]
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _audioSettingsButton;

    [Header("Objects")]
    [SerializeField] private GameObject _mainButtonsObject;
    [SerializeField] private GameObject _audioMenuObject;
    [SerializeField] private GameObject _pauseText;

    [Header("Audio Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Slider _uiSlider;
    [SerializeField] private Slider _musicSlider;

    // Values
    private AudioSource _audio;
    private GameObject _createdWeaponSelector;
    private Dictionary<WeaponType, GameObject> _currentWeaponsInUI = new Dictionary<WeaponType, GameObject>();
    private bool gameEnded;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        SubEvents();
    }

    void Start()
    {
        _createdWeaponSelector = Instantiate(_weaponSelector);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
        UIEvents.OnPlayUISound += PlayUISound;
        UIEvents.OnAllWeaponsInitialize += CreateWeaponsList;
        UIEvents.OnAddInventoryWeapon += AddInventoryWeapon;
        UIEvents.OnRemoveInventoryWeapon += RemoveInventoryWeapon;
        UIEvents.OnWeaponSwap += WeaponSwap;
        UIEvents.OnPlayerSpawn += OnPlayerSpawn;
        UIEvents.OnPlayerHPUpdate += UpdateHpBar;        
        UIEvents.OnGameEnded += ShowEndScreen;
        UIEvents.OnScoreUpdate += UpdateScore;
        UIEvents.OnGamePaused += PauseGame;

        PlayerEvents.OnWeaponAmmoUpdate += UpdateWeaponAmmo;
    }

    private void UnsubEvents()
    {
        UIEvents.OnPlayUISound -= PlayUISound;
        UIEvents.OnAllWeaponsInitialize -= CreateWeaponsList;
        UIEvents.OnAddInventoryWeapon -= AddInventoryWeapon;
        UIEvents.OnRemoveInventoryWeapon -= RemoveInventoryWeapon;
        UIEvents.OnWeaponSwap -= WeaponSwap;
        UIEvents.OnPlayerSpawn -= OnPlayerSpawn;
        UIEvents.OnPlayerHPUpdate -= UpdateHpBar;        
        UIEvents.OnGameEnded -= ShowEndScreen;
        UIEvents.OnScoreUpdate -= UpdateScore;
        UIEvents.OnGamePaused -= PauseGame;

        PlayerEvents.OnWeaponAmmoUpdate -= UpdateWeaponAmmo;
    }

    private void PlayUISound(AudioClip clip, float volume = 1)
    {
        _audio.PlayOneShot(clip, volume);
    }

    private void OnPlayerSpawn()
    {

    }

    private void UpdateHpBar(float currentLife, float maxLife)
    {
        _healthBar.fillAmount = currentLife/maxLife;
    } 

    private void ShowEndScreen(bool win)
    {
        _endScreen.SetActive(true);
        if (win) _endScreenText.text = "Level Completed!";
        gameEnded = true;
    }

    private void UpdateScore(float newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    private void CreateWeaponsList(List<WeaponDatabase> weapons)
    {
        foreach (WeaponDatabase weapon in weapons)
        {
            if (_currentWeaponsInUI.ContainsKey(weapon.WeapType)) return;
            if (weapon.WeapType == WeaponType.EnemyBlueRail) return;

            GameObject newWeaponItem = Instantiate(_weaponSingleItem);
            Image weapImage = newWeaponItem.GetComponent<Image>();
            weapImage.sprite = weapon.WeapIcon;
            _currentWeaponsInUI.Add(weapon.WeapType, newWeaponItem);

            //newWeaponItem.transform.SetParent(_UIWeaponsBox.transform, false);
            newWeaponItem.SetActive(false);
        }
    }

    private void UpdateWeaponAmmo(WeaponType type, int ammo)
    {
        if (!_currentWeaponsInUI.ContainsKey(type)) return;

        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        weapon.GetComponentInChildren<TextMeshProUGUI>().text = ammo.ToString();
    }

    private void WeaponSwap(WeaponType type)
    {
         if (!_currentWeaponsInUI.ContainsKey(type)) return;
        
        // Move weapon selector to selection
        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        _createdWeaponSelector.transform.SetParent(weapon.transform, false);
        _audio.PlayOneShot(_soundsDatabase.UISoundChangeWeapon);
    }        
    
    private void AddInventoryWeapon(WeaponType type, int ammo)
    {
        // Check if the weapon is on the allowed list
        if (!_currentWeaponsInUI.ContainsKey(type)) return;

        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        weapon.SetActive(true);
        UpdateWeaponAmmo(type, ammo);
        weapon.transform.SetParent(_UIWeaponsBox.transform, false);
    }    
    
    private void RemoveInventoryWeapon(WeaponType type)
    {
        // Check if the weapon is on the allowed list
        if (!_currentWeaponsInUI.ContainsKey(type)) return;

        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        weapon.SetActive(false);
    }

    private void PauseGame(bool isPaused)
    {
        if (gameEnded == false)
        {
            _pauseMenu.SetActive(isPaused);
            PlayUISound(_soundsDatabase.UISoundPause);
        }        
    }

    public void ResumeGame()
    {
        if (gameEnded == false)
        {
            GameManagerEvents.OnGameResume?.Invoke();
        }        
    }

    public void AudioSettings()
    {
        _mainButtonsObject.SetActive(false);
        _pauseText.SetActive(false);
        _audioMenuObject.SetActive(true);

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

    public void BackToPauseMenu()
    {
        _audioMenuObject.SetActive(false);
        _mainButtonsObject.SetActive(true);
        _pauseText.SetActive(true);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }  
}
