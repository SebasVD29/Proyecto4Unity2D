using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HacerDano : MonoBehaviour
{
    public float CantidadDano=20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Daño");
            collision.GetComponent<CodigoSalud>().RecibirDaño(CantidadDano);

        }
    }


}
