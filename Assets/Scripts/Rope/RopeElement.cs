using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeElement : MonoBehaviour
{
    private GameObject aboveObject;
    public GameObject belowObject;
    public GameObject bottom;
    void Start()
    {
        if (gameObject.GetComponent<Kunai>() == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = true;
        }
        ResetAnchor();
    }

        public void ResetAnchor()
    {
        aboveObject = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeElement aboveElement = aboveObject.GetComponent<RopeElement>();

        if (aboveElement != null)
        {
            aboveElement.belowObject = gameObject;
            float coordinate = aboveObject.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -coordinate);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
    }

}
