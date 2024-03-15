using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Times")]
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    [Header("Componentes")]
    public Transform groundCheck;
    private Rigidbody2D playerRB;
    private TrailRenderer trailRenderer;
    private Animator playerAnimator;

    [Header("Movement")]
    public float runSpeed = 4;
    public float jumpForce = 6;
    public float doubleJumpForce = 10;
    public float dashForce = 6;
    public LayerMask groundLayer;
    public float horizontal;

 
    [Header("Boleans")]
    public bool haveDashing;
    public bool haveDoubleJump;
    private bool isFacingRight = true;
    private bool doubleJump;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;

    public static PlayerMovement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayerManager();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        playerRB.velocity = new Vector2(horizontal * runSpeed, playerRB.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void FLip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void MovePlayerManager()
    {
        if (isDashing)
        {
            return;
        }
        playerRB.velocity = new Vector2(horizontal * runSpeed , playerRB.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            FLip();        
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FLip();    
        }

        if (isGrounded() == false)
        {
            playerAnimator.SetTrigger("Fall");
        }
        
    }

    private IEnumerator DashPlayer()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRB.gravityScale;
        playerRB.gravityScale = 0f;
        playerRB.velocity = new Vector2(playerRB.velocity.x * dashForce, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        playerRB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        if (horizontal > 0.5)
        {
            playerAnimator.SetFloat("Run", horizontal);

        }
        else if(horizontal < -0.5)
        {
            playerAnimator.SetFloat("Run", 1f);

        } 
        else if (horizontal == 0)
        {
            playerAnimator.SetFloat("Run", 0f);

        }

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled && isGrounded())
        {
            
            doubleJump = false;
        }
        if (context.performed && isGrounded())
        {
            playerAnimator.SetTrigger("Jump");
            playerRB.velocity = new Vector2(playerRB.velocity.x,  jumpForce);
            
        }
        if (context.performed &&  (isGrounded() || doubleJump) && haveDoubleJump)
        {
            
            playerAnimator.SetTrigger("Jump");
            playerRB.velocity = new Vector2(playerRB.velocity.x,  doubleJumpForce);
            doubleJump = !doubleJump;
        }

        if (context.canceled && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && haveDashing)
        {
            StartCoroutine(DashPlayer());
        }
    }

}
