using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour


{
    bool canJump = true;
    public float velocidadDeMovimiento;
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;
    //private bool canDoubleJump;
    

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower;
    private float dashingTime;
    private float dashingCoolDown;
    [SerializeField] private TrailRenderer tr;




    private float horizontal;

    

    Rigidbody2D rb;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    SpriteRenderer sprite;

    Animator animator;

 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
     
    }

    void Update()
    {

        if(isDashing)
        {
            return;
        }
         horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //El personaje se mueve hacia la derecha 
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            //El personaje cambia la direccion hacia la derecha 
            sprite.flipX = false;
            //Se invoca el arbol de animacion
            animator.SetFloat("Run", runSpeed);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //El personaje se mueve hacia la izquierda
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            //El personaje cambia la direccion hacia la izquierda
            sprite.flipX = true;
            //Se invoca el arbol de animacion 
            animator.SetFloat("Run", runSpeed);
        }
        else
        {
            //El personaje no se mueve
            rb.velocity = new Vector2(0, rb.velocity.y);
            //Se invoca el arbol de animacion
            animator.SetFloat("Run", 0);

        }

        //prueba

        /*if (Input.GetKeyDown("space") && canJump)
        {
            canJump = false;
            animator.SetBool("jump", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200f));
        }*/

        //El personaje salta
         if (Input.GetKeyDown("space"))
         {
             if (IsGrounded.isGrounded)
             {
                 canJump = true;
                 rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

             }
             else
             {
                 if (canJump)
                 {

                     canJump = false;
                     animator.SetBool("Jump", true);
                     rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                 }
             }
        
         }

        
        //Upgraded Jump
        if (betterJump)
        {
            if (rb.velocity.y < 0)
            {
                //Se dirige hacia abajo 
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;

            }
            if (rb.velocity.y > 0 && !Input.GetKey("space"))
            {
                //El personaje da un salto corto 
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

        //El personaje se desliza
        if (Input.GetKeyDown(KeyCode.W) && canDash)
        {
            StartCoroutine(Dash());

        }

        //Flip();

        if (IsGrounded.isGrounded)
        {
            //Para invocar al arbol de animacion
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Fall", false);
            animator.SetBool("Dash", false);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
        //Para caer
        if (rb.velocity.y < 0)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
            animator.SetBool("Fall", false);
        }


    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            //return;
            Move();
        }
        rb.velocity = new Vector2(horizontal *  runSpeed, rb.velocity.y);
    }

    private void Move()
    {

        rb.velocity = new Vector2(horizontal * runSpeed, rb.velocity.y);
    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;


    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}