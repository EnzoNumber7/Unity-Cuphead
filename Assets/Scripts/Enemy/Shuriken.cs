using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float launchPower;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 5 *  launchPower);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy")
        {
            if (collision.collider.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(1, gameObject);
            }

            Destroy(gameObject);
        }
            
    }
}
