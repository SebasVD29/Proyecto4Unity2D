using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Cantidad de vida que se restaura al recoger el objeto

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("player")) // Verificar si el objeto que ha entrado en contacto es el jugador
        {
            HealthController playerHealth = collider.GetComponent<HealthController>(); // Obtener el componente HealthController del jugador

            if (playerHealth != null) // Verificar que el jugador tenga el componente HealthController
            {
                playerHealth.Heal(healAmount); // Llamar al método Heal del jugador para restaurar la vida
                Destroy(gameObject); // Destruir el objeto de restauración de vida después de ser recogido
            }
        }
    }
}


