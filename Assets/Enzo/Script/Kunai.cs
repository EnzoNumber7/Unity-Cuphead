using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiEnzo : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;

    public bool isAttached;

    [SerializeField] float powerKnockBack;
    [SerializeField] float returnPower;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != "Grab")
        {
            Attach(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Grab")
        {
            Destroy(gameObject);
        }
    }

    public void Attach(Collider2D collision)
    {
        isAttached = true;
        GetComponent<Collider2D>().isTrigger = false;

        transform.SetParent(collision.transform, true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
    }

    public void Detach(Vector3 playerPos)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        rb.AddForce((playerPos - transform.position) * powerKnockBack);

    }

    public void ReturnToPlayer(Vector3 playerPos)
    {
        Vector3 testpos = new Vector3 (playerPos.x,playerPos.y + 3,1) ;
        rb.AddForce((testpos - transform.position) * returnPower / 2);
    }
}
