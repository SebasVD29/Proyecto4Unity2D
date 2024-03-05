using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Curar : MonoBehaviour
{
    public float CantidadCura=20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collision.GetComponent<CodigoSalud>().RecibirCura(CantidadCura);

            Destroy(gameObject);

        }
    }
}
