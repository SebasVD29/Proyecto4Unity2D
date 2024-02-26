using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] BossEnemy bossEnemy;
    [SerializeField] GameObject boss;
    [SerializeField] Animator bossAnimator;


    private void Start()
    {
        bossEnemy = GetComponent<BossEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            bossEnemy.bossHealth -= /*Player.Damage*/ 5f ;
            bossAnimator.SetTrigger("Hit");
            Debug.Log("DañoBoss");
            if (bossEnemy.bossHealth <= 0)
            {
                if (BossGeneralManager.instance.currentAngelHealth <= 0)
                {
                    boss.SetActive(false);
                }
                if (BossGeneralManager.instance.currentDevilHealth <= 0)
                {
                    boss.SetActive(false);
                }
            }
        }
    }
}
