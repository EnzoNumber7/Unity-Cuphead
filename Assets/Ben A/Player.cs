using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region stateMachine

    public CharacterStateMachine stateMachine;

    public CharacterStateIdle           stateIdle;
    public CharacterStateMoving         stateMoving;
    public CharacterStateJumping        stateJumping;
    public CharacterStateWallJumping    stateWallJumping;
    public CharacterStateFalling        stateFalling;
    public CharacterStateSliding        stateLeftSliding;
    public CharacterStateSliding        stateRightSliding;


    #endregion


    #region AnimatorVariables

    const string IS_GROUNDED_PARAM = "IsGrounded";
    const string VELX_PARAM = "Xvelocity";
    const string VELY_PARAM = "Yvelocity";
    const string SPEEDY_PARAM = "Yspeed";
    const string SPEEDX_PARAM = "Xspeed";

    #endregion

    [Range(20, 50)] public float _speed;

    public Rigidbody2D _rb;
    public GameObject _feet;

    public GameObject _leftSide;
    public GameObject _rightSide;

    public Animator animator;

    [SerializeField] public float _jumpForce;
    [SerializeField] private float _fallMultiplier;


    void Awake()
    {
        stateMachine = new CharacterStateMachine();
        stateIdle = new CharacterStateIdle(stateMachine, this);
        stateMoving = new CharacterStateMoving(stateMachine, this);
        stateJumping = new CharacterStateJumping(stateMachine, this, _jumpForce);
        stateWallJumping = new CharacterStateWallJumping(stateMachine, this, _jumpForce);
        stateFalling = new CharacterStateFalling(stateMachine, this, _fallMultiplier);
        stateLeftSliding = new CharacterStateSlidingLeft(stateMachine, this, 1);
        stateRightSliding = new CharacterStateSlidingRight(stateMachine, this, -1);

        stateMachine.Initialize(stateIdle);

        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y,0);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
        }

        animator.SetBool(IS_GROUNDED_PARAM, _feet.GetComponent<FeetPlayer>().isGrounded);
        animator.SetFloat(VELX_PARAM, _rb.velocity.x);
        animator.SetFloat(VELY_PARAM, _rb.velocity.y);
        animator.SetFloat(SPEEDX_PARAM, Math.Abs(_rb.velocity.x));
        animator.SetFloat(SPEEDY_PARAM, Math.Abs(_rb.velocity.y));
    }

}
