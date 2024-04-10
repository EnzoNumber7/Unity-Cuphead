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
    public CharacterStateSlidingLeft    stateLeftSliding;
    public CharacterStateSlidingRight   stateRightSliding;
    public CharacterStateAttaking       stateAttakcing;
    public CharacterStateKunai          stateKunai;

    #endregion


    #region AnimatorVariables

    const string IS_GROUNDED_PARAM = "IsGrounded";
    const string VELX_PARAM = "Xvelocity";
    const string VELY_PARAM = "Yvelocity";
    const string SPEEDY_PARAM = "Yspeed";
    const string SPEEDX_PARAM = "Xspeed";
    const string ISATTACKING_PARAM = "IsAttacking";

    #endregion

    [Range(20, 50)] public float _speed;

    public Rigidbody2D _rb;
    public GameObject _feet;

    public GameObject _leftSide;
    public GameObject _rightSide;
    public GameObject _cac;

    public Animator animator;
    public bool isAttaking;

    //Kunai 
    private float kunaiRadius;
    private GameObject firePoint;

    public bool isUsed;


    void Awake()
    {
        //stateMachine = new CharacterStateMachine();
        //stateIdle = new CharacterStateIdle(stateMachine, this);
        //stateMoving = new CharacterStateMoving(stateMachine, this);
        //stateJumping = new CharacterStateJumping(stateMachine, this, _jumpForce);
        //stateWallJumping = new CharacterStateWallJumping(stateMachine, this, _jumpForce);
        //stateFalling = new CharacterStateFalling(stateMachine, this, _fallMultiplier);
        //stateLeftSliding = new CharacterStateSlidingLeft(stateMachine, this, 1);
        //stateRightSliding = new CharacterStateSlidingRight(stateMachine, this, -1);
        //stateAttakcing = new CharacterStateAttaking(stateMachine, this) ;

        stateMachine.Initialize(stateIdle);

        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //_isGrounded = _feet.GetComponent<FeetPlayer>().isGrounded;

        kunaiRadius = stateKunai.kunaiRadius;
        firePoint = stateKunai.firePoint;

}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        OnChangeState();

        stateMachine.currentState.UpdateFrame();

        firePointPos();

        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y,0);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 0);
        }

        animator.SetBool(IS_GROUNDED_PARAM, _feet.GetComponent<FeetPlayer>().isGrounded);
        animator.SetFloat(VELX_PARAM, _rb.velocity.x);
        animator.SetFloat(VELY_PARAM, _rb.velocity.y);
        animator.SetFloat(SPEEDX_PARAM, Math.Abs(_rb.velocity.x));
        animator.SetFloat(SPEEDY_PARAM, Math.Abs(_rb.velocity.y));
        
    }

    public void OnChangeState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttaking = true;
            animator.SetBool(ISATTACKING_PARAM, isAttaking);
            stateMachine.ChangeState(stateAttakcing);
        }
    }

    private void firePointPos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - stateKunai.firePoint.transform.position.y, mousePos.x - stateKunai.firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        stateKunai.firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        Vector2 firePointPos = stateKunai.firePoint.transform.position;
        Vector2 playerPos = transform.position;
        Vector2 direction = (mousePos - firePointPos).normalized;

        stateKunai.firePoint.transform.position = playerPos + direction * kunaiRadius;

    }

}