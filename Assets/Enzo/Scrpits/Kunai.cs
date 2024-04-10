using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Kunai : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D col;



    public Vector2 destiny;
    public float distance = 0.02f;
    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;
    public float speed;
    private bool done = false;
    public LineRenderer lr;
    int vertexCount = 2;

    public List<GameObject> Nodes = new List<GameObject>();

    public Vector2 playerPosShoot;


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
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;
        Nodes.Add(transform.gameObject);
        rb.AddForce(transform.up * launchPower * 5);
        attachable = true;
        fallen = false;
    }

    private void Update()
    {
        if (isAttached == false)
        {
            if (Vector2.Distance(playerPosShoot, lastNode.transform.position) > 5f)
            {
                CreateNode();
            }

        }
        else if (done == false && isAttached == true)
        {
            print("done");
            done = true;
            while (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }
        RenderLine();
    }

    private void FixedUpdate()
    {
        if(isAttached) 
            return;
        if (fallen)
            return;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,rb.velocity);
    }

    void RenderLine()
    {

        lr.positionCount =vertexCount;

        int i;
        for (i = 0; i < Nodes.Count; i++)
        {

            lr.SetPosition(i, Nodes[i].transform.position);

        }

        lr.SetPosition(i, player.transform.position);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
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
        GetComponent<HingeJoint2D>().enabled = true;
        
    }

    public void Detach(Vector3 playerPos)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce((playerPos - transform.position).normalized * powerKnockBack,ForceMode2D.Impulse);
        attachable = false;
        GetComponent<HingeJoint2D>().enabled = false;

    }

    private void CreateNode()
    {

        Vector2 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);


        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();

        lastNode = go;

        Nodes.Add(lastNode);

        vertexCount++;

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
