using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateKunai : CharacterState
{

    private bool endAttack;

    [SerializeField] public float kunaiRadius;
    [SerializeField] public float rangeRadius;
    [SerializeField] public float stopPower;

    [SerializeField]private Vector2 mousePos;

    [SerializeField] public GameObject firePoint;

    [SerializeField] public GameObject currentKunai;
    [SerializeField] public GameObject Grab;
    //kunai
    [SerializeField] public GameObject Kunai;

    public CharacterStateKunai() : base() { }


    private void Start()
    {

    }

    public override void EnterState()
    {
        endAttack = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }


    public override void ExitState()
    {
        player.isAttaking = false;
    }

    public override void UpdateFrame()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckRange();
        if (currentKunai != null)
        {
            endAttack = CheckKunai();
        }

        if (endAttack) //Attaquer avec le kunai
        {
            GameObject target = currentKunai.GetComponent<Kunai>().transform.parent.gameObject;
            if (target.tag == "Enemy")
            {
                ((Enemy)target.GetComponent<Enemy>()).TakeDamage(3);
            }
        }

        base.UpdateFrame();

    }

    public override void OnChangeState()
    {
        if (endAttack)
        {
            if (player._rb.velocity.y > 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);
            }
            else if (player._rb.velocity.y < 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);
            }
            else if (player._rb.velocity.x != 0)
            {
                characterStateMachine.ChangeState(player.stateMoving);
            }
            else
            {
                characterStateMachine.ChangeState(player.stateIdle);
            }
        }
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        currentKunai = Instantiate(Kunai, firePoint.transform.position, firePoint.transform.rotation);
        Grab.GetComponent<Grab>().GetCurrentKunai(currentKunai);
        currentKunai.GetComponent<Kunai>().destiny = mousePos;

    }
    public bool CheckKunai()
    {
        return currentKunai.GetComponent<Kunai>().isAttached;
    }

    public void CheckRange()
    {
        if (currentKunai == null)
            return;

        Kunai scriptKunai = currentKunai.GetComponent<Kunai>();
        Vector2 playerPos = transform.position;
        Vector2 KunaiPos = currentKunai.transform.position;
        Vector2 direction = (playerPos - KunaiPos).normalized;
        float distance = Vector2.Distance(KunaiPos, playerPos);
        if (distance > rangeRadius && scriptKunai.attachable == true && scriptKunai.isAttached == false)
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce(direction * stopPower, ForceMode2D.Impulse);
            KunaiRb.bodyType = RigidbodyType2D.Dynamic;
            scriptKunai.attachable = false;
            scriptKunai.fallen = true;
        }
        if (distance > rangeRadius + 1.15f && scriptKunai.fallen == true && scriptKunai.isAttached == false)
        {
            Rigidbody2D KunaiRb = currentKunai.GetComponent<Rigidbody2D>();
            KunaiRb.AddForce((playerPos - direction) * 0.5f, ForceMode2D.Impulse);
        }
        if (distance > rangeRadius + 1.15f && scriptKunai.isAttached == true)
        {
            player._rb.AddForce((KunaiPos - playerPos) * 0.5f);
        }
    }
}
