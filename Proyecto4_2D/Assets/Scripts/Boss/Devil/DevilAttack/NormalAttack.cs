using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public static bool triggerNormalAttack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerNormalAttack = true;
            Debug.Log("DañoPlayer");
            //Destroy(this.gameObject);
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        triggerNormalAttack = false;
    //    }
    //    triggerNormalAttack = false;
    //}
}
