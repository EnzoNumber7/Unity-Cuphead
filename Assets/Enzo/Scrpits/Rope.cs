using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private GameObject ropeElement;
    [SerializeField] private HingeJoint2D top;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private int maxRope;
    [SerializeField] private int ropeSize;
    public GameObject kunai;
    GameObject lastElement;
    void Start()
    {
    }

    void Update()
    {
        FollowPlayer(firePoint.transform.position);
        if (GetDistanceFromAnchor() >= GetRopeMaxLenght() && GetRopeMaxLenght() > 0 && ropeSize <= maxRope)
        {
            AddElement();
        }
    }

    public void GenerateRope()
    {
        lastElement = anchor;
        for (int i = 0; i < ropeSize; i++)
        {
            GameObject newElement = Instantiate(ropeElement);
            newElement.transform.parent = transform;
            newElement.transform.position = transform.position;
            HingeJoint2D hj = newElement.GetComponent<HingeJoint2D>();
            hj.connectedBody = lastElement.GetComponent<Rigidbody2D>();
            lastElement = newElement;

            if (i == 0)
            {
                top = hj;
            }
        }
        kunai.transform.parent = transform;
        kunai.transform.position = transform.position;
        HingeJoint2D kunaiJoint = kunai.GetComponent<HingeJoint2D> ();
        kunaiJoint.connectedBody = lastElement.GetComponent <Rigidbody2D> ();
    }

    public void FollowPlayer(Vector2 firePoint)
    {
        anchor.transform.position = firePoint;
        anchor.GetComponent<HingeJoint2D>().connectedAnchor = firePoint;
    }

    public void AddElement()
    {
        GameObject newElement = Instantiate(ropeElement);
        newElement.transform.parent = transform;
        newElement.transform.position = transform.position;
        HingeJoint2D hj = newElement.GetComponent<HingeJoint2D>();
        hj.connectedBody = anchor.GetComponent<Rigidbody2D>();
        newElement.GetComponent<RopeElement>().belowObject = top.gameObject;

        top.connectedBody = newElement.GetComponent<Rigidbody2D>();
        top.GetComponent<RopeElement>().ResetAnchor();
        top = hj;
        
        ropeSize++;
    }

    public float GetRopeMaxLenght()
    {
        if (lastElement != null)
        {
            return ropeElement.GetComponent<SpriteRenderer>().bounds.size.y * ropeSize;
        }
        return 0;
    }

    public float GetDistanceFromAnchor()
    {
        if (lastElement != null)
        {
            //Vector2 ropeEndPos = new Vector2(lastElement.transform.position.x, lastElement.GetComponent<RopeElement>().bottom.transform.position.y);
            return (float)System.Math.Round(Vector2.Distance(lastElement.GetComponent<RopeElement>().bottom.transform.position, anchor.transform.position),3);
        }
        return 0;
    }
}
