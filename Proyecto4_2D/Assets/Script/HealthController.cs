using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HealthController : MonoBehaviour
{
    public Image BarradeVida;
    public float vidaActual;
    public float vidaMaxima;

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con la vida máxima al inicio del juego
        UpdateHealthBar(); // Actualizar la barra de vida al inicio
    }

    // Update is called once per frame
    void Update()
    {
        BarradeVida.fillAmount = vidaActual / vidaMaxima;
    }

    void UpdateHealthBar()
    {
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        BarradeVida.fillAmount = vidaActual / vidaMaxima;
    }

    // Función para dañar al jugador
    public void TakeDamage(int damage)
    {
        vidaActual -= damage; // Reducir la vida actual por la cantidad de daño recibido
        UpdateHealthBar(); // Actualizar la barra de vida después del daño

        // Verificar si el jugador ha muerto
        if (vidaActual <= 0)
        {
            Die();
        }
    }

    // Función para curar al jugador
    public void Heal(int healAmount)
    {
        vidaActual += healAmount; // Incrementar la vida actual por la cantidad de curación recibida
        UpdateHealthBar(); // Actualizar la barra de vida después de la curación
    }

    // Función para manejar la muerte del jugador
    void Die()
    {
        // Aquí puedes agregar lógica adicional, como mostrar un mensaje de game over o reiniciar el nivel.
        Debug.Log("Game Over");
    }
}
