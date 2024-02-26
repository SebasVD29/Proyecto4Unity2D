using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class HacerDano : MonoBehaviour
{
    public float CantidadDano=20;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && other.GetComponent<CodigoSalud>())
        {
            UnityEngine.Debug.Log("Player entered trigger collider.");
            other.GetComponent<CodigoSalud>().RecibirDano(CantidadDano);


         }

    }

   
}
