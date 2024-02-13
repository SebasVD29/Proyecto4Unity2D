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
    public float timeStateToChange;
    public AngelState state;
    public AngelAttacks attackState;
    private Vector3 positionStart = new Vector3(2.7f,0.3f,0f);

    int index=0, index2;
    
    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedFire;
    int nextPosition;
    private Vector2 moveDirection;

    [Header("Posiciones")]
    public Transform[] positions;
    

    [Header("Components")]
    private Animator animatorAngel;
    private Rigidbody2D bossRB;

    [Header("Attack Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fireBulletPrefab;
    [SerializeField] private GameObject PilarExplosivePrefab;

    [Header("Positions Attack")]
    [SerializeField] private Transform spawnAttack;
    [SerializeField] private Transform[] positionsPilar;
    [SerializeField] private Transform[] positionsColumnas;

    // Start is called before the first frame update
    void Start()
    {
        state = AngelState.Idel;
        nextPosition = 0;
        bossRB = GetComponent<Rigidbody2D>();
        animatorAngel = GetComponent<Animator>();
        
        StartCoroutine(AngelStateChange());
        
    }

    // Update is called once per frame
    void Update()
    {

        //DirecctionSelect();
    }
    IEnumerator DirecctionSelect()
    {
        yield return new WaitForSeconds(0f);
        if (transform.position == positions[nextPosition].position)
        {
            increaseNextPosition();
        }
        //transform.position = Vector3.MoveTowards(transform.position, positions[nextPosition].transform.position, moveSpeed );
        moveDirection = (positions[nextPosition].transform.position - transform.position).normalized * moveSpeed;
        bossRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }
    void increaseNextPosition()
    {
        nextPosition++;
        if (nextPosition >= positions.Length)
        {
            nextPosition = 0;
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
        //int randomAttack = Random.Range(1, 7);
        int randomAttack = 4;
        yield return new WaitForSeconds(timeStateToChange);
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
                StartCoroutine(DirecctionSelect());

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
        yield return new WaitForSeconds(0f);
        GameObject Fire = Instantiate(fireBulletPrefab, spawnAttack.position, Quaternion.identity);
        Fire.GetComponent<Bullet>().enabled = false;
        Vector2 moveDirection = (player.transform.position - spawnAttack.position).normalized * moveSpeedFire;
        Fire.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
  
    }


    IEnumerator Pilar()
    {
        GameObject Pilar = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);
        Pilar.transform.position = new Vector2 (player.transform.position.x, 0);
        yield return new WaitForSeconds(0.5f);
        Pilar.GetComponent<Animator>().SetTrigger("Explosive");
        //Vector2 moveDirection = (player.transform.position - spawnBullet.position).normalized * moveSpeedFire;
        //Fire.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);

    }
    IEnumerator ColumnaRithgToLeftAttack()
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
    IEnumerator ColumnaLeftToRightAttack()
    {
        PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 6f;
        GameObject Pilar = Instantiate(PilarExplosivePrefab, spawnAttack.position, Quaternion.identity);

        for (int i = 5; i < positionsColumnas.Length; i--)
        {
            Pilar.transform.position = positionsColumnas[i].position;
            Pilar.GetComponent<Animator>().SetTrigger("Explosive");
            yield return new WaitForSeconds(1f);
        }
        PilarExplosivePrefab.GetComponent<Explosive>().timeLife = 1.5f;
      

    }

    IEnumerator BulletHellAttack()
    {
      
        yield return new WaitForSeconds(0f);
    }
    IEnumerator PilarLateralAttck()
    {
        yield return new WaitForSeconds(0.5f);
    }





}





