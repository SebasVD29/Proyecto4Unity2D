using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] BossEnemy bossEnemy;
   // [SerializeField] GameObject boss;
    [SerializeField] Animator bossAnimator;


    private void Start()
    {
        //bossEnemy = GetComponent<BossEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            bossEnemy.bossHealth -= /*Player.Damage*/ 2f;
            StartCoroutine(HitDeathAnimation());

        }
    }


    IEnumerator HitDeathAnimation()
    {
       
        bossAnimator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        if (bossEnemy.bossHealth <= 0)
        {
            bossAnimator.SetTrigger("Death");
            yield return new WaitForSeconds(1.5f);
        }
    }

}
