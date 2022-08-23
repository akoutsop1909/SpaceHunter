using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float tumble;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.angularVelocity = Random.value * tumble;
    }
}