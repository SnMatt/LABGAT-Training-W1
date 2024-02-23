using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage 
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    private Animator _anim;

    private Collider2D _collider;
    private bool _hit;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    public void ActivateProjectile()
    {
        _hit = false;
        gameObject.SetActive(true);
        Invoke("Deactivate", _lifetime);
        _collider.enabled = true;
    }

    private void Update()
    {
        if (_hit) return;

        float movement = _speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;

        base.OnTriggerEnter2D(collision);

        _collider.enabled = false;
        Deactivate();

        if (_anim != null)
            _anim.SetTrigger("Explode");
        else
            gameObject.SetActive(false);
    }
}
