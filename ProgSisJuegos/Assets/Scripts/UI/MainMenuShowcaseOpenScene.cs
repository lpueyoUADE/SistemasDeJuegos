using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuShowcaseOpenScene : MonoBehaviour
{
    public void OpenShowcase()
    {
        StartCoroutine(OpenScene("Showcase"));
    }

    IEnumerator OpenScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
    }
}
