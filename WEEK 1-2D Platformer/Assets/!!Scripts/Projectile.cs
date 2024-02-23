using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    private float _direction;
    private bool _hit;

    private Animator _anim;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }


    private void Update()
    {
        if (_hit) return;

        float moveSpeed = _speed * _direction * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _collider.enabled = false;
        _anim.SetTrigger("Explode");

        if(collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<Health>() != null)
                collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(bool dir)
    {
        Invoke("Deactivate", _lifetime);

        _direction = dir ? -1 : 1;
        gameObject.SetActive(true);
        _hit = false;
        _collider.enabled = true;

        if(_direction != transform.localScale.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
