using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiEnzo : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D col;

    public bool isAttached;
    [SerializeField]public bool attachable;
    public bool fallen;

    [SerializeField] float launchPower;
    [SerializeField] float powerKnockBack;
    [SerializeField] float returnPower;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.AddForce(transform.up * launchPower);
        attachable = true;
        fallen = false;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if(isAttached) 
            return;
        if (fallen)
            return;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION");
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyEnzo>().TakeDamage(1);
        }
    }
    public void Attach(Collider2D collision)
    {
        if (!attachable)
            return;

        isAttached = true;
        col.isTrigger = false;

        transform.SetParent(collision.transform, true);
        rb.bodyType = RigidbodyType2D.Static;
        
    }

    public void Detach(Vector3 playerPos)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce((playerPos - transform.position).normalized * powerKnockBack,ForceMode2D.Impulse);
        attachable = false;

    }

    public void ReturnToPlayer(Vector3 playerPos)
    {
        Vector3 testpos = new Vector3 (playerPos.x,playerPos.y + 2,1) ;
        rb.AddForce((testpos - transform.position).normalized * returnPower);
    }
    
    public void DeleteKunai()
    {
        Destroy(gameObject);
    }

}
