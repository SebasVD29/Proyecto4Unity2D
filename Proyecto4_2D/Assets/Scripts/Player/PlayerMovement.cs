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

    public static PlayerMovement instance;

    [Header("Componentes")]
    public Transform groundCheck;
    private Rigidbody2D playerRB;
    private TrailRenderer trailRenderer;

    [Header("Movement")]
    public float runSpeed = 4;
    public float jumpForce = 6;
    public float dashForce = 6;
    public LayerMask groundLayer;
    public float horizontal;

 
    [Header("Boleans")]
    public bool haveDashing = false;
    public bool haveDoubleJump = false;
    private bool isFacingRight = true;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;

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
        if (horizontal > 0f)
        {
            Debug.Log("Derecha");
        } 
        else 
        {
            Debug.Log("Izquierda");
        }
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
        else if (isFacingRight && horizontal > 0f)
        {
            FLip();
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
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        if (context.canceled && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(DashPlayer());
        }
    }

}
