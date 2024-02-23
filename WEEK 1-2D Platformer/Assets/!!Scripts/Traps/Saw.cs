using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    private bool _isMovingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private void Start()
    {
        _leftEdge = transform.position.x - _moveDistance;
        _rightEdge = transform.position.x + _moveDistance;
    }

    private void Update()
    {
        if(_isMovingLeft)
        {
            if (transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y);
            }
            else
                _isMovingLeft = false;
        }else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y);

            }
            else
                _isMovingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
