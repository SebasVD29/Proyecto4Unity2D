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
    [Header("Times")]
    public float timeStateToChange;
    public float timeToSpecialAttack = 0.4f;
    public float timeToNormalAttack;

    [Header("Estados")]
    public DevilState state;

    [Header("Componentes")]
    private Animator animatorDevil;
    private Rigidbody2D bossRB;

    [Header("Movement")]
    public float moveSpeed;
    public bool mirarIzquierda = true;
    public Transform[] positionsTP;
    private Vector2 moveDirection;

    [Header("General Attacks")]
    [SerializeField] private GameObject player;

    [Header("Normal Attack")]
    [SerializeField] private GameObject NormalAttackGO;
    public float moveSpeedAttack;
    public Transform normalAttackPosition;

    [Header("Special Attack")]
    [SerializeField] private GameObject SpecialAttack;
    public Transform specialAttackPosition;

    [Header("Spell Attack")]
    [SerializeField] private GameObject SpellAttack;

    // Start is called before the first frame update
    void Start()
    {
        //state = DevilState.Idel;
        animatorDevil = GetComponentInChildren<Animator>();
        bossRB = GetComponent<Rigidbody2D>();
        transform.position = positionsTP[0].position;
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
        int randomState = Random.Range(1, 5);
        //int randomState = 2;
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
                StartCoroutine(NormalAttackMetodo());
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
        int randomPosition = Random.Range(0, 3);
        animatorDevil.SetTrigger("Tp");
        yield return new WaitForSeconds(0.8f);
        transform.position = positionsTP[randomPosition].position;
        yield return new WaitForSeconds(0.2f);
        animatorDevil.SetTrigger("Cast");
        yield return new WaitForSeconds(0.6f);
        GameObject spell = Instantiate(SpellAttack, transform.position, Quaternion.identity);
        spell.transform.position = new Vector2(player.transform.position.x, -0.3f);
        yield return new WaitForSeconds(0.8f);
        spell.GetComponent<Animator>().SetTrigger("SpellAttack");
        //yield return new WaitForSeconds(1f);

    }

    IEnumerator NormalAttackMetodo()
    {
        int randomPosition = Random.Range(0, 3);
        animatorDevil.SetTrigger("Tp");
        yield return new WaitForSeconds(0.8f);
        transform.position = positionsTP[randomPosition].position;
        
        yield return new WaitForSeconds(0.3f);
        animatorDevil.SetTrigger("Walking");
        bossRB.velocity = new Vector2(moveDirection.x, bossRB.velocity.y);

        // = NormalAttack.triggerNormalAttack;

        yield return new WaitForSeconds(timeToNormalAttack);
        animatorDevil.SetTrigger("NAttack");

        //if (triggerNAttack)
        //{
        //    NormalAttack.triggerNormalAttack = false;
        //}
        //triggerNAttack = false;
    }
    IEnumerator SpecialAttck()
    {
        int randomPosition = Random.Range(0, 3);
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
