using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool grounded;
    
    private bool onWall;
    public bool OnWall => onWall;

    [SerializeField] bool facingRight = true;
    public bool FacingRight => facingRight;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius;

    [SerializeField] float knockbackMultiplier = 0.5f;
    public float KnockbackMultiplier => knockbackMultiplier;

    private bool canMove;

    [SerializeField] float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        canMove = true;
        onWall = false;

        coyoteTimeCounter = coyoteTime;
    }

    void Update()
    {
        if (groundCheck != null)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }    
        else
        {
            if (coyoteTimeCounter < 0f)
            {
                coyoteTimeCounter = 0f;
            }

            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = false;
        }
    }

    public void Move(float input)
    {
        if (!canMove)
        {
            return;
        }

        Vector3 velocity = new Vector2(input, rb.velocity.y);

        rb.velocity = velocity;

        if ((input > 0 && !facingRight) || (input < 0 && facingRight))
        {
            Flip();
        }
    }

    public void Jump(float power)
    {
        if (coyoteTimeCounter <= 0f || !canMove)
        {
            return;
        }
        coyoteTimeCounter = 0f;

        Vector2 _newVelocity = rb.velocity;
        _newVelocity.y = power;
        rb.velocity = _newVelocity;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x = charScale.x * -1;
        transform.localScale = charScale;
    }
    public void SetToggleMove(bool state)
    {
        canMove = state;
    }

    public void SetRigidbodyVelocity(Vector3 value)
    {
        rb.velocity = value;
    }

    public void SetRigidbodyGravity(float value)
    {
        rb.gravityScale = value;
    }

    public void AddForce(Vector3 value)
    {
        rb.AddForce(value);
    }

    public Vector3 GetCurrentVelocity()
    {
        return rb.velocity;
    }

    public float GetCurrentGravity()
    {
        return rb.gravityScale;
    }
}
