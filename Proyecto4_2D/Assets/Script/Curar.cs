using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class Curar : MonoBehaviour
{
    public float CantidadCura;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && other.GetComponent<CodigoSalud>())
        {
            UnityEngine.Debug.Log("Player entered trigger collider.");
            other.GetComponent<CodigoSalud>().RecibirCura(CantidadCura);

            Destroy(gameObject);

         }

    }

   
}
