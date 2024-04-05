using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{

    private Rigidbody2D rb;

    public bool isAttached;
    public float timer;

    [SerializeField] float launchPower;
    [SerializeField] float powerKnockBack;
    [SerializeField] float returnPower;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * launchPower);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.45 && isAttached == false) 
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            
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
        Vector3 testpos = new Vector3 (playerPos.x,playerPos.y + 2,1) ;
        rb.AddForce((testpos - transform.position) * returnPower / 2);
    }
    
    public void DeleteKunai()
    {
        Destroy(gameObject);
    }

}
