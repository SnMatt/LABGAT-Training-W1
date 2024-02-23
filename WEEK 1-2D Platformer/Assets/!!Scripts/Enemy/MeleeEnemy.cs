using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCD;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private int _damage;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private LayerMask _playerLayer;
    private float _CDTimer = 100f;

    [SerializeField] private AudioClip _attackSFX;

    private Animator _anim;
    private Health _playerHealth;

    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        _CDTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            if(_CDTimer >= _attackCD && _playerHealth.CurrHealth > 0)
            {
                _CDTimer = 0;
                _anim.SetTrigger("Attack");
                AudioManager.Instance.PlaySound(_attackSFX);
            }
        }

        if(_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector2(_collider.bounds.size.x * _range, _collider.bounds.size.y),
            0, Vector2.left, 0, _playerLayer);

        if(hit.collider != null)
        {
            _playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            _playerHealth.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector2(_collider.bounds.size.x * _range, _collider.bounds.size.y));
    }
}
