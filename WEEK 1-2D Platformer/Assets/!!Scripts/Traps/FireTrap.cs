using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float _activationDelay;
    [SerializeField] private float _activeTime;

    private bool _isTriggered;
    private bool _isActive;

    private Animator _anim;
    private SpriteRenderer _sr;

    private Health _playerHealth;

    [SerializeField] private AudioClip _fireSFX;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_playerHealth != null && _isActive)
        {
            _playerHealth.TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _playerHealth = collision.GetComponent<Health>();

            if (!_isTriggered)
                StartCoroutine(ActivateTrap());
            if (_isActive)
                collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerHealth = null;
        }
    }

    private IEnumerator ActivateTrap()
    {
        _isTriggered = true;
        _sr.color = Color.red;
        yield return new WaitForSeconds(_activationDelay);
        _sr.color = Color.white;
        _isActive = true;
        _anim.SetBool("Active", true);
        AudioManager.Instance.PlaySound(_fireSFX);
        yield return new WaitForSeconds(_activeTime);
        _isActive = false;
        _isTriggered = false;
        _anim.SetBool("Active", false);
    }
}
