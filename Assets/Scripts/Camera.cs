using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
    }
}
