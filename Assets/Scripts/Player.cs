using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;


    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [Header("Knockback")]
    [SerializeField] private Transform _center;
    [SerializeField] private float _knockbackVelocity = 8f;
    [SerializeField] private float _knockbackTime = 1f;
    private bool _knockbacked = false;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _obstacklesLayer;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!_knockbacked)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            _rigidbody2D.velocity = new Vector2(horizontal * speed, _rigidbody2D.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.6f);
            }
            Flip();           
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private bool IsGrounded()
    {
        var grounded = false;
        if (Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer) || Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _obstacklesLayer)) grounded = true;
        return grounded;
    }

    public void Knockback(Transform t)
    {
        var dir = _center.position - t.position;
        _knockbacked = true;
        _rigidbody2D.velocity = dir.normalized * _knockbackVelocity;
        
        StartCoroutine(UnKnockback());
    }

    private IEnumerator UnKnockback()
    {
        yield return new WaitForSeconds(_knockbackTime);
        _knockbacked = false;
    }


}
