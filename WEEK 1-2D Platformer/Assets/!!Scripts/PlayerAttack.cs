using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _playerMovement;
    private SpriteRenderer _sr;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _fireballObjects;

    [SerializeField] private float _attackCD;
    private float _attackCDTimer = 100;

    [SerializeField] private AudioClip _fireballSFX;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && _attackCDTimer > _attackCD && _playerMovement.CanAttack())
        {
            Attack();
        }

        _attackCDTimer += Time.deltaTime;
    }

    private void Attack()
    {
        AudioManager.Instance.PlaySound(_fireballSFX);
        _anim.SetTrigger("Attack");
        _attackCDTimer = 0;

        _fireballObjects[FindFireball()].transform.position = _firePoint.position;
        _fireballObjects[FindFireball()].GetComponent<Projectile>().SetDirection(_sr.flipX);
    }

    private int FindFireball()
    {
        for (int i = 0; i < _fireballObjects.Length; i++)
        {
            if (!_fireballObjects[i].activeInHierarchy) return i;
        }

        return 0;
    }
}
