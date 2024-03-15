using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerHealth : MonoBehaviour
{

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text cantidadPosiones;
    
    private float SaludMaxima = 100;
    public float cantidadCura = 20;
    int potions;
    public void Update()
    {
        ActualizarInterfaz();
       
    }
    private void Start()
    {
        ActualizarInterfaz();
    }

    public void TakeHealth(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            Debug.Log(PlayerManager.instance.playerCuras);
            cantidadPosiones.text = "x " + PlayerManager.instance.playerCuras;
            if (PlayerManager.instance.playerCuras > 0)
            {
                PlayerManager.instance.playerSalud += cantidadCura;
                PlayerManager.instance.playerCuras -= 1;

            }
            else if (PlayerManager.instance.playerCuras <= 0)
            {
                PlayerManager.instance.playerSalud += 0;
                PlayerManager.instance.playerCuras = 0;
            }
        }
        
    }

    public void TakeDamage(float dano)
    {
        PlayerManager.instance.playerSalud -= dano;

        if (PlayerManager.instance.playerSalud <= 0)
        {
            PlayerManager.instance.playerSalud = 0; 
            gameObject.GetComponent<PlayerRespawn>().PlayerDeath();

        }
        
    }

    public void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = PlayerManager.instance.playerSalud / SaludMaxima;
        cantidadPosiones.text = "x " + PlayerManager.instance.playerCuras;
    }


}

