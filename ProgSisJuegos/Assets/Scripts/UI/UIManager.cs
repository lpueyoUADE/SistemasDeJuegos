using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private TMPro.TMP_Text _scoreText;
    private int _score = 0;
    private PlayerController _playerController;
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController.OnHpChanged += UpdateHpBar;
        _playerController.OnWeaponChanged += UpdateCurrentWeaponIcon;
    }
    // Start is called before the first frame update
    void Start()
    {
        _healthBar.value = _healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHpBar(int currentHP)
    {
        _healthBar.value = currentHP;
    }

    private void UpdateCurrentWeaponIcon()
    {
        _currentWeapon.sprite = _playerController.ShipCurrentWeapon.WeaponIcon;
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
}
