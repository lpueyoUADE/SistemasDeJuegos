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
    [SerializeField] private List<SceneInformationDatabase> _scenesInformation;

    [Header("Disable objects after game starting")]
    [SerializeField] private List<GameObject> _objectsList;   

    [Header("Textures")]
    [SerializeField] private Image _sceneSplash;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _sceneNameToUser;
    [SerializeField] private TextMeshProUGUI _shipName;

    // Values
    private Dictionary<ShipType, GameObject> _showcaseShips = new Dictionary<ShipType, GameObject>();
    private AudioSource _audio;
    public int _currentSceneIndex = 0;

    public ShipType _currentShipType = ShipType.None;
    public int _currentShipIndex = 0;

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
        for (int i = 0; i < _scenesInformation.Count; i++)
        {
            foreach (ShipDatabase shipData in _scenesInformation[i].SceneShips)
            {
                var showcaseShip = Instantiate(shipData.ShowcasePrefab, _shipShowcaseTransform.position, shipData.ShowcasePrefab.transform.rotation, _shipShowcaseTransform);
                _showcaseShips.Add(shipData.Type, showcaseShip);
                showcaseShip.SetActive(false);
            }
        }

        ShipDatabase ship = _scenesInformation[_currentSceneIndex].SceneShips[0];
        _shipName.text = ship.Name;
        _currentShipType = ship.Type;
        _showcaseShips[_currentShipType].SetActive(true);
        UserSettings.UpdatePlayerShip(ship.Prefab);

        _sceneSplash.sprite = _scenesInformation[_currentSceneIndex].SceneSplashImage;
        _sceneNameToUser.text = _scenesInformation[_currentSceneIndex].SceneNameToUser;        
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

        _sceneSplash.sprite = _scenesInformation[_currentSceneIndex].SceneSplashImage;
        _sceneNameToUser.text = _scenesInformation[_currentSceneIndex].SceneNameToUser;
        _showcaseShips[_currentShipType].SetActive(false);
        ResetSelectedShip();
    }

    private void ResetSelectedShip()
    {
        _showcaseShips[_currentShipType].SetActive(false);
        _currentShipIndex = 0;
        ExecShipChanges();
    }

    public void ChangeShip(bool isNext)
    {
        _showcaseShips[_currentShipType].SetActive(false);

        if (isNext)
        {
            if (_currentShipIndex == _scenesInformation[_currentSceneIndex].SceneShips.Count - 1) _currentShipIndex = 0;
            else _currentShipIndex++;
        }

        else
        {
            if (_currentShipIndex == 0) _currentShipIndex = _scenesInformation[_currentSceneIndex].SceneShips.Count - 1;
            else _currentShipIndex--;
        }

        ExecShipChanges();
    }

    private void ExecShipChanges()
    {
        ShipDatabase ship = _scenesInformation[_currentSceneIndex].SceneShips[_currentShipIndex];
        _currentShipType = ship.Type;
        _shipName.text = ship.Name;
        _showcaseShips[_currentShipType].SetActive(true);
        UserSettings.UpdatePlayerShip(ship.Prefab);
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

        ScenarioPersistentData.Instance.UpdateScenarioData(_scenesInformation[_currentSceneIndex]);
        StartCoroutine(OpenScene(_scenesInformation[_currentSceneIndex].SceneName));
    }

    IEnumerator OpenScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
    }
}
