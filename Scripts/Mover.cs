using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves the bolt
public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        Vector2 movement = new Vector2(0, speed);
        rb2d.velocity = movement;
    }
}
