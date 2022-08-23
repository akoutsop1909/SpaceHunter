using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        //destroy everything that leaves the trigger
        Destroy(other.gameObject);
    }
}
