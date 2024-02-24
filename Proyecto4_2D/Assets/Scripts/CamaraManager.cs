using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{

    /*private void LateUpdate()
    {
        if (player.transform.position.x < 5)
        {
            transform.position = new Vector3 (5, 0, -10);
        }
        else if(player.transform.position.x > 25)
        {

            transform.position = new Vector3 (25, 0, -10);
        }
        else
        {
            transform.position = player.transform.position + movimiento;

        }

    }*/

    public GameObject Player;
    private Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        move = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.transform.position.x < 5)
        {
            transform.position = new Vector3(5, 0, -10);
        }
        else
        {
            transform.position = Player.transform.position + move;
        }
            
    }
}
