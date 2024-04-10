using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    [SerializeField] GameObject currentKunai;
    

    public void GetCurrentKunai(GameObject Kunai)
    {
        currentKunai = Kunai;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == currentKunai)
        {
            currentKunai.GetComponent<Kunai>().DeleteKunai();
            gameObject.transform.parent.GetComponent<Player>().isUsed = false;
        }
    }
}
