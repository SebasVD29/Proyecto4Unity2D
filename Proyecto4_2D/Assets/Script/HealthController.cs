using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public Image HealthBar;
    public float vidaActual;
    public float vidaMaxima;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = vidaActual / vidaMaxima;
        
    }

    // // Función para dañar al jugador


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la vida actual por la cantidad de daño recibido
        UpdateHealthBar(); // Actualizar la barra de vida después del daño

        // Verificar si el jugador ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Función para curar al jugador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Incrementar la vida actual por la cantidad de curación recibida
        UpdateHealthBar(); // Actualizar la barra de vida después de la curación
    }

    // Función para manejar la muerte del jugador
    void Die()
    {
        // Aquí puedes agregar lógica adicional, como mostrar un mensaje de game over o reiniciar el nivel.
        Debug.Log("Game Over");
    }




}
