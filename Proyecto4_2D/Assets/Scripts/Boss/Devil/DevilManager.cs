using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DevilState
{
    Idel,
    Cast,
    NormalAttack,
    SpecialAttack,
    Hit,
    Dead,
}
public class DevilManager : MonoBehaviour
{
    public float timeStateToChange;
    public float timeToSpecialAttack = 0.4f;

    public DevilState state;

    private Animator animatorDevil;
    private Rigidbody2D bossRB;


    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedAttack;
    public bool mirarIzquierda = true;
    private Vector2 moveDirection;

    public Transform[] positionsTP;
    public Transform normalAttackPosition;
    public Transform specialAttackPosition;



    [SerializeField] private GameObject player;
    [SerializeField] private GameObject NormalAttackGO;
    [SerializeField] private GameObject SpecialAttack;
    [SerializeField] private GameObject SpellAttack;

    public float distanciaDeAtaque;
    private float distanciaCalculadaDeAtaque;

    private bool triggerNAttack = false;
    //public float radioDeAttack;
   // public float dañoDeAttack;


    // Start is called before the first frame update
    void Start()
    {
        //state = DevilState.Idel;
        animatorDevil = GetComponentInChildren<Animator>();
        bossRB = GetComponent<Rigidbody2D>();
        StartCoroutine(DevilStateChange());
    }

    // Update is called once per frame
    void Update()
    {
        //distanciaCalculadaDeAtaque = Vector2.Distance(transform.position, player.transform.position);
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        MirarJugador();
    }
    IEnumerator DevilStateChange()
    {
        //int randomState = Random.Range(1, 5);
        int randomState = 3;
        yield return new WaitForSeconds(timeStateToChange);
        switch (randomState)
        {
            case 1:
                state = DevilState.Idel;
                break;
            case 2:
                state = DevilState.Cast;
                break;
            case 3:
                state = DevilState.NormalAttack;
                break;
            case 4:
                state = DevilState.SpecialAttack;
                break;
            default:
                break;
        }
        StateChanger();
    }

    public void StateChanger()
    {
        switch (state)
        {
            case DevilState.Idel:
                animatorDevil.SetBool("Idel", true);
                StartCoroutine(DevilStateChange());
                break;
            case DevilState.Cast:
                animatorDevil.SetBool("Idel", false);
                StartCoroutine(DevilStateChange());
                StartCoroutine(ConjurarSpell());
                break;
            case DevilState.NormalAttack:
                animatorDevil.SetBool("Idel", false);
                StartCoroutine(DevilStateChange());
                StartCoroutine(NormalAttack());
               
                break;
            case DevilState.SpecialAttack:
                animatorDevil.SetBool("Idel", false);
                StartCoroutine(DevilStateChange());
                StartCoroutine(SpecialAttck());
                break;
            default:
                break;
        }
    }

    IEnumerator ConjurarSpell()
    {
        int randomPosition = Random.Range(0, 2);
        animatorDevil.SetTrigger("Tp");
        yield return new WaitForSeconds(0.8f);
        transform.position = positionsTP[randomPosition].position;
        //yield return new WaitForSeconds(1f);
    }
   
    IEnumerator NormalAttack()
    {
        int randomPosition = Random.Range(0, 2);
        animatorDevil.SetTrigger("Tp");
        yield return new WaitForSeconds(0.8f);
        transform.position = positionsTP[randomPosition].position;
        
        yield return new WaitForSeconds(0.3f);
        animatorDevil.SetTrigger("Walking");
        bossRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
        triggerNAttack = NormalAttackGO.GetComponent<NormalAttack>().triggerNormalAttack;
        if (triggerNAttack == true)
        {
            animatorDevil.SetTrigger("NAttack");

        }
    }
    IEnumerator SpecialAttck()
    {
        int randomPosition = Random.Range(0, 2);
        animatorDevil.SetTrigger("Tp");
        yield return new WaitForSeconds(0.8f);
        transform.position = positionsTP[randomPosition].position;
        yield return new WaitForSeconds(0.3f);
        animatorDevil.SetTrigger("SAttack");
        yield return new WaitForSeconds(timeToSpecialAttack);

        if (mirarIzquierda == true)
        {
            GameObject special = Instantiate(SpecialAttack, specialAttackPosition.position, Quaternion.identity);
            special.transform.SetParent(specialAttackPosition);
            special.transform.Rotate(new Vector3(0, 0, 0));
            special.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeedAttack, 0);
        }
        else
        {
            GameObject special = Instantiate(SpecialAttack, specialAttackPosition.position, Quaternion.identity);
            special.transform.SetParent(specialAttackPosition);
            special.transform.Rotate(new Vector3(0, 180, 0));
            special.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeedAttack, 0);

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

}
