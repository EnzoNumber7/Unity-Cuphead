using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : MonoBehaviour
{
    public bool isTriggering;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggering = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggering = false;
    }

}
