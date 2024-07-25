using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShaderUISpriteUpdateScore : MonoBehaviour
{
    [SerializeField] private Color _overlayColor = Color.white;
    private float _currentAnimDuration = 0;
    private Image _sprite;
    private Material _material;

    private void Start()
    {
        _sprite = GetComponent<Image>();
        _material = _sprite.material;
        _material.SetFloat("_CurrentLerpValue", 0);
        _material.SetColor("_OverlayColor", _overlayColor);

        UIEvents.OnScoreUpdate += StartAnim;
    }

    private void OnDestroy()
    {
        UIEvents.OnScoreUpdate -= StartAnim;
    }

    private void Update()
    {
        if (_currentAnimDuration > 0)
        {
            _currentAnimDuration -= Time.deltaTime;
            _material.SetFloat("_CurrentLerpValue", 1);
        }

        else _material.SetFloat("_CurrentLerpValue", 0);
    }

    public void StartAnim(float newScore)
    {
        if (newScore <= 0) return;
        _currentAnimDuration = 1;
    }
}
