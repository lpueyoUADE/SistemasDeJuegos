using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // References
    [SerializeField] private HorizontalLayoutGroup _UIWeaponsBox;
    [SerializeField] private GameObject _weaponSingleItem;
    [SerializeField] private GameObject _weaponSelector;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private TMPro.TMP_Text _scoreText;

    // Values
    private GameObject _createdWeaponSelector;
    private int _score = 0;

    // Values
    private Dictionary<WeaponType, GameObject> _currentWeaponsInUI = new Dictionary<WeaponType, GameObject>();

    // Instance
    private static UIEvents _uiEvents;
    private static UIManager _instance;
    public UIManager Instance => _instance;    

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }

        _uiEvents = new UIEvents();

        // Hook UI events
        UIEvents.OnAllWeaponsInitialize += CreateWeaponsList;

        UIEvents.OnAddInventoryWeapon += AddInventoryWeapon;
        UIEvents.OnRemoveInventoryWeapon += RemoveInventoryWeapon;

        UIEvents.OnWeaponSwap += WeaponSwap;

        /*
        instance = this;
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController.OnHpChanged += UpdateHpBar;
        _playerController.OnWeaponChanged += UpdateCurrentWeaponIcon;
        */
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        _healthBar.value = _healthBar.maxValue;*/
        _createdWeaponSelector = Instantiate(_weaponSelector);
    }

    private void UpdateHpBar(int currentHP)
    {
        _healthBar.value = currentHP;
    }

    private void UpdateCurrentWeaponIcon()
    {
        //_currentWeapon.sprite = _playerController.ShipCurrentWeapon.WeaponIcon;
    }

    private void AddScore(int value)
    {
        _score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    // Hooked events
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
