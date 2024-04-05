using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [Range (20,50)] public float _speed;

    public Rigidbody2D _rb;
    public GameObject _feet;

    [SerializeField] public float _jumpForce;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _lowJumpMultiplier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         Vector2 currentVel = new Vector2(Input.GetAxis("Horizontal") * _speed * Time.deltaTime * 100, _rb.velocity.y);


        _rb.velocity = currentVel;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
        }

        //Falling
        if ( _rb.velocity.y < 0) { _rb.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime; }

    }
}
