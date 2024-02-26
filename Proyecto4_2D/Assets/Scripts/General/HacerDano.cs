using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HacerDano : MonoBehaviour
{
    public float CantidadDano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Dano");
            collision.GetComponent<CodigoSalud>().RecibirDano(CantidadDano);

        }
    }


}
