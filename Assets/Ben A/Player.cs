using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region stateMachine

    public CharacterStateMachine stateMachine;

    public CharacterStateIdle stateIdle;
    public CharacterStateMoving stateMoving;
    public CharacterStateJumping stateJumping;
    public CharacterStateFalling stateFalling;

    #endregion


    [Range(20, 50)] public float _speed;

    public Rigidbody2D _rb;
    public GameObject _feet;

    //public bool _isGrounded;

    [SerializeField] public float _jumpForce;
    [SerializeField] private float _fallMultiplier;

    void Awake()
    {
        stateMachine = new CharacterStateMachine();
        stateIdle = new CharacterStateIdle(stateMachine, this);
        stateMoving = new CharacterStateMoving(stateMachine, this);
        stateJumping = new CharacterStateJumping(stateMachine, this, _jumpForce);
        stateFalling = new CharacterStateFalling(stateMachine, this, _fallMultiplier);

        stateMachine.Initialize(stateIdle);

        _rb = GetComponent<Rigidbody2D>();
        //_isGrounded = _feet.GetComponent<FeetPlayer>().isGrounded;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.UpdateFrame();
        
        //if (_rb.velocity.y > 0)
        //{
        //    _rb.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        //}

    }

}
