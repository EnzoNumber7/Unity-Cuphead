using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject _anchor;
    public int _ropeSize;
    public GameObject _ropeElement;
    GameObject _prevElement;
    public GameObject _player;
    HingeJoint2D _top;
    void Start()
    {
        GenerateRope();
    }

    void Update()
    {
        FollowPlayer();
        if (Input.GetKeyDown(KeyCode.C) == true)
        {
            AddElement();
        }
    }

    public void GenerateRope()
    {
        _prevElement = _anchor;
        for (int i = 0; i < _ropeSize; i++)
        {
            GameObject newElement = Instantiate(_ropeElement);
            newElement.transform.parent = transform;
            newElement.transform.position = transform.position;
            HingeJoint2D hj = newElement.GetComponent<HingeJoint2D>();
            hj.connectedBody = _prevElement.GetComponent<Rigidbody2D>();
            _prevElement = newElement;

            if (i == 0)
            {
                _top = hj;
            }
        }
    }

    public void FollowPlayer()
    {
        Vector2 playerPos = _player.transform.position;
        _anchor.transform.position = new Vector2(playerPos.x + 1,playerPos.y);
        _anchor.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(playerPos.x + 1, playerPos.y);

    }

    public void AddElement()
    {
        GameObject newElement = Instantiate(_ropeElement);
        newElement.transform.parent = transform;
        newElement.transform.position = transform.position;
        HingeJoint2D hj = newElement.GetComponent<HingeJoint2D>();
        hj.connectedBody = _anchor.GetComponent<Rigidbody2D>();
        newElement.GetComponent<RopeElement>()._belowObject = _top.gameObject;

        _top.connectedBody = newElement.GetComponent<Rigidbody2D>();
        _top.GetComponent<RopeElement>().ResetAnchor();
        _top = hj;
        
        _ropeSize++;
    }
}
