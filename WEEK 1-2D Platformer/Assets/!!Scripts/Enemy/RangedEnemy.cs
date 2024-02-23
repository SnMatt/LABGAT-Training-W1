using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCD;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;

    [SerializeField] private float _colliderDistance;
    [SerializeField] private CapsuleCollider2D _collider;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _fireballs;

    [SerializeField] private LayerMask _playerLayer;
    private float _CDTimer = 100f;

    [SerializeField] private AudioClip _fireballSFX;

    private Animator _anim;
    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        _CDTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (_CDTimer >= _attackCD)
            {
                _CDTimer = 0;
                _anim.SetTrigger("RangeAttack");
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangeAttack()
    {
        AudioManager.Instance.PlaySound(_fireballSFX);
        _CDTimer = 0;

        _fireballs[FindFireball()].transform.position = _firePoint.position;
        _fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < _fireballs.Length; i++)
        {
            if (!_fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector2(_collider.bounds.size.x * _range, _collider.bounds.size.y),
            0, Vector2.left, 0, _playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector2(_collider.bounds.size.x * _range, _collider.bounds.size.y));
    }
}
