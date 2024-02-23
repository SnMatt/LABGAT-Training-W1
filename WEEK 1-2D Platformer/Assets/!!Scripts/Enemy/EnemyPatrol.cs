using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    [SerializeField] private Transform _enemyTf;

    [SerializeField] private float _idleDuration;
    private float _idleTimer;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private Animator _anim;

    private Vector3 _initScale;
    private bool _isMovingLeft;

    private void Awake()
    {
        _initScale = _enemyTf.localScale;
    }
    private void OnDisable()
    {
        _anim.SetBool("IsMoving", false);
    }
    private void Update()
    {
        if(_isMovingLeft)
        {
            if (_enemyTf.position.x >= _leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                ChangeDirection();
            }
        }else
        {
            if (_enemyTf.position.x <= _rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        _anim.SetBool("IsMoving", false);

        _idleTimer += Time.deltaTime;

        if(_idleTimer >= _idleDuration)
        {
           _isMovingLeft = !_isMovingLeft;
            _idleTimer = 0;
        }
    }

    private void MoveInDirection(int direction)
    {
        _anim.SetBool("IsMoving", true);

        _enemyTf.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);

        _enemyTf.position = new Vector2(_enemyTf.position.x + _moveSpeed * direction * Time.deltaTime, _enemyTf.position.y);
    }
}
