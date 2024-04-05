using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            GetComponentInParent<Kunai>().Attach(collision);
        }
    }
}
