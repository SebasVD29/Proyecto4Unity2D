using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] BossEnemy bossEnemy;
    [SerializeField] GameObject Arcangel;

    private void Start()
    {
        bossEnemy = GetComponent<BossEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            bossEnemy.bossHealth -= /*Player.Damage*/ 5f ;
            Debug.Log("DañoBoss");
            if (bossEnemy.bossHealth <= 0)
            {
                Destroy(Arcangel);
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("PlayerAttack"))
    //    {
    //        bossEnemy.bossHealth -= /*Player.Damage*/ 5f;

    //        if (bossEnemy.bossHealth <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }

    //}
}
