using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public CharacterStateSliding        stateSliding;
    public CharacterStateAttaking       stateAttakcing;
    public CharacterStateKunai          stateKunai;
    public CharacterStateBalancing      stateBalancing;

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
    [SerializeField] private float hp;
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
    public BlockedDirection blockedDirection = BlockedDirection.None;

    public bool isUsed;

    [SerializeField] public int coins = 0;
    [SerializeField] int test;


    void Awake()
    {
        stateMachine.Initialize(stateIdle);

        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        Respawn();
        OnChangeState();
        stateMachine.currentState.UpdateFrame();

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y,0);

        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 0);
            firePoint.transform.localScale = new Vector3(Math.Abs(firePoint.transform.localScale.x) * -1, firePoint.transform.localScale.y, 0);

        }

        animator.SetBool(IS_GROUNDED_PARAM, _feet.GetComponent<FeetPlayer>().isGrounded);
        animator.SetFloat(VELX_PARAM, _rb.velocity.x);
        animator.SetFloat(VELY_PARAM, _rb.velocity.y);
        animator.SetFloat(SPEEDX_PARAM, Math.Abs(_rb.velocity.x));
        animator.SetFloat(SPEEDY_PARAM, Math.Abs(_rb.velocity.y));

    }

    private void FixedUpdate()
    {
        
    }

    public void OnChangeState()
    {
        if(stateKunai.currentKunai == null)
        {
            isUsed = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isAttaking = true;
            animator.SetBool(ISATTACKING_PARAM, isAttaking);
            stateMachine.ChangeState(stateAttakcing);
        }

        if (Input.GetMouseButtonDown(1) && isUsed == false)
        {
            stateMachine.ChangeState(stateKunai);
            isUsed = true;
        }
        if (stateKunai.currentKunai != null)
        {
            if (Input.GetMouseButtonDown(1) && isUsed == true && stateKunai.currentKunai.GetComponent<Kunai>().fallen == true)
            {
                stateKunai.currentKunai.GetComponent<Kunai>().ReturnToPlayer(transform.position);
            }
            if (Input.GetKeyDown(KeyCode.Q) && stateKunai.currentKunai.GetComponent<Kunai>().isAttached)
            {
                stateKunai.currentKunai.GetComponent<Kunai>().Detach(transform.position);
            }
            if(Input.GetKeyUp(KeyCode.E))
            {
                stateKunai.currentKunai.GetComponent<Kunai>().DeleteKunai();
            }
        }
    }



    public void GetCoins()
    {
        coins++;
        print(coins);
        test++;

    }

    public void TakeDamage(float damage, GameObject entity)
    {
        hp -= damage;
        KnockBack(entity);
    }

    public void KnockBack(GameObject entity)
    {
        Vector2 entityPos = entity.transform.position;
        if (entityPos.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(2, 5);
        }
        else if (entityPos.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 5);
        }
    }

    public void Respawn()
    {
        if (transform.position.y < -8)
        {
            transform.position = new Vector2(-4f, -2.9f);
            if (stateKunai.currentKunai != null)
            {
                stateKunai.currentKunai.GetComponent<Kunai>().DeleteKunai();
            }
            stateMachine.ChangeState(stateIdle);
        }
    }

}
