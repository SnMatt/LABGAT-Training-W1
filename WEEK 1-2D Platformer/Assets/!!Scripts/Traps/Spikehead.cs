using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _checkDelay;

    private Vector3[] _directions = new Vector3[4];
    private Vector3 _destination;
    private float _checkTimer;
    private bool _attacking;

    [SerializeField] private AudioClip _impactSFX;
    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (_attacking)
            transform.Translate(_destination * _speed * Time.deltaTime);
        else
        {
            _checkTimer += Time.deltaTime;
            if (_checkTimer > _checkDelay)
                CheckPlayer();
        }
    }

    private void CheckPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < 4; i++)
        {
            Debug.DrawRay(transform.position, _directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], _detectRange, _playerLayer);

            if(hit.collider != null && !_attacking)
            {
                _attacking = true;
                _destination = _directions[i];
                _checkTimer = 0;
            }
        }
    }
    private void Stop()
    {
        _destination = transform.position;
        _attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        AudioManager.Instance.PlaySound(_impactSFX);
        Stop();
    }

    private void CalculateDirections()
    {
        _directions[0] = transform.right * _detectRange;
        _directions[1] = -transform.right * _detectRange;
        _directions[2] = transform.up * _detectRange;
        _directions[3] = -transform.up * _detectRange;
    }
}
