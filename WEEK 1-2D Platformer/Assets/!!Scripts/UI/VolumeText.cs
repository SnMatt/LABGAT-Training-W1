using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string _volumeName;
    [SerializeField] private string _textDesc;
    private Text _txt;

    private void Awake()
    {
        _txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        _txt.text = _textDesc + PlayerPrefs.GetFloat(_volumeName) * 100;
    }
}
