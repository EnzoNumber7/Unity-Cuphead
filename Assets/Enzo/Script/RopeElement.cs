using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeElement : MonoBehaviour
{
    public GameObject _aboveObject;
    public GameObject _belowObject;
    void Start()
    {
        _aboveObject = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeElement aboveElement = _aboveObject.GetComponent<RopeElement>();

        if (aboveElement != null)
        {
            aboveElement._belowObject = gameObject;
            float coordinate = _aboveObject.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2 (0, -coordinate);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
    }

}
