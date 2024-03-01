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
            //Debug.Log("Player Dano");
            collision.GetComponent<CodigoSalud>().RecibirDano(playerDamage);
            //collision.GetComponent<PlayerRespawn>().PlayerDameged();
        }
    }


}
