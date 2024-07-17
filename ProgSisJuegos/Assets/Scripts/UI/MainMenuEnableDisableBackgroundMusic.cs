using UnityEngine;

public class MainMenuEnableDisableBackgroundMusic : MonoBehaviour
{
    public GameObject musicObject;

    private void OnEnable()
    {
        musicObject.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        musicObject.gameObject.SetActive(true);
    }
}
