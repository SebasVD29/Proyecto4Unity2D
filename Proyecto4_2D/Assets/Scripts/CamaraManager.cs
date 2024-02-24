using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    [Header("Limites de la Camara")]
    public float limiteXIzquierdo;
    public float limiteXDerecho;
    public float limiteYArrbia;
    public float limiteYAbajo;
   
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
        //Si la posicion del jugador en -Y esta mas abajo que el limite en -Y
        if (Player.transform.position.y < limiteYAbajo)
        {
            //La camara toma la posicion del limite en -Y y la posicion del jugador en X
            transform.position = new Vector3(Player.transform.position.x, limiteYAbajo, -10);
        }
        else
        {
            //sino(la posicion del jugador esta mas arriba del limite en -Y) la camara toma la posicion que tiene el jugador en Y
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        }

        //Si la posicion del jugador en -X esta mas a la izquierda que en el limite en -X
        if (Player.transform.position.x < limiteXIzquierdo)
        {
            //La camara toma la posicion del limite en -X
            transform.position = new Vector3(limiteXIzquierdo, 0.8f, -10);
        }
        //Si la posicion del jugador en +X esta mas a la derecha que en el limite en +X
        if (Player.transform.position.x > limiteXDerecho)
        {
            //La camara toma la posicion del limite en +X
            transform.position = new Vector3(limiteXDerecho, 0.8f, -10);
        }
    }
}
