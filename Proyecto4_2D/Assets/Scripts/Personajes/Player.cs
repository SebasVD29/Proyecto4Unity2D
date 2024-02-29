using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private TrailRenderer _trailRenderer;

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }




    bool canJump = true;
    public float velocidadDeMovimiento;
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;
    //private bool canDoubleJump;


    //Prueba Dash 
    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.5f;
    [SerializeField] private bool _active = true;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;




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
        _trailRenderer = GetComponent<TrailRenderer>();

    }

    void Update()
    {

       


            //Prueba Dash
            if (!_active)
                return;

            var inputX = Input.GetAxisRaw("Horizontal");
            var dashInput = Input.GetButton("Dash");

            if (dashInput && _canDash)
            {
                _isDashing = true;
                _canDash = true;
                _trailRenderer.emitting = true;
                _dashingDir = new Vector2(inputX, y: Input.GetAxisRaw("Vertical"));
                if (_dashingDir == Vector2.zero)
                {
                    _dashingDir = new Vector2(transform.localScale.x, y: 0);
                }

                StartCoroutine(StopDashing());

            }

            animator.SetBool("Dash", _isDashing);

            if (_isDashing)
            {
                rb.velocity = _dashingDir.normalized * _dashingVelocity;
            }

            if (IsGrounded.isGrounded)
            {
                _canDash = true;
            }

            //

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

        Attack();
    }

   /* private void FixedUpdate()
    {
        if(isDashing)
        {
            //return;
            Move();
        }
        rb.velocity = new Vector2(horizontal *  runSpeed, rb.velocity.y);
    }*/
    public void Attack()
    { 
     if(Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attack", true);
            
        }
        else 
        {
            animator.SetBool("Attack", false);
        }
    }
    private void Move()
    {

        rb.velocity = new Vector2(horizontal * runSpeed, rb.velocity.y);
    }



    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}
