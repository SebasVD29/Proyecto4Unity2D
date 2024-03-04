using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyIA : MonoBehaviour
{
    [Header("Times")]
    public float timeToAttack;
    private float waitTimeWalking;
    private float waitTimeShoting;
    public float startWaitTime = 2;

    [Header("Enemy State")]
    public bool isShooting;
    public bool isWalking;

    [Header("Componentes")]
    [SerializeField] private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animatorEnemy;
    private Rigidbody2D rdEnemy;

    [Header("Movement")]
    public bool mirarIzquierda = true;
    public float speed = 0.5f;
    private int contardor = 0;
    private Vector2 actualPosition;
    private Vector2 moveDirection;
    public Transform[] positions;


    [Header("General Attacks")]
    [SerializeField] private Transform spawnAttack;
    public float moveSpeedFire;
    public bool haveBlueBullet;
    [SerializeField] private GameObject blueBullet;
    public bool haveRedBullet;
    [SerializeField] private GameObject redBullet;

    private float distanciaDelPlayer;


    // Start is called before the first frame update
    void Start()
    {
        waitTimeWalking = startWaitTime;
        waitTimeShoting = timeToAttack;
        animatorEnemy = GetComponent<Animator>();
        rdEnemy = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distanciaDelPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (isWalking)
        {
            //transform.position = positions[0].position;
            animatorEnemy.SetBool("EnemyAttack", false);
            MoveEnemy();
            if (distanciaDelPlayer < 5)
            {
                animatorEnemy.SetBool("EnemyAttack", true);              
            }
        }
        else if (isShooting) 
        {
            MirarJugador();
            if(waitTimeShoting <= 0)
            {
                waitTimeShoting = timeToAttack;
                animatorEnemy.SetTrigger("EnemyAttack");
                Invoke("LaunchBullet", 0.5f);
            }
            else
            {
                waitTimeShoting -= Time.deltaTime;
            }
        }
    }

    void MirarJugador()
    {
        if ((player.transform.position.x < transform.position.x && !mirarIzquierda) || (player.transform.position.x > transform.position.x && mirarIzquierda))
        {
            mirarIzquierda = !mirarIzquierda;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    void MoveEnemy()
    {
        StartCoroutine(EnemyWalikng());
        transform.position = Vector2.MoveTowards(transform.position, positions[contardor].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, positions[contardor].position) < 0.1f)
        {
            if (waitTimeWalking <= 0)
            {
                if (positions[contardor] != positions[positions.Length - 1])
                {
                    contardor++;
                }
                else
                {
                    contardor = 0;
                }
                waitTimeWalking = startWaitTime;
            }
            else
            {
                waitTimeWalking -= Time.deltaTime;
            }
        }
    }

    IEnumerator EnemyWalikng()
    {
        actualPosition = transform.position;
        yield return new WaitForSeconds(0.5f);
        if (transform.position.x < actualPosition.x)
        {
            spriteRenderer.flipX = true;
            
        }
        else if (transform.position.x > actualPosition.x)
        {
            spriteRenderer.flipX = false;
          
        }
    }

    void LaunchBullet()
    {
        GameObject newBullet;
        
        if (haveBlueBullet)
        {
            newBullet = Instantiate(blueBullet, spawnAttack.position, Quaternion.identity);
            Vector2 moveDirection = (player.transform.position - spawnAttack.position).normalized;
            if (moveDirection.x > 0)
            {
                newBullet.GetComponent<SpriteRenderer>().flipX = false;
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeedFire, 0);
                

            }
            else
            {
                newBullet.GetComponent<SpriteRenderer>().flipX = true;
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeedFire, 0);

            }


        }
        else if (haveRedBullet)
        {
            newBullet = Instantiate(redBullet, spawnAttack.position, Quaternion.identity);
            Vector2 moveDirection = (player.transform.position - spawnAttack.position).normalized * moveSpeedFire;
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);

        }



    }

}
