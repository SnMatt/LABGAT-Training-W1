using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;        //var dimana semakin besar, maka makin cepat dia accelerate & reaching max speed
    [SerializeField] private float _decceleration;       //var dimana semakin besar, maka makin cepat dia deccelerate (reaching full stop)
    [SerializeField] private float _velPower;            //var dimana semakin besar, maka movement semakin snappy
    private float _moveInputX;

    [Header("Jump")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private float _rayLength;
    [SerializeField] private float _rayWallLength;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;

    [Header("Coyote Time")]
    [SerializeField] private float _coyoteTimeDuration;
    private float _coyoteTimer;

    [Header("Multi Jumps")]
    [SerializeField] private int _extraJump;
    private int _jumpCount;

    [Header("Firepoint")]
    [SerializeField] private Transform _firePoint;

    [Header("WallJump")]
    [SerializeField] private float _forceX;
    [SerializeField] private float _forceY;

    private float _wallJumpCD;
    private ParticleSystem _particle;
    private bool _wasGrounded;

    [Header("Sound")]
    [SerializeField] private AudioClip _jumpSFX;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        _anim.SetBool("IsMidair", !isGrounded);

        _moveInputX = Input.GetAxisRaw("Horizontal");
        
        if(_moveInputX != 0) 
        {
            if(_moveInputX > 0)
            { 
                _sr.flipX = false;
                _firePoint.localPosition = new Vector2(Mathf.Abs(_firePoint.localPosition.x), _firePoint.localPosition.y);
            }
            else if(_moveInputX < 0)
            { 
                _sr.flipX = true;
                _firePoint.localPosition = new Vector2(-Mathf.Abs(_firePoint.localPosition.x), _firePoint.localPosition.y);
            }

            _anim.SetBool("IsMoving", true);
            //if(AudioManager.instance.sSource.isPlaying == false)
            //{
            //    AudioManager.instance.PlayS("Walk");
            //}

        }else
        {
            _anim.SetBool("IsMoving", false);
        }

        //basic jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //adjustable jump
        if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / 2);

        if ((IsOnWallLeft() || IsOnWallRight()))
        {
            _rb.gravityScale = 1f;
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.gravityScale = 5;

            if(IsGrounded())
            {
                _coyoteTimer = _coyoteTimeDuration;
                _jumpCount = _extraJump;
            }else
            {
                _coyoteTimer -= Time.deltaTime;
            }
        }

        //if(_wallJumpCD > 0.2f)
        //{
        //    if((IsOnWallLeft() || IsOnWallRight()) && !IsGrounded())
        //    {
        //        _rb.gravityScale = 1f;
        //        _rb.velocity = Vector2.zero;
        //    }else
        //    {
        //        _rb.gravityScale = 5;
        //    }

        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Jump(isGrounded);
        //    }
        //}
        //else
        //{
        //    _wallJumpCD += Time.deltaTime;
        //}



        if (!_wasGrounded && isGrounded)
        {
            _anim.SetBool("IsMidair", false);
        }

        _wasGrounded = isGrounded;
    }

    private void FixedUpdate()
    {

        float targetSpeedX = _moveInputX * _moveSpeed;

        float speedDiffX = targetSpeedX - _rb.velocity.x;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01) ? _acceleration : _decceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDiffX) * accelRateX, _velPower) * Mathf.Sign(speedDiffX);

        _rb.AddForce(new Vector2(movementX, 0));

    }

    private void Jump()
    {
        if (_coyoteTimer < 0 && !IsOnWallLeft() && !IsOnWallRight() && _jumpCount <= 0) return;
        Debug.Log("aaa");
        AudioManager.Instance.PlaySound(_jumpSFX);

        if(IsOnWallLeft() || IsOnWallRight())
        {
            Debug.Log("bbb");

            WallJump();
        }else
        {
            if(_coyoteTimer > 0)
            {
                _rb.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
            }else
            {
                if(_jumpCount > 0)
                {
                    _rb.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
                    _jumpCount--;
                }
            }
        }

        _coyoteTimer = 0;

        //if(IsGrounded())
        //{
        //    _rb.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
        //    //_particle.Play();
        //    //_anim.SetBool("IsMidair", true);
        //}else if((IsOnWallLeft() || IsOnWallRight()) && !IsGrounded())
        //{
        //    if(IsOnWallLeft())
        //    {
        //        _rb.AddForce(new Vector2(8, 10), ForceMode2D.Impulse);
        //    }else if(IsOnWallRight())
        //    {
        //        _rb.AddForce(new Vector2(-8, 10), ForceMode2D.Impulse);
        //    }
        //    _wallJumpCD = 0;
        //}

        
    }

    private void WallJump()
    {
        if (IsOnWallLeft())
        {
            Debug.Log("ccc");

            _rb.AddForce(new Vector2(_forceX, _forceY));
        }
        else if (IsOnWallRight())
        {
            Debug.Log("ddd");

            _rb.AddForce(new Vector2(-_forceX, _forceY));
        }

        _wallJumpCD = 0;
    }

    bool IsGrounded()
    {
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - _boxSize.y * 0.5f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, _boxSize, 0f, Vector2.down, _rayLength, _groundLayer);
        return hit.collider != null;
    }
    bool IsOnWallRight()
    {
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - _boxSize.y * 0.5f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, _boxSize, 0f, Vector2.right, _rayWallLength + 0.18f, _wallLayer);
        return hit.collider != null;
    }
    bool IsOnWallLeft()
    {
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - _boxSize.y * 0.5f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, _boxSize, 0f, Vector2.left, _rayWallLength, _wallLayer);
        return hit.collider != null;
    }

    public bool CanAttack()
    {
        return _moveInputX == 0 && IsGrounded() && (!IsOnWallLeft() || !IsOnWallRight());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - _boxSize.y * 0.5f), _boxSize);
    }
}
