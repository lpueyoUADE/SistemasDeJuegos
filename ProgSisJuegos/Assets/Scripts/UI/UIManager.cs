using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UISoundsDatabase _soundsDatabase;

    // References
    [SerializeField] private HorizontalLayoutGroup _UIWeaponsBox;
    [SerializeField] private GameObject _weaponSingleItem;
    [SerializeField] private GameObject _weaponSelector;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private TextMeshProUGUI _scoreText;

    // Values
    private AudioSource _audio;
    private GameObject _createdWeaponSelector;
    private Dictionary<WeaponType, GameObject> _currentWeaponsInUI = new Dictionary<WeaponType, GameObject>();

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        SubEvents();
    }

    void Start()
    {
        /*
        _healthBar.value = _healthBar.maxValue;*/
        _createdWeaponSelector = Instantiate(_weaponSelector);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
        UIEvents.OnAllWeaponsInitialize += CreateWeaponsList;
        UIEvents.OnAddInventoryWeapon += AddInventoryWeapon;
        UIEvents.OnRemoveInventoryWeapon += RemoveInventoryWeapon;
        UIEvents.OnWeaponSwap += WeaponSwap;
        UIEvents.OnPlayerSpawn += OnPlayerSpawn;
        UIEvents.OnPlayerHPUpdate += UpdateHpBar;
        //UIEvents.OnPlayerDeath += 
        UIEvents.OnScoreUpdate += UpdateScore;
    }

    private void UnsubEvents()
    {
        UIEvents.OnAllWeaponsInitialize -= CreateWeaponsList;
        UIEvents.OnAddInventoryWeapon -= AddInventoryWeapon;
        UIEvents.OnRemoveInventoryWeapon -= RemoveInventoryWeapon;
        UIEvents.OnWeaponSwap -= WeaponSwap;
        UIEvents.OnPlayerSpawn -= OnPlayerSpawn;
        UIEvents.OnPlayerHPUpdate -= UpdateHpBar;
        //UIEvents.OnPlayerDeath -= 
        UIEvents.OnScoreUpdate -= UpdateScore;
    }

    private void OnPlayerSpawn()
    {

    }

    private void UpdateHpBar(float damage)
    {
        _healthBar.fillAmount -= damage;
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

            newWeaponItem.transform.SetParent(_UIWeaponsBox.transform, false);
            newWeaponItem.SetActive(false);
        }
    }

    private void WeaponSwap(WeaponType type)
    {
         if (!_currentWeaponsInUI.ContainsKey(type)) return;
        
        // Move weapon selector to selection
        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        _createdWeaponSelector.transform.SetParent(weapon.transform, false);
        _audio.PlayOneShot(_soundsDatabase.UISoundChangeWeapon);
    }        
    
    private void AddInventoryWeapon(WeaponType type)
    {
        // Check if the weapon is on the allowed list
        if (!_currentWeaponsInUI.ContainsKey(type)) return;

        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        weapon.SetActive(true);
    }    
    
    // Maybe replace this with a bool in InventoryWeapon to manage adding/removing?
    private void RemoveInventoryWeapon(WeaponType type)
    {
        // Check if the weapon is on the allowed list
        if (!_currentWeaponsInUI.ContainsKey(type)) return;

        _currentWeaponsInUI.TryGetValue(type, out var weapon);
        weapon.SetActive(false);
    }
}
