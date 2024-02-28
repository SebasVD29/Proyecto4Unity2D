using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class CodigoSalud : MonoBehaviour
{


    public float Salud = 100;
    public float SaludMaxima = 100;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;

    public Animator animator;

    
   
    public void Update()
    {
        ActualizarInterfaz();
    }

    public void RecibirCura(float cura) {

        Salud += cura;

        if (Salud > SaludMaxima) {

            Salud = SaludMaxima;

        }

    }

    public void  RecibirDano(float dano)
     
    {

        Salud -= dano;
        UnityEngine.Debug.Log("Health reduced by: " + dano + ". Current Health: " + Salud);

        if (Salud <= 0)
        {
            Salud = 0;
            UnityEngine.Debug.Log("El jugador ha perdido toda la salud.");
           // Destroy(gameObject);
           animator.SetTrigger("Death");
        }
        UnityEngine.Debug.Log("El jugador ha recibido " + dano + " puntos de daño. Salud actual: " + Salud);

    
      /*if (Salud==0)
        {

        gameObject.GetComponent<PlayerRespawn>().PlayerDeath();


        }
        */
    
    }

    public void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "Salud: " + Salud.ToString("f0");
    }


}

