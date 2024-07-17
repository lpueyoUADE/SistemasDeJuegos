using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Showcase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uitext;
    [SerializeField] private List<GameObject> objectsList = new List<GameObject>();
    [SerializeField] private List<string> namesList = new List<string>();
    [SerializeField] private List<Vector3> offsets = new List<Vector3>();
    [SerializeField] private int index = 0;

    [SerializeField] private AudioClip _changeObjectCue;

    private AudioSource _audio;
    private Vector3 _direction;

    public float axis => Input.GetAxisRaw("Horizontal");

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        uitext.text = namesList[0];
        _direction = objectsList[0].transform.position + offsets[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { BackToMM(); }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (index == 0) index = objectsList.Count - 1;
            else index--;
            _audio.PlayOneShot(_changeObjectCue);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (index >= objectsList.Count - 1) index = 0;
            else index++;
            _audio.PlayOneShot(_changeObjectCue);
        }

        uitext.text = namesList[index];
        _direction = objectsList[index].transform.position + offsets[index];
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.MoveTowards(transform.position, _direction, 1);
        transform.position = direction;
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
