using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidades : MonoBehaviour
{
    public bool Dash;
    public bool DoubleJump;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Dash)
            {
                collision.GetComponent<PlayerMovement>().haveDashing = true;
                Destroy(gameObject);
            }
            else if (DoubleJump)
            {
                collision.GetComponent<PlayerMovement>().haveDoubleJump = true;
                Destroy(gameObject);
            }
        }
    }
}
