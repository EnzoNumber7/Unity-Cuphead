using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChakraScript : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            player.GetComponent<Player>().GetCoins();
            Destroy(gameObject);
            //player.GetComponent<TextUpdate>().UpdateText();

        }
    }
}
