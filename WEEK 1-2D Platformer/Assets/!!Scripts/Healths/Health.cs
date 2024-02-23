using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("HPs")]
    [SerializeField] private int _startingHealth;
    public int CurrHealth { get; private set; }
    private bool _isDead = false;

    [Header("iFrames")]
    [SerializeField] private float _iframeDuration;
    [SerializeField] private int _numOfFlashes;
    private SpriteRenderer _sr;
    private bool _isInvulnerable;

    private Animator _anim;

    [Header("Components")]
    [SerializeField] private Behaviour[] _components;

    [SerializeField] private AudioClip _hurtSFX;
    [SerializeField] private AudioClip _deathSFX;

    private void Awake()
    {
        CurrHealth = _startingHealth;
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(int amount)
    {
        if (_isInvulnerable) return;

        CurrHealth = Mathf.Clamp(CurrHealth - amount, 0, _startingHealth);

        if(CurrHealth > 0)
        {
            _anim.SetTrigger("Hurt");
            AudioManager.Instance.PlaySound(_hurtSFX);
            StartCoroutine(Invulnerable());
        }else
        {
            if(!_isDead)
            {
                AudioManager.Instance.PlaySound(_deathSFX);

                if(GetComponent<Rigidbody2D>())
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                foreach (Behaviour item in _components)
                {
                    item.enabled = false;
                }

                _anim.SetBool("IsMidair", false);
                _anim.SetTrigger("Die");

                _isDead = true;
            }
        }

    }

    public void AddHealth(int value)
    {
        CurrHealth = Mathf.Clamp(CurrHealth + value, 0, _startingHealth);
    }
    public void Respawn()
    {
        _isDead = false;
        AddHealth(_startingHealth);
        _anim.ResetTrigger("Die");
        _anim.Play("Player_Idle");

        foreach (Behaviour item in _components)
        {
            item.enabled = true;
        }
    }
    private IEnumerator Invulnerable()
    {
        _isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < _numOfFlashes; i++)
        {
            _sr.color = Color.red;
            yield return new WaitForSeconds(_iframeDuration / (_numOfFlashes * 2));
            _sr.color = Color.white;
            yield return new WaitForSeconds(_iframeDuration / (_numOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
        _isInvulnerable = false;
    }
    public void ResetState()
    {
        _isInvulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
