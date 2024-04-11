using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [Range(-10, 10)] public float offsetX;
    [Range(-10, 10)] public float offsetY;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, transform.position.z);
    }
}
