using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject winPlace;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= winPlace.transform.position.x)
        {
            win = true;
        }
    }
}
