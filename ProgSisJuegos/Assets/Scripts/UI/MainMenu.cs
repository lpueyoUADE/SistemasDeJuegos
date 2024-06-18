using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UISoundsDatabase _soundsDatabase;

    [Header("Menu settings and references")]
    [SerializeField] private ScenesDatabase _scenesData;
    [SerializeField] private VerticalLayoutGroup _mainMenu;
    [SerializeField] private VerticalLayoutGroup _controlsMenu;
    [SerializeField] private VerticalLayoutGroup _optionsMenu;

    [SerializeField] private SpriteRenderer _shipSprite;
    [SerializeField] private List<ShipDatabase> _ships;
    private AudioSource _audio;

    public AudioSource Audio => _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ShipChange(bool isNext)
    {

    }

    public void ChangeMenu(int newSection)
    {
        _mainMenu.gameObject.SetActive(false);
        _controlsMenu.gameObject.SetActive(false);
        _optionsMenu.gameObject.SetActive(false);

        switch (newSection)
        {
            default: _mainMenu.gameObject.SetActive(true); break;
            case 1: _controlsMenu.gameObject.SetActive(true); break;
            case 2: _optionsMenu.gameObject.SetActive(true); break;
        }
    }

    public void OpenTestScene()
    {
        StartCoroutine(OpenScene(_scenesData.SceneTestScene));
    }

    IEnumerator OpenScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
    }
}
