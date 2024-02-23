using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _playerHP;
    [SerializeField] private Image _totalHPBar;
    [SerializeField] private Image _currHPBar;

    private void Start()
    {
        _totalHPBar.fillAmount = 0.3f;
    }

    private void Update()
    {
        UpdateHPUI();
    }

    public void UpdateHPUI()
    {
        _currHPBar.fillAmount = _playerHP.CurrHealth / 10f;
    }
}
