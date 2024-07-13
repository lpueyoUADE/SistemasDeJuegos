using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelShipSelector;

    [Header("Sound settings")]
    [SerializeField] private UISoundsDatabase _soundsDatabase;

    [Header("Menu settings and references")]
    [SerializeField] private VerticalLayoutGroup _mainMenu;
    [SerializeField] private VerticalLayoutGroup _controlsMenu;
    [SerializeField] private VerticalLayoutGroup _optionsMenu;

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

    public void ChangeMenu(int newSection)
    {
        _mainMenu.gameObject.SetActive(false);
        _controlsMenu.gameObject.SetActive(false);
        _optionsMenu.gameObject.SetActive(false);

        switch (newSection)
        {
            default: 
                _mainMenu.gameObject.SetActive(true); 
                break;
            case 1: 
                _controlsMenu.gameObject.SetActive(true); 
                break;
            case 2: 
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
