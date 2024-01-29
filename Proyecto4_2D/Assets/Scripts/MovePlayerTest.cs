using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerTest : MonoBehaviour
{
    float horizontal;
    float speed = 8f;
    Rigidbody2D player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(horizontal * speed, player.velocity.y);
    }
}
