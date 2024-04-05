using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region stateMachine

    CharacterStateMachine stateMachine;

    CharacterStateIdle stateIdle;
    CharacterStateMoving stateMoving;

    #endregion


    [Range (20,50)] public float _speed;

    public Rigidbody2D _rb;
    public GameObject _feet;

    private bool _isGrounded;

    [SerializeField] public float _jumpForce;
    [SerializeField] private float _fallMultiplier;

    void Awake()
    {
        stateMachine = new CharacterStateMachine();
        stateIdle = new CharacterStateIdle(stateMachine, this);
        stateMachine.Initialize(stateIdle);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = _feet.GetComponent<FeetPlayer>().isGrounded;
    }

    // Update is called once per frame
    void Update()
    {

        float Xaxis = Input.GetAxis("Horizontal");

        if(Xaxis != 0 && currentState != States.JUMPING) { currentState = States.MOVING; }



        switch (currentState)
        {
            case States.IDLE:
                UpdateIdle();
                break;
            case States.MOVING:
                UpdateMoving(Xaxis);
                break;
            case States.JUMPING:
                break;
            case States.FALLING:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _feet.GetComponent<FeetPlayer>().isGrounded)
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
        }

        //Falling
        if ( _rb.velocity.y < 0) { _rb.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime; }

        if( _rb.velocity.y > 0)
        {
            _rb.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        }

    }

    public void UpdateIdle()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _feet.GetComponent<FeetPlayer>().isGrounded)
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
        }
    }

    public void UpdateMoving(float axis)
    {
        _rb.velocity = new Vector2(axis * _speed * Time.deltaTime * 100, _rb.velocity.y);
    }

    public void UpdateJumping()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public void UpdateFalling() 
    {
        //Augmenter la vitesse de chute
        _rb.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;

        if (_isGrounded)
            currentState = States.IDLE;

    }

}
