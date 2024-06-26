using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowcaseCameraController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { BackToMM(); }
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);

        rb.velocity = direction;
    }

    public void BackToMM()
    {
        StartCoroutine(OpenScene("MainMenu"));
    }

    IEnumerator OpenScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
    }
}
