using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Laser") return;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" ||other.tag == "Laser") return;
    }
}
