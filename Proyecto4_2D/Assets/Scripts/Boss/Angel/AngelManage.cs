using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AngelState
{
    Idel,
    Attack,
    Hit,
    Dead,
}
public enum AngelAttacks
{
    SingleBullet, //1
    SingleExplosive,
    BulletHell,
    PilarAttack,
    ColumnaAttackRight,
    ColumnaAttackLeft,
    //6
}

public class AngelManage : MonoBehaviour
{

    [Header("Times")]
    public float timeStateToChange;
    public float timeToAttack;

    [Header("Estados")]
    public AngelState state;
    public AngelAttacks attackState;
    int index=0, index2;
    
    [Header("Posiciones")]
    public float timePositionTP;
    public float countdownTP;
    public Transform[] positions;
    
    [Header("Components")]
    private Animator animatorAngel;
    private Rigidbody2D bossRB;

    [Header("General Attack")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnAttack;


    [Header("Bullet Attack")]
    public float moveSpeedFire;
    [SerializeField] private GameObject fireBulletPrefab;

    [Header("BulletHell Attack")]
    public float divisiones;
    public float radioCirculo;

    [Header("Pilar Attack")]
    [SerializeField] private GameObject PilarExplosivePrefab;
    [SerializeField] private Transform[] positionsPilar;

    [Header("Columna Attack")]
    [SerializeField] private Transform[] positionsColumnas;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = positions[0].position;
        state = AngelState.Idel; 
        bossRB = GetComponent<Rigidbody2D>();
        animatorAngel = GetComponent<Animator>();
        countdownTP = timePositionTP;
        StartCoroutine(AngelStateChange());
        
    }

    // Update is called once per frame
    void Update()
    {
        countdownTP -= Time.deltaTime;
        DirecctionSelect();
    }
    void DirecctionSelect()
    {  
        if (countdownTP <= 0)
        {
            countdownTP = timePositionTP;
            int randomPosition = Random.Range(0, 6);
            transform.position = positions[randomPosition].position;
        }

    }
    IEnumerator AngelStateChange()
    {
        int randomState = Random.Range(1, 3);
        yield return new WaitForSeconds(timeStateToChange);

        if (randomState == 1)
        {
            state = AngelState.Idel;
            index = 1;
            index2 = 1;
            
        }

        if (randomState == 2)
        {
            state = AngelState.Attack;
            if (index == 2 || index2 != 1)
            {
                state = AngelState.Idel;
                index2 = 1;
            }
            index = 2;
        }
        StateChanger();
    }
    IEnumerator AngelAttackStateChange()
    {
        int randomAttack = Random.Range(1, 7);
       // int randomAttack = 6;
        yield return new WaitForSeconds(timeToAttack);
        switch (randomAttack)
        {
            case 1:
                attackState = AngelAttacks.SingleBullet;
                break;
            case 2:
                attackState = AngelAttacks.SingleExplosive;
                break;
            case 3:
                attackState = AngelAttacks.BulletHell;
                break;
            case 4:
                attackState = AngelAttacks.PilarAttack;
                break;
            case 5:
                attackState = AngelAttacks.ColumnaAttackRight;
                break;
            case 6:
                attackState = AngelAttacks.ColumnaAttackLeft;
                break;
            default:
                break;
        }
        AttackStateChanger();   
    }
    public void StateChanger()
    {
        switch (state)
        {
            case AngelState.Idel:
                animatorAngel.SetBool("Idel", true);
                StartCoroutine(AngelStateChange());
                

                break;
            case AngelState.Attack:
                animatorAngel.SetBool("Idel", false);
                animatorAngel.SetTrigger("Attack");
                
                StartCoroutine(AngelAttackStateChange());
                StartCoroutine(AngelStateChange());
                break;
    
            default:
                break;
        }
    }
    public void AttackStateChanger()
    {
        switch (attackState)
        {
            case AngelAttacks.SingleBullet:
                StartCoroutine(Bullet());
                break;
            case AngelAttacks.SingleExplosive:
                StartCoroutine(Pilar());
                break;
            case AngelAttacks.BulletHell:
                StartCoroutine(BulletHellAttack());
                break;
            case AngelAttacks.PilarAttack:
                StartCoroutine(PilarLateralAttck());
                break;
            case AngelAttacks.ColumnaAttackRight:
                StartCoroutine(ColumnaRithgToLeftAttack());
                break;
            case AngelAttacks.ColumnaAttackLeft:
                StartCoroutine(ColumnaLeftToRightAttack());
                break;
            default:
                break;
        }
    }
    IEnumerator Bullet()
    {
        GameObject Fire = Instantiate(fireBulletPrefab, spawnAttack.position, Quaternion.identity);
        //Fire.GetComponent<Bullet>().enabled = false;
        Vector2 moveDirection = (player.transform.position - spawnAttack.position).normalized * moveSpeedFire;
        Fire.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
        yield return new WaitForSeconds(1f);
  
    }
    IEnumerator Pilar()
    {
        GameObject Pilar = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
        Pilar.transform.position = new Vector2 (player.transform.position.x, 0);
        yield return new WaitForSeconds(0.5f);
        Pilar.GetComponent<Animator>().SetTrigger("Explosive");
        //Vector2 moveDirection = (player.transform.position - spawnBullet.position).normalized * moveSpeedFire;
        //Fire.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
        yield return new WaitForSeconds(1.5f);


    }
    IEnumerator ColumnaRithgToLeftAttack()
    {
        if (transform.position.y > 1)
        { 
            PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 6f;
            GameObject Pilar = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
        
            for (int i = 0; i < positionsColumnas.Length; i++)
            {
                Pilar.transform.position = positionsColumnas[i].position;
                Pilar.GetComponent<Animator>().SetTrigger("Explosive");
                yield return new WaitForSeconds(1f);
            }
            PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 1.5f;
        }
        else
        {
            StartCoroutine(BulletHellAttack());
        }
        yield return new WaitForSeconds(7f);
    }
    IEnumerator ColumnaLeftToRightAttack()
    {
        if (transform.position.y > 1)
        { 
            PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 6.5f;
            GameObject Pilar = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);

            for (int i = 5; i < positionsColumnas.Length && i>=0; i--)
            {
                Pilar.transform.position = positionsColumnas[i].position;
                Pilar.GetComponent<Animator>().SetTrigger("Explosive");
                yield return new WaitForSeconds(1f);
            }
            PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 1.5f;
        }
        else
        {
            StartCoroutine(BulletHellAttack());
        }
        yield return new WaitForSeconds(7f);
    }
    IEnumerator PilarLateralAttck()
    {
        
        if (transform.position.y > 1)
        {
            GameObject central = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
            central.transform.position = positionsPilar[0].position;
            central.GetComponent<Animator>().SetTrigger("Explosive");
            yield return new WaitForSeconds(1f);
            Destroy(central);
            GameObject LateralRight = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
            GameObject LateralLeft = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
            LateralRight.transform.position = positionsPilar[1].position;
            LateralLeft.transform.position = positionsPilar[2].position;
            LateralRight.GetComponent<Animator>().SetTrigger("Explosive");
            LateralLeft.GetComponent<Animator>().SetTrigger("Explosive");
            yield return new WaitForSeconds(1f);
            Destroy(LateralRight);
            Destroy(LateralLeft);

        }
        else
        {
            StartCoroutine(BulletHellAttack());
        }
        yield return new WaitForSeconds(3f);

    }
    IEnumerator BulletHellAttack()
    {
        for (float angulo = 0; angulo < 360; angulo += 360 / divisiones)
        {
            float velocidad = 1;
            CrearBala(angulo, velocidad);
        }
        yield return new WaitForSeconds(0f);
       

    }
    void CrearBala(float angulo, float velocidad)
    {
        // Calcula las coordenadas x e y basándote en el ángulo y el radio
        float x = transform.position.x + Mathf.Cos(angulo * Mathf.Deg2Rad) * radioCirculo;
        float y = transform.position.y + Mathf.Sin(angulo * Mathf.Deg2Rad) * radioCirculo;

        Vector3 moveDirection = new Vector3(x, y, 0f);
        GameObject bala = Instantiate(fireBulletPrefab, moveDirection, Quaternion.identity);

        Vector2 bulDir = (bala.transform.position - transform.position).normalized;
        // Configura la velocidad de la bala
        bala.GetComponent<Rigidbody2D>().velocity = bulDir * moveSpeedFire;

    }




}





