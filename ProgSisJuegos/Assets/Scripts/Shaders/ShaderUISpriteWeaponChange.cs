using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShaderUISpriteWeaponChange : MonoBehaviour
{
    [SerializeField] private float _animDuration = 0.35f;
    [SerializeField] private float _speed = 1;

    private float _currentAnimDuration;
    private Image _sprite;
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<Image>();
        _material = _sprite.material;
        _material.SetFloat("_MovementLerp", 0.5f);
        _material.SetFloat("_Speed", 0);
        UIEvents.OnWeaponSwapLeftRight += OnWeaponChange;
    }

    private void Update()
    {
        if (_currentAnimDuration > 0) _currentAnimDuration -= Time.deltaTime;

        else
        {
            _material.SetFloat("_MovementLerp", 0.5f);
            _material.SetFloat("_Speed", 0);
        }
    }

    private void OnDestroy()
    {
        UIEvents.OnWeaponSwapLeftRight -= OnWeaponChange;
    }

    private void OnWeaponChange(bool isLeft)
    {
        _currentAnimDuration = _animDuration;

        if (isLeft) _material.SetFloat("_MovementLerp", 0);
        else _material.SetFloat("_MovementLerp", 1);

        _material.SetFloat("_Speed", _speed);
    }
}
