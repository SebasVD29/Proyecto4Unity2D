using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyDamage : MonoBehaviour
{
    public float playerDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collision.GetComponent<CodigoSalud>().RecibirDano(playerDamage);
            
        }
    }


}
