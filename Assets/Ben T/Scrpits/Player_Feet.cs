using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Feet : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Ground"))
        {
            isGrounded = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Ground"))
        {
            isGrounded = false;

        }
    }
}
