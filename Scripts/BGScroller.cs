using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float speed;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 offset = new Vector2 (0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
