using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private AudioClip _changeSFX;
    [SerializeField] private AudioClip _selectSFX;
    [SerializeField] private RectTransform[] _optionsPos;
    private RectTransform _rect;
    private int _currPos;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePos(-1);
        }else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePos(1);
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangePos(int change)
    {
        _currPos += change;
        AudioManager.Instance.PlaySound(_changeSFX);

        if (_currPos < 0)
            _currPos = _optionsPos.Length - 1;
        else if (_currPos > _optionsPos.Length - 1)
            _currPos = 0;

        _rect.position = new Vector2(_rect.position.x, _optionsPos[_currPos].position.y);
    }

    private void Interact()
    {
        AudioManager.Instance.PlaySound(_selectSFX);

        _optionsPos[_currPos].GetComponent<Button>().onClick.Invoke();
    }

}
