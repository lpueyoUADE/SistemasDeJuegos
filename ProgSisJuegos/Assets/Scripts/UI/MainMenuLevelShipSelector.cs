using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLevelShipSelector : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuObject;
    [SerializeField] private Transform _shipShowcaseTransform;

    [Header("Disable objects after game starting")]
    [SerializeField] private List<GameObject> _objectsList;

    [Header("Scenes/Ship Data")]
    [SerializeField] private List<ShipDatabase> _shipsList;
    [SerializeField] private List<SceneInformationDatabase> _scenesInformation;

    [Header("Textures")]
    [SerializeField] private Image _sceneSplash;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _sceneNameToUser;
    [SerializeField] private TextMeshProUGUI _shipName;

    // Values
    private List<GameObject> _spawnedShipsForShowcase = new List<GameObject>();
    private AudioSource _audio;
    private int _currentSceneIndex = 0;
    private int _currentShipIndex = 0;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.Play();
    }

    private void OnEnable()
    {
        _audio.Play();
    }

    private void OnDisable()
    {
        if (_shipShowcaseTransform != null) _shipShowcaseTransform.transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        _shipShowcaseTransform.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    private void Start()
    {
        for (int i = 0; i < _shipsList.Count; i++)
        {
            var showcaseship = Instantiate(_shipsList[i].ShowcasePrefab, _shipShowcaseTransform.position, _shipsList[i].ShowcasePrefab.transform.rotation, _shipShowcaseTransform);
            _spawnedShipsForShowcase.Add(showcaseship);
            showcaseship.SetActive(false);
        }

        _sceneSplash.sprite = _scenesInformation[_currentSceneIndex].SplashImage;
        _sceneNameToUser.text = _scenesInformation[_currentSceneIndex].SceneNameToUser;

        _spawnedShipsForShowcase[_currentShipIndex].SetActive(true);
        _shipName.text = _shipsList[_currentShipIndex].Name;
        UserSettings.Instance.playership = _shipsList[_currentShipIndex].Prefab;
    }

    public void ChangeLevel(bool isNext)
    {
        if (isNext)
        {
            if (_currentSceneIndex == _scenesInformation.Count - 1) _currentSceneIndex = 0;
            else _currentSceneIndex++;
        }

        else
        {
            if (_currentSceneIndex == 0) _currentSceneIndex = _scenesInformation.Count - 1;
            else _currentSceneIndex--;
        }

        _sceneSplash.sprite = _scenesInformation[_currentSceneIndex].SplashImage;
        _sceneNameToUser.text = _scenesInformation[_currentSceneIndex].SceneNameToUser;
    }

    public void ChangeShip(bool isNext)
    {
        _spawnedShipsForShowcase[_currentShipIndex].SetActive(false);

        if (isNext)
        {
            if (_currentShipIndex == _shipsList.Count - 1) _currentShipIndex = 0;
            else _currentShipIndex++;
        }

        else
        {
            if (_currentShipIndex == 0) _currentShipIndex = _shipsList.Count - 1;
            else _currentShipIndex--;
        }

        _spawnedShipsForShowcase[_currentShipIndex].SetActive(true);
        _shipName.text = _shipsList[_currentShipIndex].Name;

        UserSettings.Instance.playership = _shipsList[_currentShipIndex].Prefab;
    }

    public void BackToMainMenu()
    {
        _mainMenuObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        for (int i = 0; i < _objectsList.Count; i++)
            _objectsList[i].SetActive(false);

        StartCoroutine(OpenScene(_scenesInformation[_currentSceneIndex].SceneName));
    }

    IEnumerator OpenScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
    }
}
