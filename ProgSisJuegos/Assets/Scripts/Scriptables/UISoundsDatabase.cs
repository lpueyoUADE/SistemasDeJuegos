using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUISoundsData", menuName = "Databases/Sounds/UI")]
public class UISoundsDatabase : ScriptableObject
{
    [SerializeField] private AudioClip _pause;
    [SerializeField] private AudioClip _genericButtonClick;
    [SerializeField] private AudioClip _changeWeapon;

    public AudioClip UISoundPause => _pause;
    public AudioClip UISoundGenericButtonClick => _genericButtonClick;
    public AudioClip UISoundChangeWeapon => _changeWeapon;
}
