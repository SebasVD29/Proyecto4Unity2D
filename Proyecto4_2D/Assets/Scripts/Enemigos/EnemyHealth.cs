using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
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
                Destroy(gameObject);
            }
        }
    }

 
}
