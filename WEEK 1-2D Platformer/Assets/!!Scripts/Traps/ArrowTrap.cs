using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float _attackCD;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _arrows;
    private float _CDTimer;

    [SerializeField] private AudioClip _shootSFX;

    private void Attack()
    {
        AudioManager.Instance.PlaySound(_shootSFX);

        _CDTimer = 0;

        _arrows[GetArrow()].transform.position = _firePoint.position;
        _arrows[GetArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int GetArrow()
    {
        for (int i = 0; i < _arrows.Length; i++)
        {
            if (!_arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        _CDTimer += Time.deltaTime;

        if (_CDTimer > _attackCD)
            Attack();
    }
}
