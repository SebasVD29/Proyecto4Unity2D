using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Curar : MonoBehaviour
{
    public float CantidadCura;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Salud Player");
            collision.GetComponent<CodigoSalud>().RecibirCura(CantidadCura);

            Destroy(gameObject);

        }
    }
}
