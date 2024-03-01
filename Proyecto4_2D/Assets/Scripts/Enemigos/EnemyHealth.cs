using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] Animator enemyAnimator;
    // Start is called before the first frame update
    private void Start()
    {
        enemy = GetComponent<Enemy>();  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            enemy.healthPoints -= 2f;
            if (enemy.healthPoints <= 0)
            {
                enemyAnimator.SetTrigger("EnemyDeath");
                Invoke("DestroyEnemy", 0.4f);
            }
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
 
}
