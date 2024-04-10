using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cac : MonoBehaviour
{

    public bool isEnemyForward = false;
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            isEnemyForward = true;
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyForward = false;
        enemy = null;
    }

}
