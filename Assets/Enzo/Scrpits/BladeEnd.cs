using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEndEnzo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            GetComponentInParent<KunaiEnzo>().Attach(collision);
        }
    }
}
